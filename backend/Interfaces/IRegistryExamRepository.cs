using backend.Models;

namespace backend.Interfaces;

public interface IRegistryExamRepository
{
    /// <summary> Rturn all registryExams </summary>
    ICollection<RegistryExam> GetRegistriesExams();

    /// <summary> Create a registryExam </summary>
    /// <param name="registryExam"></param>
    /// <returns>true successful, false not successful</returns>
    bool CreateRegistryExam(RegistryExam registryExam);
    bool Save();
}