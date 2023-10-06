using System.Linq.Dynamic.Core;
using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage;
using Guid = System.Guid;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassroomsController : Controller
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly SchoolContext _context;
    private readonly IClassroomRepository _classroomRepository;
    private readonly ITeacherRepository _teacherRepository;

    #region Costructor

    public ClassroomsController(
        SchoolContext context,
        IClassroomRepository classroomRepository,
        ITeacherRepository teacherRepository, 
        ITransactionRepository transactionRepository)
    {
        _context = context;
        _classroomRepository = classroomRepository;
        _teacherRepository = teacherRepository;
        _transactionRepository = transactionRepository;
    }
    
#endregion

#region Api Calls

    #region GetClassrooms

    /// <summary> This API call are used to take all the classrooms when you create an Student </summary>
    /// <returns>All the classrooms present on the Database</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<ClassroomDto>))]
    public IActionResult GetClassroomsList()
    {
        var classrooms = new GenericRepository<Classroom>(_context)
            .GetAllUsingIQueryable(null, query => query, out var total);

        List<ClassroomDto> response = new List<ClassroomDto>();

        foreach (Classroom classroom in classrooms)
        {
            response.Add(new ClassroomDto(classroom));
        }

        return Ok(response);
    }

    #endregion

    #region GetClassroomDetails

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200, Type = typeof(PaginationResponse<ClassroomDetails>))]
    public IActionResult GetClassroomDetails([FromQuery] PaginationParams @params, [FromRoute] Guid id)
    {
        List<Student> takenStudents = new GenericRepository<Student>(_context)
            .GetAllUsingIQueryable(@params,
                query => query
                    .Where(el => el.ClassroomId == id)
                    .Include(el => el.Registry)
                    .Include(el => el.Classroom)
                , out var totalStudents
            );

        List<StudentDto> responseStudents = new List<StudentDto>();
        foreach (Student takenStudent in takenStudents)
        {
            responseStudents.Add(new StudentDto(takenStudent));
        }

        var classroom = new GenericRepository<Classroom>(_context)
            .GetByIdUsingIQueryable(el => el.Where(classroom => classroom.Id == id)).Name;

        var teachers = new GenericRepository<Teacher>(_context)
            .GetAllUsingIQueryable(
                null,
                query => query
                    .Where(teacher => teacher.TeachersSubjectsClassrooms
                        .Any(tsc => tsc.ClassroomId == id))
                    .Include(teacher => teacher.TeachersSubjectsClassrooms)
                    .ThenInclude(tsc => tsc.Subject)
                    .Include(teacher => teacher.Registry),
                out var totalTeachers
            );
        List<TeacherDto> responseTeachers = new List<TeacherDto>();
        foreach (var teacher in teachers)
        {
            responseTeachers.Add(new TeacherDto(teacher));
        }

        return Ok(
            new PaginationResponse<ClassroomDetails>
            {
                Total = totalStudents,
                Data = new ClassroomDetails
                {
                    name_classroom = classroom,
                    Students = responseStudents,
                    Teachers = responseTeachers
                }
            }
        );
    }

    #endregion

    #region Student promotion

    [HttpPost]
    [Route("{classroomId}/students/{studentId}/graduations")]
    [ProducesResponseType(200, Type = typeof(PaginationResponse<StudentDto>))]
    public IActionResult StudentPromotion([FromBody] InputStudentPromotionDto inputStudentPromotion, [FromRoute] Guid classroomId, [FromRoute] Guid studentId)
    {
        IDbContextTransaction transaction = _transactionRepository.BeginTransaction();
        try
        {
            var splittedScholasticYear = inputStudentPromotion.SchoolYear.Split("-");
            
            //Controllo se l'anno scolastico è valido
            if (int.Parse(splittedScholasticYear[0]) +1 != int.Parse( splittedScholasticYear[1]))
            {
                throw new Exception("INVALID_SCHOOL_YEAR");
            }
            
            //Controllo della data in modo da controllare se si è nel secondo quadrimestre e quindi si può procedere con la promozione
            if (DateTime.UtcNow < new DateTime(int.Parse(splittedScholasticYear[1]), 06, 10))
            {
                throw new Exception("UNAUTHORIZED_STUDENT_PROMOTION");
            }
            
            //Prendo lo studente tramite l'id passato nella route
            Student takenStudent = new GenericRepository<Student>(_context)
                .GetByIdUsingIQueryable(query => query
                    .Where(el => el.Id == studentId)
                    .Include(el => el.StudentExams.Where(el =>
                        el.Exam.Date.Year == int.Parse(splittedScholasticYear[0])
                        || el.Exam.Date.Year == int.Parse(splittedScholasticYear[1])))
                    .ThenInclude(el => el.Exam)
                    .Include(el => el.Registry)
                );

            //Calcolo la media di tutti i voti dello studente presi negli esami che ha svolto
            double finalGraduation = 0;
            
            foreach (var el in takenStudent.StudentExams)
            {
                finalGraduation += el.Grade ?? 0;
            }

            finalGraduation /= takenStudent.StudentExams.Count;

            if (inputStudentPromotion.Promoted && finalGraduation < 6)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"The student {takenStudent.Registry.Name} {takenStudent.Registry.Surname} doesn't respect the parameters to be promoted");
            }
            
            //Creo una nuova instanza di PromotionHistory
            PromotionHistory promotionHistory = new PromotionHistory()
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                PreviousClassroomId = takenStudent.ClassroomId,
                PreviousSchoolYear = inputStudentPromotion.SchoolYear,
                FinalGraduation = Convert.ToInt32(finalGraduation),
                ScholasticBehavior = inputStudentPromotion.ScholasticBehavior,
                Promoted = inputStudentPromotion.Promoted
            };
            takenStudent.ClassroomId = inputStudentPromotion.NextClassroom;

            if (! new GenericRepository<PromotionHistory>(_context).Create(promotionHistory))
            {
                throw new Exception("NOT_CREATED");
            }

            if (! new GenericRepository<Student>(_context).UpdateEntity(takenStudent))
            {
                throw new Exception("NOT_UPDATED");
            }

            PromotionHistoryDto response = new PromotionHistoryDto(promotionHistory);
            
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception e)
        {
            _transactionRepository.RollbackTransaction(transaction);
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }

    #endregion
    
    #endregion

}
