using backend.Dto;
using backend.Models;

namespace backend.Interfaces;

public interface IStudentRepository
{
    ICollection<Student> GetStudents();
    Student GetStudentById(Guid id);
    bool StudentExist(Guid id);
    bool CreateStudent(Student student);
    bool DeleteStudent(Guid id);
    bool Save();
}