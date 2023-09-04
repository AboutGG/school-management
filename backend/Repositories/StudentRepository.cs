using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class StudentRepository : IStudentRepository
{
    #region Attributes

    private readonly SchoolContext _context;
    private readonly IMapper _mapper;

    #endregion

    #region Costructor

    public StudentRepository(SchoolContext context, IMapper mapper)
    {
        this._context = context;
        _mapper = mapper;
    }

    #endregion

    #region Attributes

    /// <summary> In this function i take All the Student including the User and Registry reference. </summary>
    /// <returns>Returns Student with his data contains User and Registry related to it</returns>
    public ICollection<Student> GetStudents()
    {
        var student = _context.Students
            .OrderBy(s => s.Id)
            .Include(s => s.User) // Include il registro associato
            .Include(s => s.Registry)
            .ToList();

        return student;
    }

    public Student GetStudentById(Guid id)
    {
        return _context.Students.Where(s => s.Id == id)
            .Include(s => s.User)
            .Include(s => s.Registry)
            .FirstOrDefault();
    }

    public int CountStudents()
    {
        return _context.Students.Count();
    }

    public bool StudentExist(Guid id)
    {
        return _context.Students.Any(s => s.Id == id);
    }

    public bool CreateStudent(Student student)
    {
        _context.Students.Add(student);
        return Save();
    }

    public bool DeleteStudent(Guid id)
    {
        var student = GetStudentById(id);
        _context.Students.Remove(student);
        return Save();
    }
    

    //save the changes on db
    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

    public object GetStudentSubjects(Guid id)
    {
        //prendo lo studente che ha come id quello proveniente dal token
        var result = _context.Students.Where(el => el.UserId == id)
            //includo TeacherSubjectClassroom con la materia e l'anagrafica per ogni elemento
            .Include(el => el.Classroom.TeacherSubjectsClassrooms)
            .ThenInclude(el => el.Subject)
            .Include(el => el.Classroom.TeacherSubjectsClassrooms)
            .ThenInclude(el => el.Teacher.Registry)
            .Select(el => //uso la select per creare un nuovo oggetto contenente gli attributi che mi interessano
                new
                {
                    classroomName = el.Classroom.Name,
                    TeacherSubjectClassrooms = el.Classroom.TeacherSubjectsClassrooms
                        .Select( el => 
                            new
                            {
                                TeacherName = el.Teacher.Registry.Name,
                                TeahcerSurname = el.Teacher.Registry.Surname,
                                Subject = el.Subject.Name
                            })
                }
            );
        return result;
    }

    #endregion

}