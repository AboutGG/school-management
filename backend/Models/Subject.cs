using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

[Table("subjects")]
public class Subject : Deleted
{
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    /// <summary> subject name </summary>
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    #region External ref
    public IList<TeacherSubjectClassroom> TeacherSubjectsClassrooms { get; set; }
    #endregion
}