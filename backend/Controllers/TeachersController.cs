using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using J2N.Text;

namespace backend.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class TeachersController : Controller
{
    #region Attributes

    private readonly SchoolContext _context;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;

    #endregion

    #region Costructor

    public TeachersController(ITeacherRepository teacherRepository, IMapper mapper, SchoolContext context)
    {
        _teacherRepository = teacherRepository;
        _mapper = mapper;
        _context = context;
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
    [ProducesResponseType(200, Type = typeof(List<ClassroomStudentCount>))]
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
    [ProducesResponseType(200, Type = typeof(SubjectClassroomDto))]
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

            var resultTeacher = new GenericRepository<TeacherSubjectClassroom>(_context).GetAllUsingIQueryable(@params,
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

            var mappedResponse = _mapper.Map<List<SubjectClassroomDto>>(resultTeacher);
            
            return Ok(new PaginationResponse<SubjectClassroomDto>
            {
                Total = total,
                Data = mappedResponse
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
                .Include( el => el.TeacherSubjectClassroom)
                .Include(el => el.TeacherSubjectClassroom.Classroom)
                    .Include(el => el.TeacherSubjectClassroom.Subject),
                out var total
            );
            if (@params.Filter != null)
                teacherExams = teacherExams.Where(el =>
                        el.TeacherSubjectClassroom.Subject.Name.Trim().ToLower() == @params.Filter.Trim().ToLower()
                        || el.TeacherSubjectClassroom.Classroom.Name.Trim().ToLower() == @params.Filter.Trim().ToLower()
                        )
                    .ToList();

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

            ExamDto mappedExams = _mapper.Map<ExamDto>(takenExam);

            return Ok(mappedExams);
        }
        catch (Exception e)
        {
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }

    #endregion

    #endregion
}