using backend.Models;

namespace backend.Interfaces;

public interface IRegistryRepository
{
    ICollection<Registry> GetRegistries();

    ICollection<Registry> GetRegistriesByName(string name);

    /// <summary> RegistryExists check using Registryname </summary>
    /// <param name="name"></param>
    /// <returns>true if exist , false if not</returns>
    //ICollection<Registry> GetRegistriesByName(string name);

    Registry GetRegistryById(Guid id);

    /// <summary> RegistryExists check using RegistryId </summary>
    /// <param name="RegistryId"></param>
    /// <returns>true if exist , false if not</returns>
    bool RegistryExists(Guid id);

    /// <summary> Create a Registry </summary>
    /// <param name="Registry"></param>
    /// <returns>true successful, false not successful</returns>
    bool CreateRegistry(Registry Registry);

    bool DeleteRegistry(Guid id);
    bool Save();

    /// <summary> UpdateRegistry </summary>
    /// <param name="Registry"></param>
    /// <returns>if the Registry is updated</returns>
    bool UpdateRegistry(Registry Registry);

    #region obsolete
    /// <summary> Filter by classroom </summary>
    /// <param name="classroom"></param>
    /// <returns> A list of object taken and filtered, from Students and Teachers, by the classroom</returns>
    //IDictionary<string, List<Registry>> GetClassroom(string classroom);
    
    #endregion
}