using backend.Models;

namespace backend.Dto;

public class TeacherDto
{
    public Guid Id { get; set; }

    /// <summary> Teacher user id </summary>
    public Guid UserId { get; set; }

    public virtual UserDto User { get; set; }

    /// <summary> Teacher registry id </summary>
    public Guid RegistryId { get; set; }

    public virtual RegistryDto Registry { get; set; }

}