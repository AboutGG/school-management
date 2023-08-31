﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

[Table("classrooms")]
public class Classroom
{
    #region Id
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    #endregion
    
    #region Name
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    #endregion

    #region External Keys
    public virtual ICollection<Student> Students { get; set; }
    public virtual IList<TeacherSubjectClassroom> TeacherSubjects { get; set; }
    
    #endregion
}