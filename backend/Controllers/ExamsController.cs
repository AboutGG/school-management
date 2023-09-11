using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core.Tokenizer;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamsController : Controller
{
    #region Attributes

    private readonly SchoolContext _context;
    private readonly ITransactionRepository _transactionRepository;

    #endregion

    #region Constructor

    public ExamsController(SchoolContext context,ITransactionRepository transactionRepository)
    {
        _context = context;
        _transactionRepository = transactionRepository;
    }
    
    #endregion

    #region Api calls

    #region Old
    
    // /// <summary> Having the token take the userId then takes the related exams </summary>
    // /// <param name="token"></param>
    // /// <returns>Return a list of Exams performed by the Student</returns>
    // [HttpGet]
    // [ProducesResponseType(200)]
    // [ProducesResponseType(401)]
    // public IActionResult GetExams([FromHeader] string token)
    // {
    //     
    //     try
    //     {
    //         //Decode the token
    //         JwtSecurityToken idFromToken = JWT.DecodeJwtToken(token);
    //         
    //         //Take the userId from the token
    //         var takenId = idFromToken.Payload["userid"].ToString();
    //         
    //         IGenericRepository<Student> userRepository = new GenericRepository<Student>(_context);
    //     
    //         //Take the student using the id
    //         Student takenStudent = userRepository.GetById(
    //             el => el.UserId.ToString() == takenId,
    //             el => el.StudentExams,
    //             el => el.Classroom
    //         );
    //         GenericRepository<Exam> examRepo = new GenericRepository<Exam>(_context);
    //         foreach (StudentExam iesim in takenStudent.StudentExams)
    //         {
    //             iesim.Exam = examRepo.GetById(el => el.Id == iesim.ExamId);
    //         }
    //         
    //         return Ok(takenStudent);
    //     }
    //     catch (Exception e)
    //     {
    //         return Unauthorized("The Token is not valid" );
    //     }
    // }

    #endregion

    #region Create Exam

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public IActionResult CreateExam([FromHeader] string Token, CreateExamDto InputExam)
    {
        JwtSecurityToken decodedToken;
        IDbContextTransaction transaction = _transactionRepository.BeginTransaction();
        Guid takenId;
        string role;
        try
        {
            //Decode the token
            decodedToken = JWTHandler.DecodeJwtToken(Token);
            takenId = new Guid(decodedToken.Payload["userid"].ToString());

            //Controllo il ruolo dello User tramite l'Id
            role = RoleSearcher.GetRole(takenId, _context);

            //Se lo user non è un professore creo una nuova eccezione restituendo Unauthorized
            if (role == "student" || role == "unknown")
                throw new Exception("UNAUTHORIZED");


            //Prendo l'id del professore tramite l'id utente ricavato dal token
            Guid teacherId = new GenericRepository<Teacher>(_context)
                .GetById2(
                    query => query
                        .Where(el => el.UserId == takenId)
                ).Id;

            //Prendo l'id di teacherSubjectClassroom in modo da poter procedere con la creazione dell'esame 
            Guid teacherSubjectClassroomId = new GenericRepository<TeacherSubjectClassroom>(_context)
                .GetById2(query => query
                    .Where(el => el.ClassroomId == InputExam.ClassroomId
                                 && el.SubjectId == InputExam.SubjectId
                                 && el.TeacherId == teacherId)).Id;
            
            //Creo l'esame che tramite i dati che passerà il FE
            Exam createdExam = new Exam
            {
                Id = new Guid(),
                TeacherSubjectClassroomId = teacherSubjectClassroomId,
                ExamDate = InputExam.ExamDate
            };

            //Nel caso non dovesse essere creato genererà un'eccezione
            if (!new GenericRepository<Exam>(_context).Create(createdExam))
            {
                throw new Exception("NOT_CREATED");
            }

            //Prendo la lista di studenti che appartengono alla classe nella quale il prof vuole svolgere l'esame
            List<Student> students = new GenericRepository<Student>(_context).GetAll2(
                null,
                query => query
                    .Where(el => el.ClassroomId == InputExam.ClassroomId)
            );
            
            //Creo record di StudentExam per tutti gli studenti della classe, inserendoli poi nel database
            foreach (var el in students)
            {
                var createdStudentExams = new StudentExam
                {
                    ExamId = createdExam.Id,
                    StudentId = el.Id
                };
                if (! new GenericRepository<StudentExam>(_context).Create(createdStudentExams))
                {
                    throw new Exception("NOT_CREATED");
                }
            }
            
            _transactionRepository.CommitTransaction(transaction);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception e)
        {
            _transactionRepository.RollbackTransaction(transaction);
            ErrorResponse error = ErrorManager.Error(e);
            return BadRequest(error);
        }
    }

    #endregion

    #endregion

}