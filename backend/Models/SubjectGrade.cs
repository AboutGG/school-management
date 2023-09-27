namespace backend.Models;

/// <summary>
/// Model to indicate the final grade of a Student
/// </summary>
public class SubjectGrade
{
    public SubjectGrade(string subject, string grade)
    {
        Subject = subject;
        Grade = grade;
    }

    public string Subject { get; set; }
    public string Grade { get; set; }
}