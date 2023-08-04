using backend.Interfaces;
using backend.Models;

namespace backend.Repositories;

public class ExamRepository : IExamRepository
{
    private readonly SchoolContext _context;

    public ExamRepository(SchoolContext context)
    {
        this._context = context;
    }
    
    public ICollection<Exam> GetExams()
    {
        return this._context.Exams.OrderBy(e => e.Id).ToList();
    }

    public bool CreateExam(Exam exam)
    {
        this._context.Exams.Add(exam);
        return Save();
    }

    public bool DeleteExam(Guid id)
    {
        var exam = GetExamById(id);
        this._context.Exams.Remove(exam);
        return Save();
    }
    public Exam GetExamById(Guid id)
    {
        return this._context.Exams.Where(e => e.Id == id).FirstOrDefault();
    }

    //save the changes on db
    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }
}