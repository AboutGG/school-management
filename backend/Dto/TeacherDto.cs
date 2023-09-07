namespace backend.Dto;

public class TeacherDto
{
    public Guid id { get; set; }
    public String name { get; set; }
    public String surname { get; set; }
    public String[] subjects { get; set; }
}