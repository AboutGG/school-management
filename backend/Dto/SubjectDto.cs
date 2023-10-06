using backend.Models;

namespace backend.Dto;

public class SubjectDto
{
    public SubjectDto(Subject subject)
    {
        Id = subject.Id;
        Name = subject.Name;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
}