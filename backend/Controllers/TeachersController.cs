using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
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

    #region Get classroom by teacher id

    /// <summary>
    /// Get all classrooms of a teacher TODO add pagination
    /// </summary>
    /// <returns> List<Id, Name, StudentCount> </returns>

    [HttpGet]
    [Route("{id}/classroom")]
    [ProducesResponseType(200, Type = typeof(List<Classroom>))]
    [ProducesResponseType(400)]
    public IActionResult GetClassrooms([FromRoute] Guid id)
    {
        // var classroomWithStudentCount = _teacherRepository.GetClassroomByTeacherId(id)
        //     .Select(el => new ClassroomStudentCount()
        //     {
        //         ClassroomId = el.Id,
        //         Name = el.Name,
        //         StudentCount = el.Students.Count()
        //     })
        //     .ToList();
        return Ok(_mapper.Map<List<ClassroomStudentCount>>(_teacherRepository.GetClassroomByTeacherId(id)));
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
            decodedToken = JWT.DecodeJwtToken(Token);
            takenId = new Guid(decodedToken.Payload["userid"].ToString());

            //Controllo il ruolo dello User tramite l'Id
            role = RoleSearcher.GetRole(takenId, _context);

            //Se lo user non è un professore creo una nuova eccezione restituendo Unauthorized
            if (role == "student" || role == "unknow")
                throw new Exception("NOT_FOUND");
            else
            {
                //Prendo le materie che insegna il professore con le relative classi
                var resultTeacher = new GenericRepository<Teacher>(_context).
                    GetById2(query => query
                        .Include(el => el.Registry)
                        .Include(el => el.TeacherSubjectsClassrooms)
                        .ThenInclude(el => el.Classroom)
                        .Include(el => el.TeacherSubjectsClassrooms)
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
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [Authorize]
    public IActionResult GetTeacherExams([FromHeader] string Token)
    {
        IGenericRepository<Teacher> teacherGenericRepository = new GenericRepository<Teacher>(_context);
        IGenericRepository<Exam> examGenericRepository = new GenericRepository<Exam>(_context);
        JwtSecurityToken decodedToken;
        Guid takenId;
        string role;

        try
        {
            //Decodifico il token
            decodedToken = JWT.DecodeJwtToken(Token);
            
            //Dal token decodificato prendo l'id dello user
            takenId = new Guid(decodedToken.Payload["userid"].ToString());
            
            //prendo il professore che come userId ha quello ricavato dal token
            Teacher teacher = teacherGenericRepository.GetById2(
                query => query.Where(el => el.UserId == takenId)
                    .Include(el => el.TeacherSubjectsClassrooms));
            List<Guid> classroomsId = teacher.TeacherSubjectsClassrooms.Select(el => el.ClassroomId).ToList();
            List<Guid> subjectsId = teacher.TeacherSubjectsClassrooms.Select(el => el.SubjectId).ToList();

            var dummy = examGenericRepository.GetById2(query =>
                query.Where(el => el.SubjectId == subjectsId.First())
            );
        }
        catch (Exception e)
        {
            return BadRequest(ErrorManager.Error(e.Message));
        }
        return Ok();
    }

    #endregion
    #endregion
}