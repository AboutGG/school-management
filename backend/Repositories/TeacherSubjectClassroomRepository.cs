using System.Linq.Expressions;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class TeacherSubjectClassroomRepository : ITeacherSubjectClassroomRepository
{
    private readonly SchoolContext _context;

    public TeacherSubjectClassroomRepository(SchoolContext context)
    {
        this._context = context;
    }

    public ICollection<TeacherSubjectClassroom> GetTeachersSubjects()
    {
        throw new NotImplementedException();
    }

    public ICollection<TeacherSubjectClassroom> GetTeachersSubjects(params Expression<Func<TeacherSubjectClassroom, object>>[] includeExpression)
    {
        IQueryable<TeacherSubjectClassroom> query = _context.TeachersSubjectsClassrooms.OrderBy(ts => ts.TeacherId);
        if (includeExpression != null)
        {
            foreach (var incclude in includeExpression)
            {
                query = query.Include(incclude); }
        }

        return query.ToList();
    }

    public bool CreateTeacherSubject(TeacherSubjectClassroom teacherSubjectClassroom)
    {
        this._context.TeachersSubjectsClassrooms.Add(teacherSubjectClassroom);
        return Save();
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }
}