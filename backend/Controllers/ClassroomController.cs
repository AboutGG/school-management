using backend.Dto;
using backend.Models;
using backend.Repositories;
using iText.StyledXmlParser.Jsoup.Select;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassroomController : Controller
{
    private readonly SchoolContext _context;

    public ClassroomController(SchoolContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Classroom>))]
    public IActionResult GetClassroom([FromQuery] PaginationParams @params)
    {
        var classrooms = new GenericRepository<Classroom>(_context);

        return Ok(classrooms.GetAll(@params,
            classroom => classroom.Name.Trim().ToUpper().Contains(@params.Search.Trim().ToUpper())
        ));
    }


    [HttpGet]
    [Route("{id}")]
    public IActionResult GetClassroomDetails([FromQuery] PaginationParams @params, [FromRoute] Guid id)
    {
        var students = new GenericRepository<Student>(_context)
            .GetAll(@params,
                student => student.ClassroomId == id,
                student => student.Registry);
        @params.Order = "TeacherId";
        var teachers = new GenericRepository<TeacherSubjectClassroom>(_context)
            .GetAll(@params,
                el => el.Classroom.Id == id, 
                el=> el.Teacher.Registry, el => el.Subject);
        return Ok(new
        {
            teachers,
            students
        });
    }
}