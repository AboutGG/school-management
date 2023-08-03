using backend.Models;

namespace backend.Interfaces;

public interface ISubjectRepository
{
    /// <summary> Return all subject </summary>
    ICollection<Subject> GetSubjects();

    /// <summary> Create a subject </summary>
    /// <param name="subject"></param>
    /// <returns>true successful, false not successful</returns>
    bool CreateSubject(Subject subject);
    bool Save();
}