using backend.Interfaces;
using backend.Models;

namespace backend.Repositories;

public class ClassroomRepository : IClassroomRepository
{
    #region Attributes

    private readonly SchoolContext _context;
    private IClassroomRepository _classroomRepositoryImplementation;

    #endregion

    #region Costructor

    public ClassroomRepository(SchoolContext context)
    {
        this._context = context;
    }

    #endregion
    
    public int GetClassroomsCount()
    {
       return _context.Classrooms
           .Select(s => s.Name)
           .Count();
    }

    public List<Classroom> GetClassrooms()
    {
        return _context.Classrooms
            .OrderBy(el => el.Name)
            .ToList();
    }
}