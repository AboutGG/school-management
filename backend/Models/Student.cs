using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

[Table("students")]
public class Student
{
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [Column("classroom")]
    [JsonPropertyName("classroom")]
    public string Classroom { get; set; }

    [Column("id_user")]
    [JsonPropertyName("id_user")]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    [Column("id_registry")]
    [JsonPropertyName("id_registry")]
    public Guid RegistryId { get; set; }
    public virtual Registry Registry { get; set; }
}