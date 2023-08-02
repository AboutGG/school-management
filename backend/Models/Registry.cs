﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.Utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace backend.Models;

[Table("registries")]
public class Registry
{
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [JsonPropertyName("name")]
    [StringValidator(3, ErrorMessage = "name cannot contain less then 3 character")]
    public string Name { get; set; }

    [Column("surname")]
    [JsonPropertyName("surname")]
    [StringValidator(3, ErrorMessage = "surname cannot contain less then 3 character")]
    public string Surname { get; set; }

    [Column("gender")]
    [JsonPropertyName("gender")]
    public String Gender { get; set; }

    [Column("birth")]
    [JsonPropertyName("birth")]
    [DateValidator(ErrorMessage = "Date must be a previous date")]
    public DateOnly? Birth { get; set; }

    [Column("email")]
    [JsonPropertyName("email")]
    [EmailAddress]
    public string? Email { get; set; }

    [Column("address")]
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [Column("telephone")]
    [JsonPropertyName("telephone")]
    [Phone]
    public string? Telephone { get; set; }

    #region References from another table

    /// <summary> Elements from another table </summary>
    public virtual Teacher Teacher { get; set; }

    public virtual Student Student { get; set; }
    public virtual IList<RegistryExam> RegistryExams { get; set; }

    #endregion
}