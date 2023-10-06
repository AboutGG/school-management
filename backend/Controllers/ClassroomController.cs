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
    
    /// <summary> This API call are used to take all the classrooms when you create an Student </summary>
    /// <returns>All the classrooms present on the Database</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ClassroomDto))]
    public IActionResult GetClassroomsList()
    {
        var classrooms = new GenericRepository<Classroom>(_context)
            .GetAllUsingIQueryable(null, (Func<IQueryable<Classroom>, IQueryable<Classroom>>?)null, out var total);
        return Ok(_mapper.Map<List<ClassroomDto>>(classrooms));
    }


    //TODO: fix the response of this api call
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200, Type = typeof(PaginationResponse<ClassroomDetails>))]
    public IActionResult GetClassroomDetails([FromQuery] PaginationParams @params, [FromRoute] Guid id)
    {
        var mappedStudents = _mapper.Map<List<StudentDto>>(new GenericRepository<Student>(_context)
            .GetAllUsingIQueryable(@params,
                query => query
                    .Where(student => student.ClassroomId == id)
                    .Include(student => student.Registry)
                , out var totalStudents
            )

        );

        var classroom = new GenericRepository<Classroom>(_context)
            .GetByIdUsingIQueryable(el => el.Where(classroom => classroom.Id == id)).Name;
        
        var teachers = _mapper.Map<List<TeacherDto>>(new GenericRepository<Teacher>(_context)
            .GetAllUsingIQueryable(
                null,
                query => query
                    .Where(teacher => teacher.TeachersSubjectsClassrooms
                        .Any(tsc => tsc.ClassroomId == id))
                    .Include(teacher => teacher.TeachersSubjectsClassrooms)
                    .ThenInclude(tsc => tsc.Subject)
                    .Include(teacher => teacher.Registry),
                out var totalTeachers
            ));

        return Ok(
            new PaginationResponse<ClassroomDetails>
            {
                Total = totalStudents,
                Data = new ClassroomDetails
                {
                    name_classroom = classroom,
                    Students = mappedStudents,
                    Teachers = teachers
                }
            }
        );
    }


}
