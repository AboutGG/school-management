using backend.Models;

namespace backend.Dto;

public class ExamResponseDto
{
    public ExamResponseDto(Guid? Id, DateOnly date, Classroom classroom, Subject subject)
    {
        this.Id = Id;
        this.Date = date;
        Classroom = new ClassroomDto(classroom);
        Subject = new SubjectDto(subject);
    }
    
    public Guid? Id { get; set; }
    public DateOnly Date { get; set; }
    public ClassroomDto Classroom { get; set; }
    public SubjectDto Subject { get; set; }

}