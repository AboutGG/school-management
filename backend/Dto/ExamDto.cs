using backend.Models;

namespace backend.Dto;

public class ExamDto
{
    public ExamDto(Exam exam)
    {
        ExamDate = exam.Date;
        Subject = exam.TeacherSubjectClassroom.Subject.Name;
        Classroom = exam.TeacherSubjectClassroom.Classroom.Name;
        StudentExams = exam.StudentExams.Select(el => new TeacherStudentExamDto()
        {
            Student = new StudentDto(el.Student),
            Grade = el.Grade
        }).ToList();
    }
    public DateOnly ExamDate { get; set; }
    public string Subject { get; set; }
    public string Classroom { get; set; }
    public List<TeacherStudentExamDto> StudentExams { get; set; }
}