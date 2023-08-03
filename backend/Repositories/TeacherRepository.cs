using backend.Interfaces;
using backend.Models;

namespace backend.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly SchoolContext _context;

    public TeacherRepository(SchoolContext context)
    {
        _context = context;
    }
    
    public ICollection<Teacher> GetTeachers()
    {
        return this._context.Teachers.OrderBy(t => t.Id).ToList();
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
}