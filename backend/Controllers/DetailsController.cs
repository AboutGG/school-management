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

    #region Get user detail by id

    [HttpGet("{Id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult GetUserDetail(Guid Id)
    {
        if (Id == null)
            return BadRequest("Id is null");

        if (_studentRepository.StudentExist(Id))
            return Ok(_studentRepository.GetStudentById(Id));
        else if (_teacherRepository.TeacherExists(Id))
            return Ok(_teacherRepository.GetTeacherById(Id));
        return NotFound();
    }

    #endregion
    
    #endregion
}