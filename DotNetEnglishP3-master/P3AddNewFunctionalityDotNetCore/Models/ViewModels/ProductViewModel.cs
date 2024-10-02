using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using P3AddNewFunctionalityDotNetCore.Attribute;



namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }
        
        //Ensure that the Name Field is required to continue
        [Required(ErrorMessage = "MissingName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        //Ensure that the Stock field is required
        //Verify that the Stock field is indeed an integer by using a RegEx
        //and is greater than 0 by using a Custom Attribute
        [Required(ErrorMessage = "MissingStock")]
        [RegularExpression(@"^\d+$", ErrorMessage = "StockNotAnInteger")]
        [GreaterThanZero(ErrorMessage = "StockNotGreaterThanZero")]
        public string Stock { get; set; }

        //Ensure that the Price field is required
        //Verify that the Price field is indeed a number by using a RegEx
        //and is greater than 0 by using a Custom Attribute
        [Required(ErrorMessage ="MissingPrice")]
        [RegularExpression(@"^\d+\,\d{2}$", ErrorMessage = "PriceNotANumber")]
        [GreaterThanZero(ErrorMessage = "PriceNotGreaterThanZero")]
        public string Price { get; set; }
    }
}
