using System.Text.Json.Serialization;

namespace backend.Dto;

public class ClassroomStudentCount
{
    public Guid id_classroom { get; set; }
    public string name_classroom { get; set; }
    public int student_count { get; set; }
}