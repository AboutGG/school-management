using backend.Utils;

namespace backend.Dto;

public class UserStudentDto
{
    [StringValidator(2, ErrorMessage = "Classroom is required")]
    public string Classroom { get; set; }
    
    public UserDto User { get; set; }
    public RegistryDto Registry { get; set; }

}