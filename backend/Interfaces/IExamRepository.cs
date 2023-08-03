using backend.Models;

namespace backend.Interfaces;

public interface IExamRepository
{
    /// <summary> Return all teacherSubject </summary>
    ICollection<Exam> GetExams();

    /// <summary> Create a user </summary>
    /// <param name="exam"></param>
    /// <returns>true successful, false not successful</returns>
    Exam GetExamById(Guid id);
    bool CreateExam(Exam exam);
    bool DeleteExam(Guid id);
    bool Save();
}