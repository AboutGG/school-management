namespace backend.Dto;

public class InputStudentExamDto
{
    public Guid ExamId { get; set; }

    public Guid Id { get; set; }
    
    public int? Grade { get; set; }
}