using System.Linq.Dynamic.Core;
using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Guid = System.Guid;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassroomsController : Controller
{
    private readonly SchoolContext _context;
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMapper _mapper;
    private readonly ITeacherRepository _teacherRepository;

    public ClassroomsController(
        SchoolContext context, 
        IClassroomRepository classroomRepository,
        IMapper mapper,
        ITeacherRepository teacherRepository)
    {
        _context = context;
        _classroomRepository = classroomRepository;
        _mapper = mapper;
        _teacherRepository = teacherRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<ClassroomDto>))]
    public IActionResult GetClassroomsList()
    {
        var classrooms = new GenericRepository<Classroom>(_context)
            .GetAll2(null, (Func<IQueryable<Classroom>, IQueryable<Classroom>>?)null);
        return Ok(_mapper.Map<List<ClassroomDto>>(classrooms));
    }


    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200, Type = typeof(List<ClassroomDetails>))]
    public IActionResult GetClassroomDetails([FromQuery] PaginationParams @params, [FromRoute] Guid id)
    {
        var students = _mapper.Map<List<StudentDto>>(new GenericRepository<Student>(_context)
            .GetAll2(@params,
                query => query
                    .Where(student => student.ClassroomId == id)
                    .Include(student => student.Registry)));

        var teachers = _mapper.Map<List<TeacherDto>>(new GenericRepository<Teacher>(_context)
            .GetAll2(
                null, 
                query => query
                    .Where(teacher => teacher.TeachersSubjectsClassrooms
                        .Any(tsc => tsc.ClassroomId == id))
                    .Include(teacher => teacher.TeachersSubjectsClassrooms)
                    .ThenInclude(tsc => tsc.Subject)
                    .Include(teacher => teacher.Registry)));
        
        return Ok(new { students, teachers });
    }
    
   
}
