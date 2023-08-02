using backend.Models;

namespace backend.Interfaces;

public interface ISubjectRepository
{
    /// <summary> Return all subject </summary>
    ICollection<Subject> GetExams();

    /// <summary> Create a subject </summary>
    /// <param name="subject"></param>
    /// <returns>true successful, false not successful</returns>
    bool CreateExam(Subject subject);
    bool Save();
}