using System.IdentityModel.Tokens.Jwt;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using J2N.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class TeachersController : Controller
{
    #region Attributes

    private readonly ITransactionRepository _transactionRepository;
    private readonly SchoolContext _context;
    private readonly ITeacherRepository _teacherRepository;

    #endregion

    #region Costructor

    public TeachersController(ITeacherRepository teacherRepository, SchoolContext context, ITransactionRepository transactionRepository)
    {
        _teacherRepository = teacherRepository;
        _context = context;
        _transactionRepository = transactionRepository;
    }

    #endregion

    #region Api calls
    
    #region Get classroom

    /// <summary>
    /// Get all classrooms of a teacher 
    /// </summary>
    /// <returns> List<Id, Name, StudentCount> </returns>

    [HttpGet]
    [Route("Classrooms")]
    [ProducesResponseType(200, Type = typeof(PaginationResponse<ClassroomStudentCount>))]
    [ProducesResponseType(400)]
    public IActionResult GetClassrooms([FromQuery] PaginationParams @params, [FromHeader] string Token)
    {
        JwtSecurityToken decodedToken;
        try
        {
            //Decode the token
            decodedToken = JWTHandler.DecodeJwtToken(Token);
            Guid takenId = new Guid(decodedToken.Payload["userid"].ToString());

            // //Controllo il ruolo dello User tramite l'Id
            // var role = RoleSearcher.GetRole(takenId, _context);
            //
            // //Se lo user non è un professore creo una nuova eccezione restituendo Unauthorized
            // if (role == "student" || role == "unknown")
            //     throw new Exception("NOT_FOUND");

            var classrooms = new GenericRepository<Teacher>(_context)
                .GetAllUsingIQueryable(null,
                    query => query
                        .Include(teacher => teacher.TeachersSubjectsClassrooms)
                        .ThenInclude(tsc => tsc.Classroom.Students)
                        .Include(el => el.TeachersSubjectsClassrooms)
                        .ThenInclude(el => el.Subject)
                        .Where(tsc => tsc.UserId == takenId),
                    out var total
                    )
                .SelectMany(teacher =>
                    teacher.TeachersSubjectsClassrooms
                        .Select(tsc => tsc.Classroom)).Distinct().ToList();
            
            var filteredClassroom = new GenericRepository<Classroom>(_context)
                .GetAllUsingIQueryable(@params,
                query => classrooms.AsQueryable()
                    .Where(classrooom => classrooom.Name.ToLower().Trim().Contains(@params.Search.ToLower())),
                out var totalClassrooms
            );
            
            var result  = new List<ClassroomStudentCount>();
            foreach (var el in filteredClassroom)
            {
                result.Add(new ClassroomStudentCount(el));
            }
            return Ok(new PaginationResponse<ClassroomStudentCount>
            {
                Total = totalClassrooms,
                Data = result
            });
        }
        catch (Exception e)
        {
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }

    }

    #endregion

    #region Get Teachers

    /// <summary> Get All Teachers with its Registry and user </summary>
    /// <returns>ICollection<Teacher></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Teacher>))]
    public IActionResult GetTeachers()
    {
        return Ok(_teacherRepository.GetTeachers());
    }

    #endregion

    #region Get Subjects
    /// <summary> A method that return a Teacher's subjects list </summary>
    /// <param name="Token">Token to take the user id of the Teacher and checks the role</param>
    /// <returns>A list of Subject and his classrooms</returns>
    /// <exception cref="Exception">Errors if the token is not valid or more.</exception>
    [HttpGet]
    [Route("subjects")]
    [ProducesResponseType(200, Type = typeof(PaginationResponse<SubjectClassroomDto>))]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult GetSubjects([FromHeader] string Token, [FromQuery] PaginationParams @params)
    {
        JwtSecurityToken decodedToken;
        Guid takenId;
        //string role;
        try
        {
            //Decode the token
            decodedToken = JWTHandler.DecodeJwtToken(Token);
            takenId = new Guid(decodedToken.Payload["userid"].ToString());

            var resultTeachers = new GenericRepository<TeacherSubjectClassroom>(_context).GetAllUsingIQueryable(@params,
                query => query
                    .Where(el => el.Teacher.UserId == takenId
                    && (el.Classroom.Name.Trim().ToLower().Contains(@params.Search.Trim().ToLower())
                    || el.Subject.Name.Trim().ToLower().Contains(@params.Search.Trim().ToLower()))
                    )
                    .Include(el => el.Teacher)
                    .Include(el => el.Teacher.Registry)
                    .Include(el => el.Classroom)
                    .Include(el => el.Subject),
                out var total
            );

            // if (@params.Filter != null)
            //     resultTeacher = resultTeacher
            //         .Where(el => el.Classroom.Name.Trim().ToLower() == @params.Filter.Trim().ToLower()
            //                      || el.Subject.Name.Trim().ToLower() == @params.Filter.Trim().ToLower()
            //         ).ToList();

            List<SubjectClassroomDto> response = new List<SubjectClassroomDto>();

            foreach (var resultTeacher in resultTeachers)
            {
                response.Add(new SubjectClassroomDto(resultTeacher));
            }
            
            return Ok(new PaginationResponse<SubjectClassroomDto>
            {
                Total = total,
                Data = response
            });
        }
        catch (Exception e)
        {
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }

    #endregion

    #region GetExams

    /// <summary> Api call to take the Exams of a teacher. </summary>
    /// <param name="Token">To check the role, authorization and to take the userId</param>
    /// <param name="params">Orders, filter, search etc../param>
    /// <returns>A list of exam planned by the Teacher</returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    [Route("exams")]
    [ProducesResponseType(200, Type = typeof(List<ExamResponseDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult GetTeacherExams([FromHeader] string Token, [FromQuery] PaginationParams @params)
    {
        IGenericRepository<Teacher> teacherGenericRepository = new GenericRepository<Teacher>(_context);
        IGenericRepository<Exam> examGenericRepository = new GenericRepository<Exam>(_context);
        JwtSecurityToken decodedToken;
        Guid takenId;
        //string role;

        try
        {
            //Decodifico il token
            decodedToken = JWTHandler.DecodeJwtToken(Token);

            //Dal token decodificato prendo l'id dello user
            takenId = new Guid(decodedToken.Payload["userid"].ToString());
            
            // role = RoleSearcher.GetRole(takenId, _context);
            // if (role.Trim().ToLower() == "student" || role.Trim().ToLower() == "unknown")
            // {
            //     throw new Exception("UNAUTHORIZED");
            // }

            //Prendo la lista di esami eseguiti dal professore che come userId ha 
            List<Exam> teacherExams = examGenericRepository.GetAllUsingIQueryable(@params,
                query => query
                    . Where(el => el.TeacherSubjectClassroom.Teacher.UserId == takenId)
                    .Where(el => 
                        string.IsNullOrWhiteSpace(@params.Filter) ? true :
                        el.TeacherSubjectClassroom.Subject.Name.Trim().ToLower() == @params.Filter.Trim().ToLower() || 
                        el.TeacherSubjectClassroom.Classroom.Name.Trim().ToLower() == @params.Filter.Trim().ToLower())
                .Include( el => el.TeacherSubjectClassroom)
                .Include(el => el.TeacherSubjectClassroom.Classroom)
                    .Include(el => el.TeacherSubjectClassroom.Subject),
                out var total
            );

            List<ExamResponseDto> response = new List<ExamResponseDto>();
            
            foreach (var el in teacherExams)
            {
                response.Add(new ExamResponseDto(el.Id, el.Date, el.TeacherSubjectClassroom.Classroom, el.TeacherSubjectClassroom.Subject));
            }
            
            return Ok(new PaginationResponse<ExamResponseDto>
            {
                Total = total,
                Data = response
            });
        }
        catch (Exception e)
        {
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }

    #endregion

    #region Get exam detail

    /// <summary> A method that returns, based on an exam, the list of students who take it. </summary>
    /// <param name="params">Pagination params for pagination, orders etc..</param>
    /// <param name="id">The Id of the Exam which i want to see</param>
    /// <returns>An Exam and his Student list with the grade</returns>
    [HttpGet]
    [Route("exams/{id}")]
    [ProducesResponseType(200, Type = typeof(ExamDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetTeacherExamsDetail([FromQuery] PaginationParams @params, string id)
    {
        IGenericRepository<Exam> examGenericRepository = new GenericRepository<Exam>(_context);
        IGenericRepository<StudentExam> studentExamGenericRepository = new GenericRepository<StudentExam>(_context);
        try
        {
            Exam takenExam = examGenericRepository.GetByIdUsingIQueryable(query => query
                .Where(el => el.Id.ToString() == id)
                .Include(el => el.TeacherSubjectClassroom.Subject)
                .Include(el => el.TeacherSubjectClassroom.Classroom)
                .Include(el => el.StudentExams)
                .ThenInclude(el => el.Student)
                .ThenInclude(el => el.Registry)
            );
            @params.Order = "Student.Registry." + $"{@params.Order}";
            takenExam.StudentExams = new GenericRepository<StudentExam>(_context)
                .GetAllUsingIQueryable(@params, el => takenExam.StudentExams.AsQueryable()
                    .Where(el => el.Student.Registry.Name.Trim().ToLower()
                                     .Contains(@params.Search.Trim().ToLower())
                                 || el.Student.Registry.Surname.Trim().ToLower()
                                     .Contains(@params.Search.Trim().ToLower()))
                ,  out var total
                );

            ExamDto responseExam = new ExamDto(takenExam);

            return Ok(responseExam);
        }
        catch (Exception e)
        {
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }

    #endregion

    #region GetSubjectsFromClassroom
    /// <summary>
    /// Api call which returns all the subject of a teacher
    /// </summary>
    /// <param name="userId">id to take the single instance of teacher</param>
    /// <param name="classroomId">is used to filter the subjects by the classroom id, then returns all the subject witch teaches on the class</param>
    /// <returns>All the teachers' subject</returns>
    [HttpGet]
    [Route("{userId}/subjects")]
    [ProducesResponseType(200, Type = typeof(List<SubjectDto>))]
    [ProducesResponseType(401)]
    public IActionResult GetSujectsFromClassroom([FromRoute] Guid userId, [FromQuery] Guid? classroomId)
    {
        try
        {
            List<TeacherSubjectClassroom> takenSubjects = new GenericRepository<TeacherSubjectClassroom>(_context).GetAllUsingIQueryable(null,
                query => query
                    .Where(el => el.Teacher.UserId == userId)
                    .Include(el => el.Subject)
                    .Include(el => el.Classroom)
                , out var total
            );

            if (classroomId != null)
            {
                takenSubjects = new GenericRepository<TeacherSubjectClassroom>(_context).GetAllUsingIQueryable(null,
                    query => takenSubjects.AsQueryable()
                        .Where(el => el.Classroom.Id == classroomId)
                    , out total
                );
            }

            List<SubjectDto> response = new List<SubjectDto>();


            foreach (Subject el in takenSubjects.Select(el => el.Subject).Distinct().ToList())
            {
                response.Add(new SubjectDto(el));
            }
            
            return Ok(response);

        }
        catch (Exception e)
        {
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }
    #endregion

    #region UpdateExam
    /// <summary>
    /// Api call used to update the exam's details
    /// </summary>
    /// <param name="inputUpdateExamDto">are the data used to update the exam</param>
    /// <param name="examId">is the id of an exam's instance</param>
    /// <param name="userId">is the teacher's id which need to update the exam</param>
    /// <returns>Return the exam which the teacher updates</returns>
    /// <exception cref="Exception">if we don't found an exam and if teacher hasn't authorized to assign the class / subject which he has selected</exception>
    [HttpPut]
    [Route("{userId}/exams/{examId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult AssignStudentsVote([FromBody] ExamRequest inputUpdateExamDto,[FromRoute] Guid examId, [FromRoute] Guid userId)
    {
        IDbContextTransaction transaction = _transactionRepository.BeginTransaction();
        
        try
        {
            //Prendo l'esame di cui mi interessa modificarne i parametri
            Exam takenExam = new GenericRepository<Exam>(_context)
                .GetByIdUsingIQueryable(el => el
                    .Where(el => el.Id == examId)
                );
            
            //Prendo l'instanza di teacherSubjectClassroom nel caso in cui bisogna modificare la classe o/e la materia dell'esame
            var takenTeacher = new GenericRepository<TeacherSubjectClassroom>(_context).GetByIdUsingIQueryable(
                query => query
                    .Where(el => el.ClassroomId == inputUpdateExamDto.classroomId && 
                                 el.SubjectId == inputUpdateExamDto.subjectId && 
                                 el.Teacher.UserId == userId)
                    .Include(el => el.Teacher)
                );

            if (takenExam == null)
            {
                throw new Exception("NOT_FOUND");
            }

            if (takenTeacher == null)
            {
                throw new Exception("UNAUTHORIZED_UPDATE_EXAM");
            }
            
            //Procedo con la modifica dei dati
            takenExam.Date = inputUpdateExamDto.date;
            takenExam.TeacherSubjectClassroomId = takenTeacher.Id;
            
            
            if (! new GenericRepository<Exam>(_context).UpdateEntity(takenExam))
            {
                throw new Exception("NOT_UPDATED");
            }
            
            _transactionRepository.CommitTransaction(transaction);
            return StatusCode(StatusCodes.Status200OK, takenExam);
        }
        catch (Exception e)
        {
            _transactionRepository.RollbackTransaction(transaction);
            ErrorResponse error = ErrorManager.Error(e);
            return BadRequest(error);
        }
    }

    #endregion
    
    #endregion
}