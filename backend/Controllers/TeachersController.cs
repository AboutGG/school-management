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

    #region Get classroom by teacher id

    /// <summary>
    /// Get all classrooms of a teacher 
    /// </summary>
    /// <returns> List<Id, Name, StudentCount> </returns>

    [HttpGet]
    [Route("Classrooms")]
    [ProducesResponseType(200, Type = typeof(List<ClassroomStudentCount>))]
    [ProducesResponseType(400)]
    public IActionResult GetClassrooms([FromQuery] PaginationParams @params, [FromHeader] string token)
    {
        JwtSecurityToken decodedToken;
        try
        {
            //Decode the token
            decodedToken = JWTHandler.DecodeJwtToken(token);
            Guid takenId = new Guid(decodedToken.Payload["userid"].ToString());

            //Controllo il ruolo dello User tramite l'Id
            var role = RoleSearcher.GetRole(takenId, _context);

            //Se lo user non è un professore creo una nuova eccezione restituendo Unauthorized
            if (role == "student" || role == "unknown")
                throw new Exception("NOT_FOUND");

            var classrooms = new GenericRepository<Teacher>(_context)
                .GetAllUsingIQueryable(@params,
                    query => query
                        .Include(teacher => teacher.TeachersSubjectsClassrooms)
                        .ThenInclude(tsc => tsc.Classroom.Students)
                        .Where(tsc => tsc.UserId == takenId))
                .SelectMany(teacher =>
                    teacher.TeachersSubjectsClassrooms
                        .Select(tsc => tsc.Classroom)).Distinct().ToList();

            var filterclassroom = classrooms
                .Where(classrooom => classrooom.Name.ToLower().Trim().Contains(@params.Search.ToLower())).ToList();
            return Ok(_mapper.Map<List<ClassroomStudentCount>>(filterclassroom));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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
    [ProducesResponseType(200, Type = typeof(TeacherDto))]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult GetSubjects([FromHeader] string Token)
    {
        JwtSecurityToken decodedToken;
        Guid takenId;
        string role;
        try
        {
            //Decode the token
            decodedToken = JWTHandler.DecodeJwtToken(Token);
            takenId = new Guid(decodedToken.Payload["userid"].ToString());

            //Controllo il ruolo dello User tramite l'Id
            role = RoleSearcher.GetRole(takenId, _context);
            
            //Se lo user non è un professore creo una nuova eccezione restituendo Unauthorized
            if (role == "student" || role == "unknown")
                throw new Exception("UNAUTHORIZED");
            else
            {
                //Prendo le materie che insegna il professore con le relative classi
                var resultTeacher = new GenericRepository<Teacher>(_context).GetByIdUsingIQueryable(query => query
                    .Where(el => el.UserId == takenId)
                    .Include(el => el.Registry)
                    .Include(el => el.TeachersSubjectsClassrooms)
                    .ThenInclude(el => el.Classroom)
                    .Include(el => el.TeachersSubjectsClassrooms)
                    .ThenInclude(el => el.Subject)
                );
                return Ok(_mapper.Map<TeacherSubjectDto>(resultTeacher));
            }
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
    [ProducesResponseType(200, Type = typeof(List<TeacherExamDto>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult GetTeacherExams([FromHeader] string Token, [FromQuery] PaginationParams @params)
    {
        IGenericRepository<Teacher> teacherGenericRepository = new GenericRepository<Teacher>(_context);
        IGenericRepository<Exam> examGenericRepository = new GenericRepository<Exam>(_context);
        JwtSecurityToken decodedToken;
        Guid takenId;
        string role;

        try
        {
            //Decodifico il token
            decodedToken = JWTHandler.DecodeJwtToken(Token);

            //Dal token decodificato prendo l'id dello user
            takenId = new Guid(decodedToken.Payload["userid"].ToString());
            role = RoleSearcher.GetRole(takenId, _context);
            if (role.Trim().ToLower() == "student" || role.Trim().ToLower() == "unknown")
            {
                throw new Exception("UNAUTHORIZED");
            }

            //Prendo la lista di esami eseguiti dal professore che come userId ha 
            List<Exam> dummy = examGenericRepository.GetAll(@params,
                el => el.TeacherSubjectClassroom.Teacher.UserId == takenId,
                el => el.TeacherSubjectClassroom,
                el => el.TeacherSubjectClassroom.Classroom,
                el => el.TeacherSubjectClassroom.Subject
            );
            if (@params.Filter != null)
                dummy = dummy.Where(el =>
                        el.TeacherSubjectClassroom.Subject.Name.Trim().ToLower() == @params.Filter.Trim().ToLower()
                        || el.TeacherSubjectClassroom.Classroom.Name.Trim().ToLower() == @params.Filter.Trim().ToLower()
                        )
                    .ToList();
            return Ok(_mapper.Map<List<TeacherExamDto>>(dummy));
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
                                     .Contains(@params.Search.Trim().ToLower())
                    ));

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