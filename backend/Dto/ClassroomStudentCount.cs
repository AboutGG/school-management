using System.Text.Json.Serialization;

namespace backend.Dto;

public class ClassroomStudentCount
{
    [JsonPropertyName("id_classroom")]
    public Guid ClassroomId { get; set; }

    [JsonPropertyName("name_classroom")]
    public string Name { get; set; }

    [JsonPropertyName("student_count")]
    public int StudentCount { get; set; }
}