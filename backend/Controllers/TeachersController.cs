using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
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
    [ProducesResponseType(200, Type = typeof(ICollection<Classroom>))]
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

    #endregion
}