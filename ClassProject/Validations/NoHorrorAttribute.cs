using System.ComponentModel.DataAnnotations;

namespace ClassProject.Validations;

public class NoHorrorAttribute : ValidationAttribute
{

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var book = (Book)validationContext.ObjectInstance;

        if (book.Genre == Book.BookGenre.Horror)
        {
            return new ValidationResult("Sorry, Horror books are not allowed.");
        }
        return ValidationResult.Success;
    }
}
