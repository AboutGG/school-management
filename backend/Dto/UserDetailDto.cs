using backend.Utils;

namespace backend.Dto;

public class UserDetailDto
{
    public string? Classroom { get; set; }
    
    public UserDto User { get; set; }
    public RegistryDto Registry { get; set; }

}