namespace backend.Dto;

public class UpdateTeacherRequest
{
    public Guid classroomId;

    public Guid subjectId;

    public UpdateTeacherRequest(Guid subjectId, Guid classroomId)
    {
       this.subjectId = subjectId;
       this.classroomId = classroomId;
    }
}