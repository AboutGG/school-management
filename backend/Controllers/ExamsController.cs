using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Tokenizer;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    /// <summary> Api call which plan an Exam </summary>
    /// <param name="Token">token to autenticate the role</param>
    /// <param name="InputExam">The exam to add on the Db</param>
    /// <returns>201 if the exams is created, 400 if not</returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public IActionResult CreateExam([FromHeader] string Token, ExamResponseDto InputExam)
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
                .GetByIdUsingIQueryable(
                    query => query
                        .Where(el => el.UserId == takenId)
                ).Id;

            //Prendo l'id di teacherSubjectClassroom in modo da poter procedere con la creazione dell'esame 
            Guid teacherSubjectClassroomId = new GenericRepository<TeacherSubjectClassroom>(_context)
                .GetByIdUsingIQueryable(query => query
                    .Where(el => el.ClassroomId == InputExam.Classroom.Id
                                 && el.SubjectId == InputExam.Subject.Id
                                 && el.TeacherId == teacherId)).Id;
            
            //Creo l'esame che tramite i dati che passerà il FE
            Exam createdExam = new Exam
            {
                Id = new Guid(),
                TeacherSubjectClassroomId = teacherSubjectClassroomId,
                Date = InputExam.Date
            };

            //Nel caso non dovesse essere creato genererà un'eccezione
            if (!new GenericRepository<Exam>(_context).Create(createdExam))
            {
                throw new Exception("NOT_CREATED");
            }

            //Prendo la lista di studenti che appartengono alla classe nella quale il prof vuole svolgere l'esame
            List<Student> students = new GenericRepository<Student>(_context).GetAllUsingIQueryable(
                null,
                query => query
                    .Where(el => el.ClassroomId == InputExam.Classroom.Id),
                out var total
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
            return StatusCode(StatusCodes.Status201Created, createdExam);
        }
        catch (Exception e)
        {
            _transactionRepository.RollbackTransaction(transaction);
            ErrorResponse error = ErrorManager.Error(e);
            return BadRequest(error);
        }
    }

    #endregion

    #region Put Exam
    /// <summary> API call which update the grade taken on a Exam </summary>
    /// <param name="Token">Token di autorizzazione</param>
    /// <param name="InputStudentExam">Dto con lo scopo di passare il voto che deve ricevere lo studente in un determinato esame</param>
    /// <param name="id">Students id</param>
    /// <returns>Un messaggio se si modifica con successo il voto.</returns>
    /// <exception cref="Exception">Gestione degli errori nei casi in cui chi effettua la chiamata non è un professore o nel caso in cui non venga modificato</exception>
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult PutExam([FromHeader] string Token, [FromBody] InputStudentExamDto InputStudentExam, Guid id)
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
            
            //Tramite l'id che passa il FE prendo lo studente con quell'id e a sua volta l'instanza dell'esame tramite l'oggetto in Input dal FE
            var takenStudentExam = new GenericRepository<Student>(_context)
                .GetByIdUsingIQueryable(query => query
                        .Where(el => el.Id == id)
                        .Include(s => s.StudentExams) // Include StudentExams navigation property
                ).StudentExams
                .FirstOrDefault(src => src.StudentId == id && src.ExamId == InputStudentExam.ExamId);
            
            //Modifico il voto 
            takenStudentExam.Grade = InputStudentExam.Grade;
            
            if (! new GenericRepository<StudentExam>(_context).UpdateEntity(takenStudentExam))
            {
                throw new Exception("NOT_UPDATED");
            }
            
            _transactionRepository.CommitTransaction(transaction);
            return StatusCode(StatusCodes.Status200OK, "Grade successfully updated.");
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