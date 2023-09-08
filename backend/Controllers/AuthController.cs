using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly SchoolContext _context;
    public AuthController(SchoolContext context ,IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _context = context;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserDto request)
    {
        if (_userRepository.UserExists(request.Username))
        {
            var user = _userRepository.GetUser(request.Username);
            if (_userRepository.CheckCredentials(request))
            {
                var token = JWTHandler.GenerateJwtToken(user);
                return Ok(new {token = token.Trim(), role = RoleSearcher.GetRole(user.Id, _context) });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized); // Utente non autenticato
            }
        }
        return BadRequest();
    }
    
    
}