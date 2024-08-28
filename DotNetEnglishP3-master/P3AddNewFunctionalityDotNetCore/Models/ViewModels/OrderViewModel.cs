using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class OrderViewModel
    {
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "ErrorMissingName")]
        [RegularExpression(@"^[a-zA-Zà-ÿÀ-Ÿ '-]+$", ErrorMessage = "ErrorInvalidName")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ErrorMissingAdress")]
        [RegularExpression(@"^[a-zA-Z0-9à-ÿÀ-Ÿ\s,.'-]+$", ErrorMessage = "ErrorInvalidAddress")]
        public string Address { get; set; }

        [Required(ErrorMessage = "ErrorMissingCity")]
        [RegularExpression(@"^[a-zA-Z\s'-]+$", ErrorMessage = "ErrorInvalidCity")]
        public string City { get; set; }

        [Required(ErrorMessage = "ErrorMissingZipCode")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "ErrorInvalidZipCode")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "ErrorMissingCountry")]
        [RegularExpression(@"^[a-zA-Zà-ÿÀ-Ÿ\s-]+$", ErrorMessage = "ErrorInvalidCountry")]
        public string Country { get; set; }

        [BindNever]
        public DateTime Date { get; set; }
    }
}
