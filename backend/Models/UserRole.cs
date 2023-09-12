using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;


[Table("users_roles")]
public class UserRole
{
    [Column("id_user")]
    [JsonPropertyName("id_user")]
    public Guid UserId { get; set; }
    
    public User User { get; set; }
    
    [Column("id_role")]
    [JsonPropertyName("id_role")]
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
}