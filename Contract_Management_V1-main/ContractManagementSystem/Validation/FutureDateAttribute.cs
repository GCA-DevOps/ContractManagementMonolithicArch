using System;
using System.ComponentModel.DataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime dateValue;
        if (value == null || !DateTime.TryParse(value.ToString(), out dateValue))
        {
            return new ValidationResult("Invalid date format.");
        }

        if (dateValue < DateTime.Today)
        {
            return new ValidationResult("The start date cannot be earlier than today.");
        }

        return ValidationResult.Success;
    }
}
