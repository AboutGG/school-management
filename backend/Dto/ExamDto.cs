namespace backend.Dto;

public class ExamDto
{
    public DateOnly ExamDate { get; set; }
    public string Subject { get; set; }
    public List<TeacherStudentExamDto> StudentExams { get; set; }
}