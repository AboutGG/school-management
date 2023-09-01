using System.Text.Json.Serialization;

namespace backend.Dto;

public class ClassroomDto
{
    [JsonPropertyName("id_classroom")]
    public Guid Id { get; set; }

    [JsonPropertyName("name_classroom")]
    public String Name { get; set; }
}