using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Attribute
{
    public class GreaterThanZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            double.TryParse(value.ToString(), out double result);
            
                if (result > 0)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage ?? "Value must be greater than 0");
                }
            
        }
    }
}
