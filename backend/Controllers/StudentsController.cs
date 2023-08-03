using backend.Dto;
using backend.Interfaces;
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

    /// <summary> Get All Teachers with its Registry and user </summary>
    /// <returns>ICollection<TeacherDto></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<StudentDto>))]
    public IActionResult GetStudents()
    {
        return Ok(_studentRepository.GetStudents());
    }

    #endregion
}