using backend.Dto;
using backend.Models;
namespace backend.Interfaces;

public interface IUserRepository
{
    ICollection<User> GetUsers();

    User GetUser(Guid id);
    
    User GetUser(string username);

    int CountUsers();
        
    /// <summary> UserExists check using userId </summary>
    /// <param name="userId"></param>
    /// <returns>true if exist , false if not</returns>
    bool UserExists(Guid id);

    /// <summary> UserExists check using username </summary>
    /// <param name="username"></param>
    /// <returns>true if exist , false if not</returns>
    bool UserExists(string username);

    bool CheckCredentials(UserDto user);
    
    /// <summary> Create a user </summary>
    /// <param name="user"></param>
    /// <returns>true successful, false not successful</returns>
    bool CreateUser(User user);

    bool DeleteUser (Guid id);
    bool Save();

    /// <summary> UpdateUser </summary>
    /// <param name="user"></param>
    /// <returns>if the user is updated</returns>
    bool UpdateUser(User user);
    Me GetMe(string token);

}