using System.Text.Json.Serialization;

namespace backend.Dto;

public class ClassroomDto
{
    public Guid Id { get; set; }
    public String Name { get; set; }
}