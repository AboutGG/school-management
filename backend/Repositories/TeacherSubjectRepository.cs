using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class TeacherSubjectRepository : ITeacherSubjectRepository
{
    private readonly SchoolContext _context;

    public TeacherSubjectRepository(SchoolContext context)
    {
        this._context = context;
    }
    
    public ICollection<TeacherSubject> GetTeachersSubjects()
    {
        return _context.TeacherSubjects.OrderBy(ts => ts.TeacherId)
            .Include(ts => ts.Teacher)
                .ThenInclude(t => t.Registry)
            .Include(ts => ts.Subject)
            .ToList();
    }

    public bool CreateTeacherSubject(TeacherSubject teacherSubject)
    {
        this._context.TeacherSubjects.Add(teacherSubject);
        return Save();
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }
}