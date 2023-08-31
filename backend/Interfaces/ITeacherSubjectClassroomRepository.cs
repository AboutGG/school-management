using System.Linq.Expressions;
using backend.Models;

namespace backend.Interfaces;

public interface ITeacherSubjectClassroomRepository
{
    /// <summary> Rturn all teacherSubject </summary>
    ICollection<TeacherSubjectClassroom> GetTeachersSubjects();

    public ICollection<TeacherSubjectClassroom> GetTeachersSubjects(
        params Expression<Func<TeacherSubjectClassroom, object>>[] includeExpression);

    /// <summary> Check if user exist </summary>
    /// <param name="teacherId"></param>
    /// <returns>true if exist, false if not exist</returns>

    /// <summary> Create a user </summary>
    /// <param name="teacherSubjectClassroom"></param>
    /// <returns>true successful, false not successful</returns>
    bool CreateTeacherSubject(TeacherSubjectClassroom teacherSubjectClassroom);
    bool Save();
}