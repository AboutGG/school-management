namespace backend.Dto;

public class InputUpdateExamDto
{
    public DateOnly Date { get; set; }
    public Guid classroomId { get; set; }
    public Guid subjectId { get; set; }
}