using System.ComponentModel.DataAnnotations;

namespace backend.Utils;

public class AgeRange : ValidationAttribute
{
    private readonly int _minAge;
    private readonly int _maxAge;

    public AgeRange(int minAge, int maxAge)
    {
        this._minAge = minAge;
        this._maxAge = maxAge;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not DateOnly dateOfBirth)
        {
            return new ValidationResult("Il valore fornito non è una data valida.");
        }

        int age = CalculateAge(dateOfBirth);

        if (age <= _minAge || age >= _maxAge)
        {
            return new ValidationResult($"The user’s age must be between {this._minAge} and {this._maxAge} years.");
        }

        return ValidationResult.Success;
    }

    private static int CalculateAge(DateOnly dateOfBirth)
    {
        ///<summary> Calculate the age using the current data and the birth date. </summary>
        int age = DateTime.Today.Year - dateOfBirth.Year;

        ///<summary>Subtract a year if the user has not yet completed the current year. </summary>
        if (DateOnly.FromDateTime(DateTime.Today) < dateOfBirth.AddYears(age))
        {
            age--;
        }

        return age;
    }
}