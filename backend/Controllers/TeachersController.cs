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
    #endregion

    #region Costructor
    public TeachersController(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }
    #endregion

    #region Api calls
    
    /// <summary> Get All Teachers </summary>
    /// <returns>ICollection<TeacherDto></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TeacherDto>))]
    public IActionResult GetTeachers()
    {
        return Ok(_teacherRepository.GetTeachers());
    }

    #endregion
}