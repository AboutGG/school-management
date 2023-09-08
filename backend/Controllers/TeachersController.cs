using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
using J2N.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            if (role == "student" || role == "unknow")
                throw new Exception("NOT_FOUND");

            var classrooms = new GenericRepository<Teacher>(_context)
                .GetAll2(@params,
                    query => query
                        .Include(teacher => teacher.TeachersSubjectsClassrooms)
                        .ThenInclude(tsc => tsc.Classroom.Students)
                        .Where(tsc => tsc.UserId == takenId))
                .SelectMany(teacher =>
                    teacher.TeachersSubjectsClassrooms
                        .Select(tsc => tsc.Classroom)).ToList();

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

    [HttpGet]
    [Route("subjects")]
    [ProducesResponseType(200)]
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
            if (role == "student" || role == "unknow")
                throw new Exception("NOT_FOUND");
            else
            {
                //Prendo le materie che insegna il professore con le relative classi
                var resultTeacher = new GenericRepository<Teacher>(_context).GetById2(query => query
                    .Include(el => el.Registry)
                    .Include(el => el.TeachersSubjectsClassrooms)
                    .ThenInclude(el => el.Classroom)
                    .Include(el => el.TeachersSubjectsClassrooms)
                    .ThenInclude(el => el.Subject)
                );
                return Ok(_mapper.Map<TeacherDto>(resultTeacher));
            }
        }
        catch (Exception e)
        {
            return BadRequest(ErrorManager.Error(e.Message));
        }
    }

    #endregion

    #region GetExams

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
            if (role.Trim().ToLower() == "student" || role.Trim().ToLower() == "unknow")
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
                        el.TeacherSubjectClassroom.Subject.Name.Trim().ToLower() == @params.Filter.Trim().ToLower()).ToList();
            return Ok(_mapper.Map<List<TeacherExamDto>>(dummy));
        }
        catch (Exception e)
        {
            return BadRequest(ErrorManager.Error(e.Message));
        }
    }

    #endregion

    #endregion
}