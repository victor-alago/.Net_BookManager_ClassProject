using System;
using System.ComponentModel.DataAnnotations;

namespace ClassProject.Validations
{
    public class PublicationDateInPastAttribute : ValidationAttribute
    {
        public PublicationDateInPastAttribute() 
            : base("The publication date cannot be in the future.")
        {
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateOnly publicationDate)
            {
                
                if (publicationDate > DateOnly.FromDateTime(DateTime.Now))
                {
                    return new ValidationResult(ErrorMessage ?? "The publication date cannot be in the future.");
                }
                return ValidationResult.Success;
            }

           
            return new ValidationResult("Invalid date format.");
        }
    }
}