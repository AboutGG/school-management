using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

[Table("exams")]
public class Exam
{
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [Column("id_subject")]
    [JsonPropertyName("id_subject")]
    public Guid SubjectId { get; set; }
    public virtual Subject Subject { get; set; }

    [Column("exam_date")]
    [JsonPropertyName("exam_date")]
    public DateOnly ExamDate { get; set; }

    #region Foreign Key reference
    public virtual IList<StudentExam> StudentExams { get; set; }
    #endregion
}