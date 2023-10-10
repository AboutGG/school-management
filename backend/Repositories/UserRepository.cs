using System.IdentityModel.Tokens.Jwt;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SchoolContext _context;
    private readonly IGenericRepository<User> _userGenericRepository;

    public UserRepository(SchoolContext context, IGenericRepository<User> userGenericRepository)
    {
        _userGenericRepository = userGenericRepository;
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
        return this._context.Users.Where(u => u.Username.Trim().ToLower() == username.Trim().ToLower())
            .Include(el => el.Student)
            .Include(el => el.Teacher)
            .FirstOrDefault();
    }

    public int CountUsers()
    {
        return _context.Users.Count();
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

    public Me GetMe(string token)
    {
        var userid = Guid.Parse(JWTHandler.DecodeJwtToken(token).Payload["userid"].ToString());
        var user = _userGenericRepository.GetByIdUsingIQueryable(
            query => query
                .Where(el => el.Id == userid)
                .Include(u => u.Student)
                .ThenInclude(s => s.Registry)
                .Include(u => u.Teacher)
                .ThenInclude(t => t.Registry));
        Me userme = new Me(user);
        return userme;
    }
}