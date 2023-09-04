using System.Text.Json.Serialization;

namespace backend.Dto;

public class ClassroomStudentCount
{
    [JsonPropertyName("id_classroom")]
    public Guid id_classroom { get; set; }

    [JsonPropertyName("name_classroom")]
    public string name_classroom { get; set; }

    [JsonPropertyName("student_count")]
    public int student_count { get; set; }
}