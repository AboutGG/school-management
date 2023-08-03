using backend.Dto;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DetailsController : Controller
{
    #region Attributes

    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;

    #endregion

    #region Costructor

    public DetailsController(IStudentRepository studentRepository, ITeacherRepository teacherRepository)
    {
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
    }

    #endregion

    #region Api calls

    [HttpGet("{Id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult GetUserDetail(Guid Id ,[FromQuery] bool isStudent)
    {
        if (Id == null)
        {
            return BadRequest("Id is null");
        }

        if (isStudent && _studentRepository.StudentExist(Id))
        {
            return Ok(_studentRepository.GetStudentById(Id));
        } //else if (_teacherRepository.te)
        // {
        //     
        // }
        return NotFound();
    }

    #endregion
}