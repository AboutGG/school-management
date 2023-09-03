using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        var teachers = new GenericRepository<Teacher>(_context)
            .GetAllM(@params,
                el => el.TeacherSubjectsClassrooms.Any(tsc => tsc.ClassroomId == id),
                c =>
                    c.Include(s => s.Registry)
                        .Include(s => s.TeacherSubjectsClassrooms)
                        .ThenInclude(s => s.Subject));
        
        var lol = _mapper.Map<List<AnotherDto>>(teachers);

        var sex = students.Select(el => new
        {
            id = el.UserId,
            name = el.Registry.Name,
            surname = el.Registry.Surname,
            gender = el.Registry.Gender
        }).ToList();

        return Ok(new
        {
            lol,
            sex
        });
    }
}