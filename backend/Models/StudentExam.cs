﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

[Table("students_exams")]
public class StudentExam
{
    /// <summary> Registries_exam Foreign Key id_student </summary>
    [Column("id_user")]
    [JsonPropertyName("id_user")]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    /// <summary> Registries_exam Foreign Key id_subject </summary>
    [Column("id_exam")]
    [JsonPropertyName("id_exam")]
    public Guid ExamId { get; set; }

    public virtual Exam Exam { get; set; }

    /// <summary> Registries_exam grade </summary>
    [Column("grade")]
    [JsonPropertyName("grade")]
    public int? Grade { get; set; }
}