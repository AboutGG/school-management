using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Utils;

public static class RoleSearcher
{
    public static string GetRole(Guid userId,SchoolContext _context)
    {
        IGenericRepository<UserRole> usersRepository = new GenericRepository<UserRole>(_context);
        UserRole takenUser = usersRepository.GetByIdUsingIQueryable(query => query
            .Where(el => el.UserId == userId)
            .Include(el => el.Role));
        string role = takenUser.Role.Name;
        return role;
    }
}