using AutoMapper;
using backend.Dto;
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
    public ICollection<TeacherDto> GetTeachers()
    {
        var teachers = _mapper.Map<List<TeacherDto>>(_context.Teachers
            .OrderBy(t => t.Id)
            .Include(t => t.Registry) // Include il registro associato
            .Include(t => t.User)
            .ToList());

        return teachers;
    }

    public Teacher GetTeacherById(Guid id)
    {
        return this._context.Teachers.Where(t => t.Id == id).FirstOrDefault();
    }

    public bool TeacherExists(Guid id)
    {
        return this._context.Users.Any(t => t.Id == id);
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