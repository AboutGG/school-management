using backend.Interfaces;
using backend.Models;

namespace backend.Repositories;

public class RegistryExamRepository : IRegistryExamRepository
{
    private readonly SchoolContext _context;

    public RegistryExamRepository(SchoolContext context)
    {
        this._context = context;
    }
    
    public ICollection<StudentExam> GetRegistriesExams()
    {
        return this._context.RegistryExams.OrderBy(re => re.ExamId).ToList();
    }

    public bool CreateRegistryExam(StudentExam studentExam)
    {
        this._context.RegistryExams.Add(studentExam);
        return Save();
    }
    
    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }
}