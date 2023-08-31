using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

[Table("teachers_subjects_classrooms")]
public class TeacherSubjectClassroom
{
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
}