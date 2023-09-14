namespace backend.Dto;

public class TeacherSubjectDto
{
    public Guid id { get; set; }
    public String name { get; set; }
    public String surname { get; set; }
    public SubjectClassroomDto[] subjects { get; set; }
}