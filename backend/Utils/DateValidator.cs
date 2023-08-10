using System.ComponentModel.DataAnnotations;

namespace backend.Utils;

public class DateValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null) 
        {
            if (value is not DateOnly dateValue)
            {
                return new ValidationResult("This value is not a date.");
            }

            if (dateValue > DateOnly.MaxValue || dateValue < DateOnly.MinValue)
            {
                return new ValidationResult("The date is not valid");
            }

            if (dateValue > DateOnly.FromDateTime(DateTime.Now))
            {
                return new ValidationResult("The date must be less then today.");
            }
        }

        return ValidationResult.Success;
    }
}