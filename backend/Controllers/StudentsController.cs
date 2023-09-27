using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core;
using AutoMapper;
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
            var resultStudent = new GenericRepository<TeacherSubjectClassroom>(_context).GetAllUsingIQueryable(
                @params,
                query => query
                    .Where(el => el.ClassroomId == studentclassroomId)
                    .Include(el => el.Classroom)
                    .Include(el => el.Teacher.Registry)
                    .Include(el => el.Subject),
                out var total
            ).Select(el => el.Teacher).ToList();

            var mappedResponse = _mapper.Map<List<TeacherDto>>(resultStudent.DistinctBy(el => el.Id));

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
    /// <returns>Return a PDF file with the student's report</returns>
    [HttpGet]
    [Route("{Id}/Reports")]
    [ProducesResponseType(200)]
    public IActionResult GetStudentReport([FromRoute] Guid Id)
    {
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
        ).TeacherSubjectsClassrooms.Select(el => new Subject{ Id = el.Subject.Id, Name = el.Subject.Name}).ToList();
        
        List<SubjectGrade> report = new List<SubjectGrade>();
        
        foreach (Subject subject in subjectClassrooms)
        {
            // prendo gli esami per materia
            List<StudentExam> studentExams = new GenericRepository<StudentExam>(_context).GetAllUsingIQueryable(null,
                query => takenStudent.StudentExams.AsQueryable()
                    .Where(el => el.Exam.TeacherSubjectClassroom.Subject.Id == subject.Id)
                , out var total);
            
            double media = 0;
            //se l'alunno non ha eseguito esami in quella materia allora la media rimarrà a 0, nel caso opposto calcolerà la media di tutti i voti presi negli esami
            //nel caso in cui non è presente un voto allora metterà il valore 0
            if (studentExams != null)
            {
                foreach (StudentExam el in studentExams)
                {
                    media += el.Grade ?? 0;

                    media /= total;
                }
            }

            report.Add(new SubjectGrade(subject.Name, media));
        }
        
        var pdf = PdfHandler.GeneratePdf<SubjectGrade>(report);
        return File(pdf, "application/pdf", $"{takenStudent.Registry.Name}_{takenStudent.Registry.Surname}_student_report.pdf");
    }

    #endregion
    
    #endregion
}