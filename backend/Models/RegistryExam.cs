using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

[Table("registries_exams")]
public class RegistryExam
{
    /// <summary> Registries_exam Foreign Key id_registry </summary>
    [Column("id_registry")]
    [JsonPropertyName("id_registry")]
    public Guid RegistryId { get; set; }

    public virtual Registry Registry { get; set; }

    /// <summary> Registries_exam Foreign Key id_subject </summary>
    [Column("id_exam")]
    [JsonPropertyName("id_exam")]
    public Guid ExamId { get; set; }

    public virtual Exam Exam { get; set; }

    /// <summary> Registries_exam grade </summary>
    [Column("grade")]
    [JsonPropertyName("grade")]
    public int? Grade { get; set; }
}