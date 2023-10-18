using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
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

    #endregion

    #region Costructor

    public StudentsController(IStudentRepository studentRepository, SchoolContext context)
    {
        _studentRepository = studentRepository;
        _context = context;
    }

    #endregion

    #region API calls

    #region Get students

    /// <summary> Get All Teachers with its Registry and user </summary>
    /// <returns>ICollection<TeacherDto></returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<StudentDto>))]
    public IActionResult GetStudents()
    {
        var takenStudents = _studentRepository.GetStudents().ToList();

        List<StudentDto> responseStudents = new List<StudentDto>();

        foreach (Student takenStudent in takenStudents)
        {
            responseStudents.Add(new StudentDto(takenStudent));
        }
        
        return Ok(responseStudents);
    }

    #endregion

    #region Get Subjects

    [HttpGet]
    [Route("subjects")]
    [ProducesResponseType(200, Type = typeof(List<TeacherDto>))]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public IActionResult GetSubjects([FromQuery] PaginationParams @params, [FromHeader] string Token)
    {
        JwtSecurityToken decodedToken;
        Guid takenId;
        string role;
        try
        {
            //Decode the token
            decodedToken = JWTHandler.DecodeJwtToken(Token);
            takenId = new Guid(decodedToken.Payload["userid"].ToString());

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
            var resultTeachers = new GenericRepository<TeacherSubjectClassroom>(_context).GetAllUsingIQueryable(
                @params,
                query => query
                    .Where(el => el.ClassroomId == studentclassroomId
                    && ( el.Teacher.Registry.Name.Trim().ToLower().Contains(@params.Search.Trim().ToLower())
                         || el.Teacher.Registry.Surname.Trim().ToLower().Contains(@params.Search.Trim().ToLower())
                        || el.Subject.Name.Trim().ToLower().Contains(@params.Search.Trim().ToLower()))
                    )
                    .Include(el => el.Classroom)
                    .Include(el => el.Teacher.Registry)
                    .Include(el => el.Subject),
                out var total
            ).Select(el => el.Teacher).DistinctBy(el => el.Id).ToList();

            List<TeacherDto> mappedResponse = new List<TeacherDto>();

            foreach (Teacher resultTeacher in resultTeachers)
            {
                mappedResponse.Add(new TeacherDto(resultTeacher));
            }

            return Ok(
                new PaginationResponse<TeacherDto>
                {
                    Total = total,
                    Data = mappedResponse
                });
        }
        catch (Exception e)
        {
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }

    #endregion

    #region Get Exams

    /// <summary> Having the token take the userId then takes the related exams </summary>
    /// <param name="token"></param>
    /// <returns>Return a list of Exams performed by the Student</returns>
    [HttpGet]
    [Route("exams")]
    [ProducesResponseType(200, Type = typeof(PaginationResponse<StudentExamDto>))]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetStudentExams([FromHeader] string Token, [FromQuery] PaginationParams @params)
    {
        try
        {
            //Decode the token
            JwtSecurityToken idFromToken = JWTHandler.DecodeJwtToken(Token);

            //Take the userId from the token
            var takenId = new Guid(idFromToken.Payload["userid"].ToString());

            @params.Order = "Exam." + @params.Order;

            //Take the student using the id
            List<StudentExam> takenStudent = new GenericRepository<StudentExam>(_context)
                .GetAllUsingIQueryable(@params,
                    query => query
                        .Where(el => el.Student.UserId == takenId)
                        .Include(el => el.Student)
                        .Include(el => el.Exam)
                        .ThenInclude(el => el.TeacherSubjectClassroom.Teacher.Registry)
                        .Include(el => el.Student.Classroom)
                        .Include(el => el.Exam.TeacherSubjectClassroom)
                        .ThenInclude(el => el.Subject)
                    , out var total
                );
            if (takenStudent == null)
            {
                throw new Exception("NOT_FOUND");
            }

            if (@params.Filter != null)
                takenStudent = takenStudent.Where(el =>
                        el.Exam.TeacherSubjectClassroom.Subject.Name.Trim().ToLower() ==
                        @params.Filter.Trim().ToLower())
                    .ToList();

            var studentExamDtos = new List<StudentExamDto>();

            foreach (var el in takenStudent)
            {
                studentExamDtos.Add(new StudentExamDto(el));
            }

            return Ok(new PaginationResponse<StudentExamDto>
            {
                Total = total,
                Data = studentExamDtos
            });
        }
        catch (Exception e)
        {
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }

    #endregion

    #region Student Report

    /// <summary> Api call which passing the UserId return the student's report </summary>
    /// <param name="Id">UserId to take the student's instance</param>
    /// <param name="firstQuarter">is a flag to indicate the quarter which we want to see for the report</param>
    /// <returns>Return a PDF file with the student's report</returns>
    [HttpGet]
    [Route("{Id}/Reports")]
    [ProducesResponseType(200)]
    public IActionResult GetStudentReport([FromRoute] Guid Id, [FromQuery] bool firstQuarter, [FromQuery] string schoolYear)
    {
        DateOnly startFirstQuarter;
        DateOnly endFirstQuarter;
        DateOnly startSecondQuarter;
        DateOnly endSecondQuarter;
        
        try
        {
            var splittedSchoolYear = schoolYear.Split('-');

            if (Convert.ToInt32(splittedSchoolYear[0]) +1 != Convert.ToInt32(splittedSchoolYear[1]))
            {
                throw new Exception("INVALID_SCHOOL_YEAR");
            }
            
            startFirstQuarter = new DateOnly(int.Parse(splittedSchoolYear[0]), 09, 15);
            endFirstQuarter = new DateOnly(int.Parse(splittedSchoolYear[1]), 01, 31);
            startSecondQuarter = new DateOnly(int.Parse(splittedSchoolYear[1]), 02, 1);
            endSecondQuarter = new DateOnly(int.Parse(splittedSchoolYear[1]), 05, 15);
        
            if(DateTime.UtcNow.Year == int.Parse(splittedSchoolYear[1]) || DateTime.UtcNow.Year == int.Parse(splittedSchoolYear[0]))
            { 
                if ((firstQuarter && DateOnly.FromDateTime(DateTime.UtcNow) < endFirstQuarter) ||
                    (!firstQuarter &&  DateOnly.FromDateTime(DateTime.UtcNow) < endSecondQuarter))
                {
                    throw new Exception("UNAUTHORIZED_QUARTER_REPORT");
                }
            } else if (DateTime.UtcNow.Year < int.Parse(splittedSchoolYear[1]))
            {
                throw new Exception("INVALID_SCHOOL_YEAR");
            }

        }
        catch (Exception e)
        {
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
        

        //Ottengo lo studente tramite l'id che viene passato dalla route
        Student takenStudent = new GenericRepository<Student>(_context).GetByIdUsingIQueryable(query => query
            .Where(el => el.UserId == Id)
            .Include(el => el.Registry)
            .Include(el => el.StudentExams)
            .ThenInclude(el => el.Exam)
            .ThenInclude(el => el.TeacherSubjectClassroom.Subject)
        );

        //Prendo le materie insegnate nella classe dello studente
        List<Subject> subjectClassrooms = new GenericRepository<Classroom>(_context).GetByIdUsingIQueryable(
            query => query
                .Where(el => el.Id == takenStudent.ClassroomId)
                .Include(el => el.TeacherSubjectsClassrooms)
                .ThenInclude(el => el.Subject)
        ).TeacherSubjectsClassrooms.Select(el => new Subject { Id = el.Subject.Id, Name = el.Subject.Name }).ToList();

        List<SubjectGrade> report = new List<SubjectGrade>();

        subjectClassrooms.ForEach(subject =>
        {
            // prendo gli esami svolti la singola materia insegnata nella classe dello studente 
            List<StudentExam> studentExams = new GenericRepository<StudentExam>(_context).GetAllUsingIQueryable(null,
                query => takenStudent.StudentExams.AsQueryable()
                    .Where(el => el.Exam.TeacherSubjectClassroom.Subject.Id == subject.Id
                                 //in base al valore del quadrimestre controlla i range: nel primo controlla dal 15 Settembre al 31 Gennaio
                                 // nel secondo caso controlla dall'1 Febbraio al 10 Giugno
                                 && (firstQuarter
                                     ? el.Exam.Date >= startFirstQuarter &&
                                       el.Exam.Date <= endFirstQuarter
                                     : el.Exam.Date >= startSecondQuarter &&
                                       el.Exam.Date <= endSecondQuarter
                                 )
                    )
                , out var total);

            double media = 0;
            
            //se l'alunno non ha eseguito esami in quella materia allora la media rimarrà a 0, invece nel caso opposto calcolerà la media di tutti i voti presi negli esami
            if (studentExams != null)
            {
                foreach (StudentExam el in studentExams)
                {
                    media += el.Grade ?? 0;

                    media /= total;
                }
            }

            //Nel caso in cui la media non presenta un voto allora il voto in quella materia verrà sostituito da NC (non classificato)
            report.Add(new SubjectGrade(subject.Name, media > 0 ? media.ToString() : "NC"));
        });
        
        if (report.Count(el => el.Grade.Trim().ToLower() == "nc") == report.Count())
        {
            return StatusCode(StatusCodes.Status400BadRequest, $"The Student {takenStudent.Registry.Name} {takenStudent.Registry.Surname} in all the quarter hasn't any grades");
        }
        
        var pdf = PdfHandler.GeneratePdf<SubjectGrade>(report, takenStudent, schoolYear, firstQuarter);
        return File(pdf, "application/pdf",
            $"{takenStudent.Registry.Name}_{takenStudent.Registry.Surname}_report.pdf");
    }

    #endregion

    #region Get Student school years

    /// <summary>
    /// Api call which return all the schoolYears which the student did on his school career
    /// </summary>
    /// <param name="userId">Id of the user which we take the student</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{userId}/school_years")]
    [ProducesResponseType(200)]
    public IActionResult GetStudentReport([FromRoute] Guid userId)
    {
        try
        {
            var takenStudent = new GenericRepository<Student>(_context).GetByIdUsingIQueryable(
                query => query
                    .Where(el => el.UserId == userId));
            if (takenStudent == null)
            {
                throw new Exception("NOT_FOUND");
            }

            if (takenStudent.SchoolYear == null)
            {
                throw new Exception("SCHOOL_YEAR_NOT_FOUND");
            }
            var takenSchoolYear = new GenericRepository<PromotionHistory>(_context).GetAllUsingIQueryable(
                null, query => query
                    .Where(el => el.StudentId == takenStudent.Id)
                , out var total).Select(el => el.PreviousSchoolYear).ToList();
            takenSchoolYear.Add(takenStudent.SchoolYear);
            takenSchoolYear = takenSchoolYear.Distinct().ToList();
        
            return Ok(takenSchoolYear);
        }
        catch (Exception e)
        {
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
       
    }

    #endregion
    
    #endregion
}