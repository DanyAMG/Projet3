using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Attribute
{

    //This methode serve the Custom Atribut [GreaterThanZero] by converting the string field received into a double number
    //and then compares if it's lower than 0. If it's greater then The validation result is sucessfull,
    //else the validation result send the ErrorMessage bound to the CustomAtttributes, if no ErrorMessage is bound
    //then it's send the message "Value must be greater than 0"
    public class GreaterThanZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessage ?? "Value is null");
            }
            else
            {
                if(double.TryParse(value.ToString(), out double result))
                {
                    if (result > 0)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult(ErrorMessage ?? "Value must be greater than 0");
                    }
                }
                else
                {
                    return new ValidationResult("Value is not a number");
                }
                
            }
            
        }
        public override bool IsValid(object value)
        {
            
            return this.IsValid(value, null) == ValidationResult.Success;
        }
    }
}
