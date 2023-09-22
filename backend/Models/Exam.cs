using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

[Table("exams")]
public class Exam : Deleted
{
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [Column("id_teacherSubjectClassroom")]
    [JsonPropertyName("id_teacherSubjectClassroom")]
    public Guid TeacherSubjectClassroomId { get; set; }
    public virtual TeacherSubjectClassroom TeacherSubjectClassroom { get; set; }

    [Column("date")]
    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    #region Foreign Key reference
    public virtual List<StudentExam> StudentExams { get; set; }
    #endregion
}