using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class TeachersController : Controller
{
    #region Attributes
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;
    #endregion

    #region Costructor

    public TeachersController(ITeacherRepository teacherRepository, IMapper mapper)
    {
        _teacherRepository = teacherRepository;
        _mapper = mapper;
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

    #endregion
}