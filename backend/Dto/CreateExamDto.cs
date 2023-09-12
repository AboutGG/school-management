using backend.Models;

namespace backend.Dto;

public class CreateExamDto
{
    public DateOnly ExamDate { get; set; }
    public Guid SubjectId { get; set; }
    public Guid ClassroomId { get; set; }
}