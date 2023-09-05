using backend.Dto;
using backend.Models;

namespace backend.Interfaces;

public interface ITeacherRepository
{
    List<Teacher> GetTeachers();

    Teacher GetTeacherById(Guid id);

    int CountTeachers();

    /// <summary> Check if user exist </summary>
    /// <param name="teacherId"></param>
    /// <returns>true if exist, false if not exist</returns>
    bool TeacherExists(Guid teacherId);

    /// <summary> Create a user </summary>
    /// <param name="teacher"></param>
    /// <returns>true successful, false not successful</returns>
    bool CreateTeacher(Teacher teacher);
    
    bool DeleteTeachers(Guid id);
    
    bool UpdateTeacher(Teacher teacher);

    List<Classroom> GetClassroomByTeacherId(Guid id);
    
    //object GetTeacherSubjectClassroom(Guid id);
    bool Save();
}