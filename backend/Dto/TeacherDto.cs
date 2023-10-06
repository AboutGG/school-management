using backend.Models;

namespace backend.Dto;

public class TeacherDto
{
    public TeacherDto(Teacher teacher)
    {
        id = teacher.UserId;
        name = teacher.Registry.Name;
        surname = teacher.Registry.Surname;
        subjects = teacher.TeachersSubjectsClassrooms.Select(el => el.Subject.Name).Distinct().ToArray();
    }
    public Guid id { get; set; }
    public String name { get; set; }
    public String surname { get; set; }
    public String[] subjects { get; set; }
}