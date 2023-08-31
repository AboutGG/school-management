using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.Utils;

namespace backend.Dto;

public class RegistryDto
{
    /// <summary> name </summary>
    [StringValidator(3, ErrorMessage ="Name cannot contain less then 3 character")]
    public string Name { get; set; }

    /// <summary> surname </summary>
    [StringValidator(3, ErrorMessage = "Surname cannot contain less then 3 character")]
    public string Surname { get; set; }
    
    [Required]
    public String Gender { get; set; } 

    [DateValidator(ErrorMessage = "Date must be a previous date")]
    public DateOnly? Birth { get; set; }
    
    [StringValidator(2)]
    [EmailAddress]
    public string? Email { get; set; }
    
    [StringValidator(2)]
    public string? Address { get; set; }
    
    [Phone]
    [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Invalid phone format")]
    public string? Telephone { get; set; }
}