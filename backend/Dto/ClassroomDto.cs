using System.Text.Json.Serialization;

namespace backend.Dto;

public class ClassroomDto
{
    public Guid id_classroom { get; set; }
    public String name_classroom { get; set; }
}