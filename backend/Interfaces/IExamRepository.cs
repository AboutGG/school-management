using backend.Models;

namespace backend.Interfaces;

public interface IExamRepository
{
    /// <summary> Rturn all teacherSubject </summary>
    ICollection<Exam> GetExams();

    /// <summary> Create a user </summary>
    /// <param name="exam"></param>
    /// <returns>true successful, false not successful</returns>
    bool CreateExam(Exam exam);
    bool Save();
}