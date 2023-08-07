using backend.Dto;
using backend.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        this._userRepository = userRepository;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserDto request)
    {
        if (_userRepository.UserExists(request.Username))
        {
            var user = _userRepository.GetUser(request.Username);
            if (_userRepository.CheckCredentials(request))
            {
                var token = JWT.GenerateJwtToken(user);
                return Ok(new { access_token = token });
            }
            else
            {
                return Unauthorized(); // Utente non autenticato
            }
        }
        return BadRequest();
    }
    
    
}