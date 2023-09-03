using AutoMapper;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class TeacherRepository : ITeacherRepository
{
    #region Attributes

    private readonly SchoolContext _context;
    private readonly IMapper _mapper;

    #endregion

    #region Costructor

    public TeacherRepository(SchoolContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    /// <summary> In this function i take All the Teachers including the User and Registry reference. </summary>
    /// <returns>Returns Teacher with his data contains User and Registry related to it</returns>
    public List<Teacher> GetTeachers()
    {
        var teachers =_context.Teachers
            .OrderBy(t => t.Id)
            .Include(t => t.Registry) // Include il registro associato
            .Include(t => t.User)
            .ToList();

        return teachers;
    }

    public Teacher GetTeacherById(Guid id)
    {
        var teacher = _context.Teachers.Where(s => s.Id == id)
            .Include(s => s.User)
            .Include(s => s.Registry)
            .FirstOrDefault();
        return teacher;
    }

    public List<Classroom> GetClassroomByTeacherId(Guid id)
    {
        var classrooms = _context.Teachers
            .Where(el => el.Id == id)
            .Include(el => el.TeacherSubjectsClassrooms)
            .ThenInclude(el => el.Classroom.Students)
            .SelectMany(el => el.TeacherSubjectsClassrooms
                .Select(c => c.Classroom))
            .ToList();
        return classrooms;
    }
    
    
    public int CountTeachers()
    {
        return _context.Teachers.Count();
    }

    public bool TeacherExists(Guid id)
    {
        return this._context.Teachers.Any(t => t.Id == id);
    }

    public bool CreateTeacher(Teacher teacher)
    {
        this._context.Teachers.Add(teacher);
        return Save();
    }

    public bool DeleteTeachers(Guid id)
    {
        var teacher = GetTeacherById(id);
        this._context.Teachers.Remove(teacher);
        return Save();
    }

    public bool UpdateTeacher(Teacher teacher)
    {
        _context.Teachers.Update(teacher);
        return Save();
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

    #endregion

}