using backend.Interfaces;
using backend.Models;

namespace backend.Repositories;

public class RegistryRepository : IRegistryRepository
{
    #region Attributes

    private readonly SchoolContext _context;

    #endregion

    #region Costructor

    public RegistryRepository(SchoolContext context)
    {
        this._context = context;
    }

    #endregion

    #region Methods

    /// <summary> Get all the Registrys </summary>
    public ICollection<Registry> GetRegistries()
    {
        return _context.Registries.OrderBy(r => r.Id).ToList();
    }

    /// <summary> Get one Registry having the id </summary>
    public Registry GetRegistryById(Guid id)
    {
        return this._context.Registries.Where(r => r.Id == id).FirstOrDefault();
    }

    /// <summary> Get all having the Registry </summary>
    public ICollection<Registry> GetRegistriesByName(string name)
    {
        return _context.Registries.Where(r => r.Name.Contains(name)).ToList();
    }

    /// <summary> check if id exist </summary>
    public bool RegistryExists(Guid id)
    {
        return this._context.Registries.Any(r => r.Id == id);
    }

    public bool CreateRegistry(Registry Registry)
    {
        _context.Registries.Add(Registry);
        return Save();
    }

    //save the changes on db
    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

    public bool DeleteRegistry(Guid id)
    {
        var registry = GetRegistryById(id);
        this._context.Registries.Remove(registry);
        return Save();
    }

    public bool UpdateRegistry(Registry Registry)
    {
        _context.Registries.Update(Registry);
        return Save();
    }

    // public IDictionary<string, List<Registry>> GetClassroom(string classroom)
    // {
    //     var studentRegistries = _context.Registries.Where(r => r.Student.Classroom.Trim().ToLower().Contains(classroom))
    //         .ToList();
    //     var teacherRegistries = _context.Registries.Where(r =>
    //         r.Teacher.TeacherSubjects.Any(ts => ts.Classroom.Trim().ToLower().Contains(classroom))).ToList();
    //     ///<summary>concats the two results in an unique List </summary>
    //     IDictionary<string, List<Registry>> result = new Dictionary<string, List<Registry>>
    //     {
    //         { "students", studentRegistries },
    //         { "teachers", teacherRegistries }
    //     };
    //     return result;
    // }

    #endregion
}