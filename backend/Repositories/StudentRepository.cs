﻿using AutoMapper;
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
            .OrderBy(s => s.UserId)
            .Include(s => s.User) // Include il registro associato
            .ThenInclude(u => u.Registry)
            .ToList();

        return student;
    }

    public Student GetStudentById(Guid id)
    {
        return _context.Students.Where(s => s.UserId == id)
            .Include(s => s.User)
            .ThenInclude(s => s.Registry)
            .FirstOrDefault();
    }

    public int CountStudents()
    {
        return _context.Students.Count();
    }

    public bool StudentExist(Guid id)
    {
        return _context.Students.Any(s => s.UserId == id);
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

    #region Old GetStudentSubjects
    
    // public object GetStudentSubjects(Guid id)
    // {
    //     //prendo la classe dello studente studente che ha come id quello proveniente dal token
    //     var studentclassroomId = _context.Students.FirstOrDefault(el => el.UserId == id).ClassroomId;
    //     var resultTeacherSubjectClassrooms = _context.TeachersSubjectsClassrooms
    //         .Where(el=> el.ClassroomId == studentclassroomId)
    //         .Include(el=>el.Classroom)
    //         .Include(el=>el.Teacher.Registry)
    //         .Include(el=>el.Subject).ToList();
    //     return resultTeacherSubjectClassrooms;
    // }

    #endregion

    #endregion

}