using backend.Dto;
using backend.Interfaces;
using backend.Models;

namespace backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SchoolContext _context;

    public UserRepository(SchoolContext context)
    {
        _context = context;
    }
    
    public ICollection<User> GetUsers()
    {
        return this._context.Users.OrderBy(u => u.Id).ToList();
    }

    public User GetUser(Guid id)
    {
        return this._context.Users.Where(u => u.Id == id).FirstOrDefault();
    }

    public User GetUser(string username)
    {
        return this._context.Users.Where(u => u.Username.Trim().ToLower() == username.Trim().ToLower()).FirstOrDefault();
    }

    public bool CheckCredentials(UserDto user)
    {
        return this._context.Users.Any(u => u.Username.Trim().ToLower() == user.Username.Trim().ToLower() && u.Password == user.Password);
    }

    public bool UserExists(Guid id)
    {
        return this._context.Users.Any(u => u.Id == id);
    }

    public bool UserExists(string username)
    {
        return this._context.Users.Any(u=> u.Username.Trim().ToLower() == username.Trim().ToLower());
    }

    public bool CreateUser(User user)
    {
        this._context.Users.Add(user);
        return Save();
    }

    public bool DeleteUser(Guid id)
    {
        var user = GetUser(id);
        this._context.Users.Remove(user);
        return Save();
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

    public bool UpdateUser(User user)
    {
        _context.Users.Update(user);
        return Save();
    }
    
}