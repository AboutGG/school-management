using backend.Models;

namespace backend.Dto;

public class UserDetailDto
{

    public Guid Id { get; set; }
    public RegistryDto Registry { get; set; }
    public List<UserRoleDto> Roles { get; set; }
    public List<ClassroomDto> Classrooms { get; set; }


}