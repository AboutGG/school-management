using System.IdentityModel.Tokens.Jwt;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExamsController : Controller
{
    #region Attributes

    private readonly SchoolContext _context;

    #endregion

    #region Constructor

    public ExamsController(SchoolContext context)
        {
            _context = context;
        }

    #endregion
    
    #region Api calls
    /// <summary> Having the token take the userId then takes the related exams </summary>
    /// <param name="token"></param>
    /// <returns>Return a list of Exams performed by the Student</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    public IActionResult GetExams([FromHeader] string token)
    {
        
        try
        {
            //Decode the token
            JwtSecurityToken idFromToken = JWT.DecodeJwtToken(token);
            
            //Take the userId from the token
            var takenId = idFromToken.Payload["userid"].ToString();
            
            IGenericRepository<Student> userRepository = new GenericRepository<Student>(_context);
        
            //Take the student using the id
            Student takenStudent = userRepository.GetById(
                el => el.UserId.ToString() == takenId,
                el => el.StudentExams,
                el => el.Classroom
            );
            GenericRepository<Exam> examRepo = new GenericRepository<Exam>(_context);
            foreach (StudentExam iesim in takenStudent.StudentExams)
            {
                iesim.Exam = examRepo.GetById(el => el.Id == iesim.ExamId, el => el.Subject);
            }
            
            return Ok(takenStudent);
        }
        catch (Exception e)
        {
            return Unauthorized("The Token is not valid" );
        }
    }

    #endregion
    
}