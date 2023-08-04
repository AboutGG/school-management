using backend.Interfaces;
using backend.Models;

namespace backend.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly SchoolContext _context;
    
    public SubjectRepository(SchoolContext context)
    {
        this._context = context;
    }
    
    public ICollection<Subject> GetSubjects()
    {
        return this._context.Subjects.OrderBy(s => s.Id).ToList();
    }

    public bool CreateSubject(Subject subject)
    {
        this._context.Subjects.Add(subject);
        return Save();
    }
    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }
}