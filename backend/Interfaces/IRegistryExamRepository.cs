using backend.Models;

namespace backend.Interfaces;

public interface IRegistryExamRepository
{
    /// <summary> Return all registryExams </summary>
    ICollection<StudentExam> GetRegistriesExams();

    /// <summary> Create a registryExam </summary>
    /// <param name="studentExam"></param>
    /// <returns>true successful, false not successful</returns>
    bool CreateRegistryExam(StudentExam studentExam);
    bool Save();
}