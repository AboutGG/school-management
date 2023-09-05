using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core;
using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : Controller
{
    #region Attributes

    private readonly SchoolContext _context;
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    #endregion

    #region Costructor

    public StudentsController(IStudentRepository studentRepository, SchoolContext context, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _context = context;
        _mapper = mapper;
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
    public IActionResult GetSubjects([FromQuery] PaginationParams @params, [FromHeader] string Token)
    {
        #region Decode the token

        JwtSecurityToken decodedToken;
        Guid takenId;
        string role;
        User takenUser;
        try
        {
            //Decode the token
            decodedToken = JWT.DecodeJwtToken(Token, "DZq7JkJj+z0O8TNTvOnlmj3SpJqXKRW44Qj8SmsW8bk=");
            takenId = new Guid(decodedToken.Payload["userid"].ToString());

            //tramite lo user ricavo il ruolo tramite l'Id
            role = RoleSearcher.GetRole(takenId, _context);

            //nel caso non dovesse essere un alunno esegue una nuova exception ritornando Unauthorized
            if (role == "teacher" || role == "unknow")
            {
                throw new Exception("NOT_FOUND");
            }
            else
            {
                //Prendo l'id della classe riguardante lo studente 
                Guid studentclassroomId = _context.Students.FirstOrDefault(el => el.UserId == takenId).ClassroomId;

                switch (@params.Order.Trim().ToLower())
                {
                    case "name":
                        @params.Order = "Teacher.Registry.Name";
                        break;
                    case "surname":
                        @params.Order = "Teacher.Registry.Surname";
                        break;
                }
                
                //Prendo le materie che pratica lo studente nella sua classe
                var resultStudent = new GenericRepository<TeacherSubjectClassroom>(_context).GetAll(@params,
                    query => query
                        .Where(el => el.ClassroomId == studentclassroomId)
                        .Include(el => el.Classroom)
                        .Include(el => el.Teacher.Registry)
                        .Include(el => el.Subject)
                        );
                
                return Ok(_mapper.Map<List<TeacherSubjectClassroomDto>>(resultStudent.DistinctBy(el => el.TeacherId)));
            }
        }
        catch (Exception e)
        {
            switch (e.Message)
            {
                case "NOT_FOUND":
                    return StatusCode(StatusCodes.Status404NotFound);
                case "UNAUTHORIZED":
                    return StatusCode(StatusCodes.Status401Unauthorized, "The token in not valid");
                default:
                    return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        #endregion
    }

    #endregion

    #endregion
}