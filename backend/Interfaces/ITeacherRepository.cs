using backend.Models;

namespace backend.Interfaces;

public interface ITeacherRepository
{
    ICollection<Teacher> GetTeachers();

    /// <summary> Check if user exist </summary>
    /// <param name="teacherId"></param>
    /// <returns>true if exist, false if not exist</returns>
    bool TeacherExists(Guid teacherId);

    /// <summary> Create a user </summary>
    /// <param name="teacher"></param>
    /// <returns>true successful, false not successful</returns>
    bool CreateTeacher(Teacher teacher);
    bool Save();
}