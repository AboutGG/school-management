using System.Text.Json.Serialization;
using backend.Models;

namespace backend.Dto;

public class ClassroomDto
{
    
    public ClassroomDto(Classroom classroom)
    {
        Id = classroom.Id;
        Name = classroom.Name;
    }

    public ClassroomDto()
    {

    }

    public Guid Id { get; set; }
    public String Name { get; set; }
}