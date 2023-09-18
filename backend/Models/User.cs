using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.Utils;

namespace backend.Models;

[Table("users")]
public class User : Deleted
{
    /// <summary> User id </summary>
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    /// <summary> User username </summary>
    [Column("username")]
    [JsonPropertyName("username")]
    [StringValidator(3, ErrorMessage = "Username cannot contain less then 3 character")]
    public string Username { get; set; }

    /// <summary> User password </summary>
    [Column("password")]
    [JsonPropertyName("password")]
    [StringValidator(3, ErrorMessage = "Password cannot contain less then 3 character")]
    public string Password { get; set; }


    /// <summary> Element from another table</summary>
    public virtual Teacher Teacher { get; set; }

    public virtual Student Student { get; set; }
}