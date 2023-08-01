using backend.Models;

namespace backend.Interfaces;

public interface IStudentRepository
{
    ICollection<Student> GetStudents();
    bool CreateStudent(Student student);
    bool Save();
}