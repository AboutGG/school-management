using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;
[Table("promotion_histories")]
public class PromotionHistory: Deleted
{
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [Column("id_student")]
    [JsonPropertyName("id_student")]
    public Guid StudentId { get; set; }
    public virtual Student Student { get; set; }
    
    [Column("id_previous_classroom")]
    [JsonPropertyName("id_previous_classroom")]
    public Guid PreviousClassroomId { get; set; }
    public virtual Classroom PreviousClassroom { get; set; }
    
    [Column("previous_school_year")]
    [JsonPropertyName("previous_school_year")]
    public string PreviousSchoolYear { get; set; }
    
    [Column("final_graduation")]
    [JsonPropertyName("final_graduation")]
    public int FinalGraduation { get; set; }
    
    [Column("scholastic_behavior")]
    [JsonPropertyName("scholastic_behavior")]
    public int ScholasticBehavior { get; set; }
    
    [Column("promoted")]
    [JsonPropertyName("promoted")]
    public bool Promoted { get; set; }
}