using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core;
using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : Controller
{
    #region Attributes

    private readonly SchoolContext _context;
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    #endregion

    #region Costructor

    public StudentsController(IStudentRepository studentRepository, SchoolContext context, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _context = context;
        _mapper = mapper;
    }

    #endregion

    #region API calls

    #region Get students

    /// <summary> Get All Teachers with its Registry and user </summary>
    /// <returns>ICollection<TeacherDto></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<Student>))]
    public IActionResult GetStudents()
    {
        return Ok(_mapper.Map<List<StudentDto>>(_studentRepository.GetStudents()));
    }

    #endregion

    #region Get Subjects

    [HttpGet]
    [Route("subjects")]
    [ProducesResponseType(200, Type = typeof(List<TeacherSubjectClassroomDto>))]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult GetSubjects([FromQuery] PaginationParams @params, [FromHeader] string Token)
    {
        #region Decode the token

        JwtSecurityToken decodedToken;
        Guid takenId;
        string role;
        try
        {
            //Decode the token
            decodedToken = JWTHandler.DecodeJwtToken(Token);
            takenId = new Guid(decodedToken.Payload["userid"].ToString());

            //tramite lo user ricavo il ruolo tramite l'Id
            role = RoleSearcher.GetRole(takenId, _context);

            //nel caso non dovesse essere un alunno esegue una nuova exception ritornando Unauthorized
            if (role == "teacher" || role == "unknown")
            {
                throw new Exception("NOT_FOUND");
            }
            else
            {
                //Prendo l'id della classe riguardante lo studente 
                Guid studentclassroomId = _context.Students.FirstOrDefault(el => el.UserId == takenId).ClassroomId;

                switch (@params.Order.Trim().ToLower())
                {
                    case "name":
                        @params.Order = "Teacher.Registry.Name";
                        break;
                    case "surname":
                        @params.Order = "Teacher.Registry.Surname";
                        break;
                }
                
                //Prendo le materie che pratica lo studente nella sua classe
                var resultStudent = new GenericRepository<TeacherSubjectClassroom>(_context).GetAllUsingIQueryable(
                    @params,
                    query => query
                        .Where(el => el.ClassroomId == studentclassroomId)
                        .Include(el => el.Classroom)
                        .Include(el => el.Teacher.Registry)
                        .Include(el => el.Subject));
                
                return Ok(_mapper.Map<List<TeacherSubjectClassroomDto>>(resultStudent.DistinctBy(el => el.TeacherId)));
            }
        }
        catch (Exception e)
        {
            ErrorResponse error = ErrorManager.Error(e);
            return BadRequest(error);
        }

        #endregion
    }

    #endregion
    
    #region Get Exams
    /// <summary> Having the token take the userId then takes the related exams </summary>
    /// <param name="token"></param>
    /// <returns>Return a list of Exams performed by the Student</returns>
    [HttpGet]
    [Route("exams")]
    [ProducesResponseType(200, Type = typeof(StudentExamDto))]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetStudentExams([FromHeader] string token)
    {
        try
        {
            //Decode the token
            JwtSecurityToken idFromToken = JWTHandler.DecodeJwtToken(token);
            
            //Take the userId from the token
            var takenId = new Guid (idFromToken.Payload["userid"].ToString());
            
            //Take the student using the id
            Student takenStudent = new GenericRepository<Student>(_context)
                .GetByIdUsingIQueryable(
                    query => query
                        .Where(el => el.UserId == takenId)
                        .Include(el=> el.Registry)
                        .Include(el => el.StudentExams)
                        .ThenInclude(el => el.Exam)
                        .ThenInclude(el => el.TeacherSubjectClassroom.Subject)
                        .Include(el => el.StudentExams)
                        .ThenInclude(el => el.Exam.TeacherSubjectClassroom.Teacher.Registry)
                    );
            if (takenStudent == null)
            {
                throw new Exception("NOT_FOUND");
            }

            string role = RoleSearcher.GetRole(takenId, _context);

            if (role == "teacher" || role == "unknown")
                throw new Exception("UNAUTHORIZED");
            // var dummyExams = new GenericRepository<StudentExam>(_context).GetAll2(null, 
            //     el => 
            //         takenStudent.StudentExams.AsQueryable()
            //             .Include(el => el.Student)
            //             .Include(el => el.Exam)
            //             .Include(el => el.Exam.TeacherSubjectClassroom)
            //             .Include(el => el.Exam.TeacherSubjectClassroom.Subject)
            //             .Include(el => el.Exam.TeacherSubjectClassroom.Teacher)
            //         );
            //
            // takenStudent.StudentExams = dummyExams;
            
            var dummy = _mapper.Map<StudentExamDto>(takenStudent);
            return Ok(dummy);
        }
        catch (Exception e)
        {
            return BadRequest(ErrorManager.Error(e));
        }
    }

    #endregion

    #endregion
}