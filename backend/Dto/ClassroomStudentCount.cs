using System.Text.Json.Serialization;
using backend.Models;
using backend.Utils;

namespace backend.Dto;

public class ClassroomStudentCount
{
    public ClassroomStudentCount(Classroom classroom)
    {
        id_classroom = classroom.Id;
        name_classroom = classroom.Name;
        student_count = classroom.Students.Where(el => el.SchoolYear == SchoolYearUtils.GetCurrentSchoolYear()).ToList().Count();
    }
    
    
    public Guid id_classroom { get; set; }
    public string name_classroom { get; set; }
    public int student_count { get; set; }
}