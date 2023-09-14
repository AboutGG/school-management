using backend.Models;

namespace backend.Dto;

public class UserList
{
    public Guid id { get; set; }
    public RegistryDto Registry { get; set; }
}