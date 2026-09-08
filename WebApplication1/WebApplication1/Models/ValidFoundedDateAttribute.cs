using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class ValidFoundedDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime date)
        {
            var now = DateTime.Now;
            var minDate = now.AddYears(-100);

            if (date > now)
                return new ValidationResult("Дата основания не может быть в будущем.");

            if (date < minDate)
                return new ValidationResult("Дата основания не может быть ранее 100 лет назад.");
        }
        return ValidationResult.Success;
    }
}