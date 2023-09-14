namespace backend.Dto;

public class SubjectClassroomDto
{
    public Guid SubjectId { get; set; }
    public string SubjectName { get; set; }
    public Guid ClassroomId { get; set; }
    public String ClassroomName { get; set; }
}