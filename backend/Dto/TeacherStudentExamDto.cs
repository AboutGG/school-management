using iText.Commons.Utils;

namespace backend.Dto;

public class TeacherStudentExamDto
{
    public StudentDto Student { get; set; }
    public int? Grade { get; set; }
}