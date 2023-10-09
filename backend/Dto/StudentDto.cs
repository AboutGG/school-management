using backend.Models;
using backend.Utils;

namespace backend.Dto;

public class StudentDto
{
    public StudentDto(Student student)
    {
        id = student.UserId;
        name = student.Registry.Name;
        surname = student.Registry.Surname;
    }
    
    public Guid id { get; set; }
    public string name { get; set; }
    public string surname { get; set; }
}