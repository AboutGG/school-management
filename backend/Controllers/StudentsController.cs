using System.IdentityModel.Tokens.Jwt;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : Controller
{
    #region Attributes

    private readonly SchoolContext _context;
    private readonly IStudentRepository _studentRepository;

    #endregion

    #region Costructor

    public StudentsController(IStudentRepository studentRepository, SchoolContext context)
    {
        _studentRepository = studentRepository;
        _context = context;
    }

    #endregion

    #region API calls

    #region Get students

    /// <summary> Get All Teachers with its Registry and user </summary>
    /// <returns>ICollection<TeacherDto></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<Student>))]
    public IActionResult GetStudents()
    {
        return Ok(_studentRepository.GetStudents());
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
            //prendo lo user che contiene l'id ricavato dal token
            takenUser = usersRepository.GetById(el => el.Id == takenId, el => el.Teacher, el => el.Student);
            
            //tramite lo user ricavo il ruolo
            role = takenUser.Student != null ? "student" : takenUser.Teacher != null ? "teacher" : "unknow";
            
            //nel caso non dovesse essere un alunno esegue una nuova exception ritornando Unauthorized
            if (role == "teacher" || role == "unknow")
                throw new Exception();
            else
            {
                //Prendo le materie che pratica lo studente
                var resultStudent = _studentRepository.GetStudentSubjects(takenId);
                return Ok(resultStudent);
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