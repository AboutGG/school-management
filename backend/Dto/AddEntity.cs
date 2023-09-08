using backend.Models;

namespace backend.Dto;

public class AddEntity
{
    public Registry Registry { get; set; }

    public User User { get; set; }

    public Classroom? Classroom { get; set; }
}