using backend.Models;

namespace backend.Dto;

public class SubjectClassroomDto
{
    public SubjectClassroomDto(TeacherSubjectClassroom teacherSubjectClassroom)
    {
        SubjectId = teacherSubjectClassroom.SubjectId;
        SubjectName = teacherSubjectClassroom.Subject.Name;
        ClassroomId = teacherSubjectClassroom.ClassroomId;
        ClassroomName = teacherSubjectClassroom.Classroom.Name;
    }
    public Guid SubjectId { get; set; }
    public string SubjectName { get; set; }
    public Guid ClassroomId { get; set; }
    public String ClassroomName { get; set; }
}