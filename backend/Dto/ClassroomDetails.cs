using backend.Models;

namespace backend.Dto;

public class ClassroomDetails
{
    public List<StudentDto> Students { get; set; }
    
    public List<TeacherDto> Teachers { get; set; }
}