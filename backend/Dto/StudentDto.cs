using backend.Models;
using backend.Utils;

namespace backend.Dto;

public class StudentDto
{
    public Guid Id { get; set; }
    [StringValidator(2, ErrorMessage = "Classroom cannot contain less then 3 character")]
    public string Classroom { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public Guid RegistryId { get; set; }
    public virtual Registry Registry { get; set; }
}