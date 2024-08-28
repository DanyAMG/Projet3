using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3AddNewFunctionalityDotNetCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILanguageService _languageService;
        private readonly IStringLocalizer<ProductController> _localizer;

        public ProductController(IProductService productService, ILanguageService languageService, IStringLocalizer<ProductController> localizer)
        {
            _productService = productService;
            _languageService = languageService;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductViewModel> products = _productService.GetAllProductsViewModel();
            return View(products);
        }

        [Authorize]
        public IActionResult Admin()
        {
            return View(_productService.GetAllProductsViewModel().OrderByDescending(p => p.Id));
        }

        [Authorize]
        public ViewResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ProductViewModel product)
        {
            if (product.Name == null || string.IsNullOrWhiteSpace(product.Name))
            {
                ModelState.AddModelError(nameof(product.Name), _localizer["MissingName"]);
            }

            if (product.Price == null || string.IsNullOrWhiteSpace(product.Price))
            {
                ModelState.AddModelError(nameof(product.Price), _localizer["MissingPrice"]);
            }
            if (!decimal.TryParse(product.Price, out decimal price))
                {
                    ModelState.AddModelError(nameof(product.Price), _localizer["PriceNotANumber"]);
                }
                else if (price <= 0)
                {
                    ModelState.AddModelError(nameof(product.Price), _localizer["PriceNotGreaterThanZero"]);
                }

            if (product.Stock == null || string.IsNullOrWhiteSpace(product.Stock))
            {
                ModelState.AddModelError(nameof(product.Stock), _localizer["MissingStock"]);
            }

            if (!int.TryParse(product.Stock, out int qt))
            {
                ModelState.AddModelError(nameof(product.Stock), _localizer["StockNotAnInteger"]);
            }
            else
            {
                if (qt <= 0)
                    ModelState.AddModelError(nameof(product.Stock), _localizer["StockNotGreaterThanZero"]);
            }

            if (ModelState.IsValid)
            {
                _productService.SaveProduct(product);
                return RedirectToAction("Admin");
            }

            return View(product);
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Admin");
        }
    }
}