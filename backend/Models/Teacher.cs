using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.Dto;

namespace backend.Models;

[Table("teachers")]
public class Teacher : Deleted
{
    /// <summary> Teacher id </summary>
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    /// <summary> Teacher registry id </summary>
    [Column("id_registry")]
    [JsonPropertyName("id_regitry")]
    public Guid RegistryId { get; set; }
    public virtual Registry Registry { get; set; }

    /// <summary> Teacher user id </summary>
    [Column("id_user")]
    [JsonPropertyName("id_user")]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public IList<TeacherSubjectClassroom> TeachersSubjectsClassrooms { get; set; }
}