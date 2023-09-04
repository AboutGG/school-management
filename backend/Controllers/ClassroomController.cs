using System.Linq.Dynamic.Core;
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
        var classrooms = new GenericRepository<Classroom>(_context)
            .GetAll(null, (Func<IQueryable<Classroom>, IQueryable<Classroom>>?)null);
        return Ok(_mapper.Map<List<ClassroomDto>>(classrooms));
    }


    [HttpGet]
    [Route("{id}")]
    public IActionResult GetClassroomDetails([FromQuery] PaginationParams @params, [FromRoute] Guid id)
    {
        var studentsRepo = new GenericRepository<Student>(_context)
            .GetAll(@params,
                query => query
                    .Where(student => student.ClassroomId == id)
                    .Include(student => student.Registry));

        var teachers = _mapper.Map<List<TeacherDto>>(new GenericRepository<Teacher>(_context)
            .GetAll(
                null, 
                query => query
                    .Include(teacher => teacher.Registry)
                    .Include(teacher => teacher.TeacherSubjectsClassrooms
                        .Where(tsc => tsc.ClassroomId == id))
                    .ThenInclude(tsc => tsc.Subject)));

        var students = studentsRepo.Select(el => new
        {
            id = el.UserId,
            name = el.Registry.Name,
            surname = el.Registry.Surname,
            gender = el.Registry.Gender
        }).ToList();

        return Ok(new
        {
            teachers,
            students
        });

    }
}
