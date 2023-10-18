namespace backend.Utils;

public static class CurrentSchoolYear
{
    public static string GetCurrentSchoolYear()
    {
        string currentSchoolYear = string.Empty;

        var dummy = DateTime.UtcNow;
        if (dummy.Month >= 09 && dummy.Month <= 12)
        {
            currentSchoolYear = $"{dummy.Year}-{dummy.Year + 1}";
        }
        else
        {
            currentSchoolYear = $"{dummy.Year -1}-{dummy.Year}";
        }
        if (dummy.Month >= 01 && dummy.Month<= 06)
        {
            currentSchoolYear = $"{dummy.Year - 1}-{dummy.Year}";
        }
        else
        {
            currentSchoolYear = $"{dummy.Year}-{dummy.Year + 1}";
        }
        
        return currentSchoolYear;
    }
    
}