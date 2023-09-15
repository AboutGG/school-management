using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

[Table("exams")]
public class Exam
{
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [Column("id_teacherSubjectClassroom")]
    [JsonPropertyName("id_teacherSubjectClassroom")]
    public Guid TeacherSubjectClassroomId { get; set; }
    public virtual TeacherSubjectClassroom TeacherSubjectClassroom { get; set; }

    [Column("exam_date")]
    [JsonPropertyName("exam_date")]
    public DateOnly ExamDate { get; set; }

    #region Foreign Key reference
    public virtual List<StudentExam> StudentExams { get; set; }
    #endregion
}