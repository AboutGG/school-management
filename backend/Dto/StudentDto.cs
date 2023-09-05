using backend.Models;
using backend.Utils;

namespace backend.Dto;

public class StudentDto
{
    public Guid id { get; set; }
    public string name { get; set; }
    public string surname { get; set; }
}