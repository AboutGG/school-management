using System.Text.Json.Serialization;

namespace backend.Dto;

public class ClassroomDto
{
    [JsonPropertyName("id_classroom")]
    public Guid ClassroomId { get; set; }

    [JsonPropertyName("name_classroom")]
    public String Name { get; set; }
}