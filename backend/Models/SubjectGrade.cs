namespace backend.Models;

/// <summary>
/// Model to indicate the final grade of a Student
/// </summary>
public class SubjectGrade
{
    public SubjectGrade(string subject, double grade)
    {
        Subject = subject;
        Grade = grade;
    }

    public string Subject { get; set; }
    public double Grade { get; set; }
}