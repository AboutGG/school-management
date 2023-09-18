using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

[Table("teachers_subjects_classrooms")]
public class TeacherSubjectClassroom : Deleted
{
    #region Id
    [Required]
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    #endregion
    
    #region Teacher
    [Column("id_teacher")]
    [JsonPropertyName("id_techer")]
    public Guid TeacherId { get; set; }
    public virtual Teacher Teacher { get; set; }
    #endregion
    
    #region Subject
    [Column("id_subject")]
    [JsonPropertyName("id_subject")]
    public Guid SubjectId { get; set; }
    public virtual Subject Subject { get; set; }
    #endregion

    #region Classroom
    [Column("id_classroom")]
    [JsonPropertyName("id_classroom")]
    public Guid ClassroomId { get; set; }
    public virtual Classroom Classroom { get; set; }
    #endregion

    #region Foreign Key

    public virtual ICollection<Exam> Exam { get; } = new List<Exam>();

    #endregion
}