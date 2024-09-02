using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Controllers;
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Attribute
{
    public class LocalizedError:RequiredAttribute
    {
        private readonly IStringLocalizer<ProductController> _localizer;

    }

//    public LocalizedError(IStringLocalizer<ProductController> localizer)
//    {

//    }
}
