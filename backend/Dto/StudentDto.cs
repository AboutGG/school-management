using backend.Models;
using backend.Utils;

namespace backend.Dto;

public class StudentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}