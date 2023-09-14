namespace backend.Dto;

public class TeacherExamDto
{
    public Guid ExamId { get; set; }
    public DateOnly ExamDate { get; set; }
    public string Classroom { get; set; }
    public string Subject { get; set; }

}