using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassroomController : Controller
{
    private readonly ITeacherSubjectRepository _teacherSubjectRepository;
    private readonly IStudentRepository _studentRepository;

    public ClassroomController(ITeacherSubjectRepository teacherSubjectRepository, IStudentRepository studentRepository)
    {
        this._teacherSubjectRepository = teacherSubjectRepository;
        this._studentRepository = studentRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    public IActionResult GetClassroom(string classroom, int skip = 1, int take = 10)
    {
        var teacher = this._teacherSubjectRepository.GetTeachersSubjects()
            .Where(el => el.Classroom == classroom)
            .Select(el => new
            {
                id = el.Teacher.Registry.Id
            });
        this._studentRepository.GetStudents()
            .Where(columns => columns.Classroom == classroom);

        return Ok(teacher);

    }
}