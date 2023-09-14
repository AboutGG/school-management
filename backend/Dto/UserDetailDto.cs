using backend.Models;

namespace backend.Dto;

public class UserDetailDto
{
    public Guid Id { get; set; }
    public RegistryDto Registry { get; set; }
    public List<RoleDto> Roles { get; set; }

    public ClassroomDto Classroom {get; set; }
}