using System.Text.Json.Serialization;
using backend.Models;

namespace backend.Dto;

public class UserDetailResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("surname")]
    public string Surname { get; set; }
    
    [JsonPropertyName("gender")]
    public string Gender { get; set; } 
    
    [JsonPropertyName("birth")]
    public DateOnly? Birth { get; set; }
    
    [JsonPropertyName("email")]
    public string? Email { get; set; }
  
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("telephone")]
    public string? Telephone { get; set; }
    
    [JsonPropertyName("role")]
    public string Role { get; set; }

    public UserDetailResponse(Registry registry, string role)
    {
        Name = registry.Name;
        Surname = registry.Surname;
        Gender = registry.Gender;
        Birth = registry.Birth;
        Email = registry.Email;
        Address = registry.Address;
        Telephone = registry.Telephone;
        Role = role;
    }
    
}