using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.Utils;

namespace backend.Models;

[Table("students")]
public class Student : Deleted
{
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [Column("id_classroom")]
    [JsonPropertyName("id_classroom")]
    public Guid ClassroomId { get; set; }
    public virtual Classroom Classroom { get; set; }
    
    [Column("school_year")]
    [JsonPropertyName("school_year")]
    public string? SchoolYear { get; set; }

    [Column("id_user")]
    [JsonPropertyName("id_user")]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    [Column("id_registry")]
    [JsonPropertyName("id_registry")]
    public Guid RegistryId { get; set; }
    public virtual Registry Registry { get; set; }
    
    #region Foreign Keys

    public virtual List<StudentExam> StudentExams{ get; set; }
    
    public virtual List<PromotionHistory> PromotionHistories { get; set; }

    #endregion
}