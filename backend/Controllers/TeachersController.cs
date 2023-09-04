using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core.Tokenizer;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace backend.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class TeachersController : Controller
{
    #region Attributes

    private readonly ITeacherRepository _teacherRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly SchoolContext _context;

    #endregion

    #region Costructor

    public TeachersController(
        ITeacherRepository teacherRepository,
        IStudentRepository studentRepository,
        SchoolContext context)
    {
        _teacherRepository = teacherRepository;
        _studentRepository = studentRepository;
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
        var classroomWithStudentCount = _teacherRepository.GetClassroomByTeacherId(id)
            .Select(c => new 
            {
                c.Id,
                c.Name,
                StudentCount = c.Students.Count()
            })
            .ToList();
        
        return Ok(classroomWithStudentCount);
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
        #region Decode the token

        JwtSecurityToken decodedToken;
        Guid takenId;
        string role;
        User takenUser;
        IGenericRepository<User> usersRepository = new GenericRepository<User>(_context);
        
        try
        {
            //Decode the token
            decodedToken = JWT.DecodeJwtToken(Token, "DZq7JkJj+z0O8TNTvOnlmj3SpJqXKRW44Qj8SmsW8bk=");
            takenId = new Guid(decodedToken.Payload["userid"].ToString());
            
            //Prendo lo user tramite l'id che ricavo dal Token
            takenUser = usersRepository.GetById(el => el.Id == takenId, el => el.Teacher, el => el.Student);
            
            //Controllo il ruolo dello User
            role = takenUser.Student != null ? "student" : takenUser.Teacher != null ? "teacher" : "unknow";
            
            //Se lo user non è un professore creo una nuova eccezione restituendo Unauthorized
            if (role == "student" || role == "unknow")
                throw new Exception();
            else
            {
                //Prendo le materie che insegna il professore con le relative classi
                var resultTeacher = _teacherRepository.GetTeacherSubjectClassroom(takenId);
                return Ok(resultTeacher);
            }
        }
        catch (Exception e)
        {
            return Unauthorized("The token is not valid");
        }

        #endregion
        
    }

    #endregion
    #endregion
}