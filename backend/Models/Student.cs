using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.Utils;

namespace backend.Models;

[Table("students")]
public class Student
{
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [Column("classroom")]
    [JsonPropertyName("classroom")]
    [StringValidator(2, ErrorMessage = "Classroom lenght can't be less then 2")]
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