using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

[Table("roles")]
public class Role
{
    [Column("id")]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    public List<UserRole> UsersRoles { get; set; }
}