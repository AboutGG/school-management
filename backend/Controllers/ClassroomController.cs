using System.Linq.Dynamic.Core;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Guid = System.Guid;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassroomsController : Controller
{
    private readonly SchoolContext _context;
    private readonly IClassroomRepository _classroomRepository;
    private readonly ITeacherRepository _teacherRepository;

    public ClassroomsController(
        SchoolContext context,
        IClassroomRepository classroomRepository,
        ITeacherRepository teacherRepository)
    {
        _context = context;
        _classroomRepository = classroomRepository;
        _teacherRepository = teacherRepository;
    }

    #region GetClassrooms

    /// <summary> This API call are used to take all the classrooms when you create an Student </summary>
    /// <returns>All the classrooms present on the Database</returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(200, Type = typeof(List<ClassroomDto>))]
    public IActionResult GetClassroomsList()
    {
        var classrooms = new GenericRepository<Classroom>(_context)
            .GetAllUsingIQueryable(null, query => query, out var total);

        List<ClassroomDto> response = new List<ClassroomDto>();

        foreach (Classroom classroom in classrooms)
        {
            response.Add(new ClassroomDto(classroom));
        }

        return Ok(response);
    }

    #endregion

    #region GetClassroomDetails

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200, Type = typeof(PaginationResponse<ClassroomDetails>))]
    public IActionResult GetClassroomDetails([FromQuery] PaginationParams @params, [FromRoute] Guid id)
    {
        List<Student> takenStudents = new GenericRepository<Student>(_context)
            .GetAllUsingIQueryable(@params,
                query => query
                    .Where(el => el.ClassroomId == id)
                    .Include(el => el.Registry)
                    .Include(el => el.Classroom)
                , out var totalStudents
            );

        List<StudentDto> responseStudents = new List<StudentDto>();
        foreach (Student takenStudent in takenStudents)
        {
            responseStudents.Add(new StudentDto(takenStudent));
        }

        var classroom = new GenericRepository<Classroom>(_context)
            .GetByIdUsingIQueryable(el => el.Where(classroom => classroom.Id == id)).Name;

        var teachers = new GenericRepository<Teacher>(_context)
            .GetAllUsingIQueryable(
                null,
                query => query
                    .Where(teacher => teacher.TeachersSubjectsClassrooms
                        .Any(tsc => tsc.ClassroomId == id))
                    .Include(teacher => teacher.TeachersSubjectsClassrooms)
                    .ThenInclude(tsc => tsc.Subject)
                    .Include(teacher => teacher.Registry),
                out var totalTeachers
            );
        List<TeacherDto> responseTeachers = new List<TeacherDto>();
        foreach (var teacher in teachers)
        {
            responseTeachers.Add(new TeacherDto(teacher));
        }

        return Ok(
            new PaginationResponse<ClassroomDetails>
            {
                Total = totalStudents,
                Data = new ClassroomDetails
                {
                    name_classroom = classroom,
                    Students = responseStudents,
                    Teachers = responseTeachers
                }
            }
        );
    }

    #endregion

}
