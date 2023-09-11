using backend.Models;

namespace backend.Dto;

public class AddEntity
{
    public RegistryDto Registry { get; set; }

    public UserDto User { get; set; }

    public ClassroomDto? Classroom { get; set; }
}