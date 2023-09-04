using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core;
using backend.Interfaces;
using backend.Models;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubjectsController : Controller
{
    #region Attributes

    private readonly SchoolContext _context;
    private readonly IStudentRepository _studentRepository;
    
    #endregion

    #region Costructor

    public SubjectsController(SchoolContext context, IStudentRepository studentRepository)
    {
        _context = context;
        _studentRepository = studentRepository;
    }

    #endregion

    #region Api calls

    #region GetClassrooms

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult GetClassrooms([FromHeader] string Token)
    {
        #region Decode the token

        JwtSecurityToken decodedToken;
        try
        {
            //Decode the token
            decodedToken = JWT.DecodeJwtToken(Token, "DZq7JkJj+z0O8TNTvOnlmj3SpJqXKRW44Qj8SmsW8bk=");
        }
        catch (Exception e)
        {
            return Unauthorized("The token is not valid");
        }

        #endregion
        
        //prendo l'id dal token decodificato
        var takenId = new Guid (decodedToken.Payload["userid"].ToString());

        //prendo le materie che vengono insegnate al singolo studente
        var result = _studentRepository.GetStudentSubjects(takenId);
        return Ok(result);
    }

    #endregion

    #endregion
}