using backend.Dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class TeacherRepository : ITeacherRepository
{
    #region Attributes

    private readonly SchoolContext _context;
    private readonly IGenericRepository<Teacher> _teacherGenericRepository;
    private readonly IGenericRepository<TeacherSubjectClassroom> _tscGenericRepository;

    #endregion

    #region Costructor

    public TeacherRepository(
        SchoolContext context,
        IGenericRepository<Teacher> teacherGenericRepository,
        IGenericRepository<TeacherSubjectClassroom> tscGenericRepository)
    {
        _context = context;
        _teacherGenericRepository = teacherGenericRepository;
        _tscGenericRepository = tscGenericRepository;
    }

    #endregion

    #region Methods

    /// <summary> In this function i take All the Teachers including the User and Registry reference. </summary>
    /// <returns>Returns Teacher with his data contains User and Registry related to it</returns>
    public List<Teacher> GetTeachers()
    {
        var teachers = _context.Teachers
            .OrderBy(t => t.Id)
            .Include(t => t.Registry) // Include il registro associato
            .Include(t => t.User)
            .ToList();

        return teachers;
    }

    public Teacher GetTeacherById(Guid id)
    {
        var teacher = _context.Teachers.Where(s => s.Id == id)
            .Include(s => s.User)
            .Include(s => s.Registry)
            .FirstOrDefault();
        return teacher;
    }
    
    public Teacher GetTeacherByUserId(Guid id)
    {
        var teacher = _teacherGenericRepository
            .GetByIdUsingIQueryable(query =>
                query.Where(el => el.UserId == id));
        
        return teacher;
    }

    public List<Classroom> GetClassroomByTeacherId(Guid id)
    {
        var classrooms = _context.Teachers
            .Where(el => el.UserId == id)
            .Include(el => el.TeachersSubjectsClassrooms)
            .ThenInclude(el => el.Classroom.Students)
            .SelectMany(el => el.TeachersSubjectsClassrooms
                .Select(c => c.Classroom))
            .ToList();
        return classrooms;
    }

    #region UpdateTeacherDesk

    public List<TeacherSubjectClassroom> AssignTeacherDesk(Guid userId, List<UpdateTeacherRequest> request)
    {
        var teacherId = GetTeacherByUserId(userId).Id;

        if (teacherId == null)
            throw new Exception("NOT_FOUND");

        List<TeacherSubjectClassroom> result = new List<TeacherSubjectClassroom>();
        foreach (var desk in request)
        {
            TeacherSubjectClassroom tsc = new TeacherSubjectClassroom
            {
                Id = Guid.NewGuid(),
                TeacherId = teacherId,
                ClassroomId = desk.classroomId,
                SubjectId = desk.subjectId
            };
            _tscGenericRepository.Create(tsc);
            result.Add(tsc);
        }
        return result;
    }

    #endregion

    public int CountTeachers()
    {
        return _context.Teachers.Count();
    }

    public bool TeacherExists(Guid id)
    {
        return this._context.Teachers.Any(t => t.Id == id);
    }

    public bool CreateTeacher(Teacher teacher)
    {
        this._context.Teachers.Add(teacher);
        return Save();
    }

    public bool DeleteTeachers(Guid id)
    {
        var teacher = GetTeacherById(id);
        this._context.Teachers.Remove(teacher);
        return Save();
    }

    public bool UpdateTeacher(Teacher teacher)
    {
        _context.Teachers.Update(teacher);
        return Save();
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }

    #endregion

}