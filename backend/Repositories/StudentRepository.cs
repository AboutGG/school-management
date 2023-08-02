using backend.Interfaces;
using backend.Models;

namespace backend.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly SchoolContext _context;

    public StudentRepository(SchoolContext context)
    {
        this._context = context;
    }
    
    public ICollection<Student> GetStudents()
    {
        return _context.Students.OrderBy(s => s.Id).ToList();
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
}