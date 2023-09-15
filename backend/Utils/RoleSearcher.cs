using backend.Interfaces;
using backend.Models;
using backend.Repositories;

namespace backend.Utils;

public static class RoleSearcher
{
    public static string GetRole(Guid userId,SchoolContext _context)
    {
        IGenericRepository<User> usersRepository = new GenericRepository<User>(_context);
        User takenUser = usersRepository.GetById(el => el.Id == userId, el => el.Teacher, el => el.Student);
        string role = takenUser.Student != null ? "student" : takenUser.Teacher != null ? "teacher" : "unknown";
        return role;
    }
}