using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

[Table("teachers_subjects")]
public class TeacherSubject
{
    [Column("id_teacher")]
    [JsonPropertyName("id_techer")]
    public Guid TeacherId { get; set; }

    public Teacher Teacher { get; set; }

    [Column("id_subject")]
    [JsonPropertyName("id_subject")]
    public Guid SubjectId { get; set; }

    public Subject Subject { get; set; }

    [Column("classroom")]
    [JsonPropertyName("classroom")]
    public string Classroom { get; set; }

}