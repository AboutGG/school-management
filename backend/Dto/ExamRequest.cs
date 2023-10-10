using backend.Models;

namespace backend.Dto;

public class ExamRequest
{
    public Guid classroomId { get; set; }
    public DateOnly date { get; set; }
    public Guid subjectId { get; set; }

    public ExamRequest(DateOnly date, Guid classroomId, Guid subjectId)
    {
        this.date = date;
        this.classroomId = classroomId;
        this.subjectId = subjectId;
    }
}