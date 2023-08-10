using backend.Interfaces;
using backend.Models;

namespace backend.Repositories;

public class ClassroomRepository : IClassroomRepository
{
    #region Attributes

    private readonly SchoolContext _context;

    #endregion

    #region Costructor

    public ClassroomRepository(SchoolContext context)
    {
        this._context = context;
    }

    #endregion


    public int GetClassrooms()
    {
       return _context.Students
           .Select(s => s.Classroom)
           .Distinct()
           .Count();
       
    }
}