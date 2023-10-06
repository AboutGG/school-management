using backend.Utils;

namespace backend.Dto;

public class ChangePasswordDto
{
    
    public string oldPassword { get; set; }
    
    [StringValidator(3, ErrorMessage = "Password cannot contain less then 3 character")]
    public string newPassword { get; set; }
}