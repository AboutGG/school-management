using backend.Models;

namespace backend.Dto;

public class StudentExamDto
{
    public StudentExamDto(StudentExam studentExams)
    {
        Subject = studentExams.Exam.TeacherSubjectClassroom.Subject.Name;
        Date = studentExams.Exam.Date;
        Grade = studentExams.Grade ?? null;
        Teacher = $"{studentExams.Exam.TeacherSubjectClassroom.Teacher.Registry.Name} " +
                  $"{studentExams.Exam?.TeacherSubjectClassroom.Teacher.Registry.Surname}";
    }
    
    public string Subject { get; set; }

    public DateOnly Date { get; set; }

    public int? Grade { get; set; }

    public string Teacher { get; set; }
}