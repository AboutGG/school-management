using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class StudentRepository : IStudentRepository
{
    #region Attributes

    private readonly SchoolContext _context;
    private readonly IMapper _mapper;

    #endregion

    #region Costructor

    public StudentRepository(SchoolContext context, IMapper mapper)
    {
        this._context = context;
        _mapper = mapper;
    }

    #endregion

    #region Attributes

    /// <summary> In this function i take All the Student including the User and Registry reference. </summary>
    /// <returns>Returns Student with his data contains User and Registry related to it</returns>
    public ICollection<StudentDto> GetStudents()
    {
        var student = _mapper.Map<List<StudentDto>>(_context.Students
            .OrderBy(s => s.Id)
            .Include(t => t.User) // Include il registro associato
            .Include(t => t.Registry)
            .ToList());

        return student;
    }

    public Student GetStudentById(Guid id)
    {
        return _context.Students.Where(s => s.Id == id).FirstOrDefault();
    }

    public bool StudentExist(Guid id)
    {
        return _context.Students.Any(s => s.Id == id);
    }

    public bool CreateStudent(Student student)
    {
        _context.Students.Add(student);
        return Save();
    }

    public bool DeleteStudent(Guid id)
    {
        var student = GetStudentById(id);
        _context.Students.Remove(student);
        return Save();
    }

    //save the changes on db
    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

    #endregion

}