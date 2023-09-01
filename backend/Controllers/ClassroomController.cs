using AutoMapper;
using backend.Dto;
using backend.Interfaces;
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
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMapper _mapper;

    public ClassroomController(
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
    public IActionResult GetClassroom()
    {
        return Ok(_mapper.Map<List<ClassroomDto>>(_classroomRepository.GetClassrooms()));
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