using backend.Models;

namespace backend.Interfaces;

public interface ITeacherSubjectRepository
{
    /// <summary> Rturn all teacherSubject </summary>
    ICollection<TeacherSubject> GetTeachersSubjects();

    /// <summary> Check if user exist </summary>
    /// <param name="teacherId"></param>
    /// <returns>true if exist, false if not exist</returns>

    /// <summary> Create a user </summary>
    /// <param name="teacherSubject"></param>
    /// <returns>true successful, false not successful</returns>
    bool CreateTeacherSubject(TeacherSubject teacherSubject);
    bool Save();
}