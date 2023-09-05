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
public class ClassroomsController : Controller
{
    private readonly SchoolContext _context;
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMapper _mapper;

    public ClassroomsController(
        SchoolContext context, 
        IClassroomRepository classroomRepository,
        IMapper mapper)
    {
        _context = context;
        _classroomRepository = classroomRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<ClassroomDto>))]
    public IActionResult GetClassroomsList()
    {
        var classrooms = new GenericRepository<Classroom>(_context)
            .GetAll(null, (Func<IQueryable<Classroom>, IQueryable<Classroom>>?)null);
        return Ok(_mapper.Map<List<ClassroomDto>>(classrooms));
    }


    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200, Type = typeof(List<ClassroomDetails>))]
    public IActionResult GetClassroomDetails([FromQuery] PaginationParams @params, [FromRoute] Guid id)
    {
        var students = _mapper.Map<List<StudentDto>>(new GenericRepository<Student>(_context)
            .GetAll(@params,
                query => query
                    .Where(student => student.ClassroomId == id)
                    .Include(student => student.Registry)));

        var teachers = _mapper.Map<List<TeacherDto>>(new GenericRepository<Teacher>(_context)
            .GetAll(
                null, 
                query => query
                    .Include(teacher => teacher.Registry)
                    .Include(teacher => teacher.TeacherSubjectsClassrooms
                        .Where(tsc => tsc.ClassroomId == id))
                    .ThenInclude(tsc => tsc.Subject)));
        
        return Ok(new { students, teachers });

    }
    
    #region Get classroom by teacher id

    /// <summary>
    /// Get all classrooms of a teacher TODO add pagination
    /// </summary>
    /// <returns> List<Id, Name, StudentCount> </returns>

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<Classroom>))]
    [ProducesResponseType(400)]
    public IActionResult GetClassrooms([FromQuery] PaginationParams @params, [FromHeader] string token)
    {
        JwtSecurityToken decodedToken;
        try
        {
            //Decode the token
            decodedToken = JWT.DecodeJwtToken(token, "DZq7JkJj+z0O8TNTvOnlmj3SpJqXKRW44Qj8SmsW8bk=");
            Guid takenId = new Guid(decodedToken.Payload["userid"].ToString());

            //Controllo il ruolo dello User tramite l'Id
            var role = RoleSearcher.GetRole(takenId, _context);

            //Se lo user non è un professore creo una nuova eccezione restituendo Unauthorized
            if (role == "student" || role == "unknow")
                throw new Exception("NOT_FOUND");

            var classrooms = new GenericRepository<Teacher>(_context)
                .GetAll(@params,
                    query => query
                        .Include(teacher => teacher.TeachersSubjectsClassrooms)
                        .ThenInclude(tsc => tsc.Classroom)
                        .Select());

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       // return Ok(_mapper.Map<List<ClassroomStudentCount>>(_teacherRepository.GetClassroomByTeacherId(id)));
    }
    
    #endregion
}
