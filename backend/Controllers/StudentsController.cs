using backend.Dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : Controller
{
    #region Attributes

    private readonly IStudentRepository _studentRepository;

    #endregion

    #region Costructor

    public StudentsController(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    #endregion

    #region API calls

    #region Get students

    /// <summary> Get All Teachers with its Registry and user </summary>
    /// <returns>ICollection<TeacherDto></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
    public IActionResult GetStudents()
    {
        return Ok(_studentRepository.GetStudents());
    }

    #endregion
    
    #endregion
}