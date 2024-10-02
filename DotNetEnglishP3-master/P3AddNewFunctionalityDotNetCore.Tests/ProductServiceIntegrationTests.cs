using Xunit;
using Moq;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System.Collections.Generic;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models;
using System.Linq;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceIntegrationTests
    {
        private readonly Mock<IStringLocalizer<ProductService>> _mockLocalizer;
        private readonly ProductService _productService;

        public ProductServiceIntegrationTests()
        {
            // Mock the IStringLocalizer to return specific localized strings for error messages
            _mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            // Setup the mock to return values for specific keys
            _mockLocalizer.Setup(l => l["MissingName"]).Returns(new LocalizedString("MissingName", "Name is required"));
            _mockLocalizer.Setup(l => l["MissingPrice"]).Returns(new LocalizedString("MissingPrice", "Price is required"));
            _mockLocalizer.Setup(l => l["PriceNotANumber"]).Returns(new LocalizedString("PriceNotANumber", "Price must be a number"));
            _mockLocalizer.Setup(l => l["PriceNotGreaterThanZero"]).Returns(new LocalizedString("PriceNotGreaterThanZero", "Price must be greater than zero"));
            _mockLocalizer.Setup(l => l["MissingQuantity"]).Returns(new LocalizedString("MissingQuantity", "Quantity is required"));
            _mockLocalizer.Setup(l => l["StockNotAnInteger"]).Returns(new LocalizedString("StockNotAnInteger", "Stock must be an integer"));
            _mockLocalizer.Setup(l => l["StockNotGreaterThanZero"]).Returns(new LocalizedString("StockNotGreaterThanZero", "Stock must be greater than zero"));

            // Initialize the ProductService with the mocked localizer
            _productService = new ProductService(null, null, null, _mockLocalizer.Object);
        }

        [Fact]
        public void CheckProductModelErrors_Should_Return_Errors_For_Invalid_Product()
        {
            // Arrange: Create a ProductViewModel with invalid data
            var invalidProduct = new ProductViewModel
            {
                Name = "", // Invalid (required)
                Price = "abc", // Invalid (not a number)
                Stock = "-10" // Invalid (negative number)
            };

            // Act: Call CheckProductModelErrors
            var result = _productService.CheckProductModelErrors(invalidProduct);

            // Assert: Check if the correct errors are returned
            Assert.Contains("Name is required", result);
            Assert.Contains("Price must be a number", result);
            Assert.Contains("Stock must be greater than zero", result);
        }

        [Fact]
        public void CheckProductModelErrors_Should_Return_No_Errors_For_Valid_Product()
        {
            // Arrange: Create a ProductViewModel with valid data
            var validProduct = new ProductViewModel
            {
                Name = "Valid Product",
                Price = "10,99", // Valid price
                Stock = "5" // Valid stock
            };

            // Act: Call CheckProductModelErrors
            var result = _productService.CheckProductModelErrors(validProduct);

            // Assert: No errors should be returned
            Assert.Empty(result);
        }
    }
    public class ProductServiceReflectClientsIntegrationTest
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ICart> _mockCart;
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly ProductService _productService;

        public ProductServiceReflectClientsIntegrationTest()
        {
            // Mocks initialization
            _mockProductRepository = new Mock<IProductRepository>();
            _mockCart = new Mock<ICart>();
            _mockOrderRepository = new Mock<IOrderRepository>();

            // Initialise ProductService avec des mocks
            // Initialize ProductService with mocks
            _productService = new ProductService(_mockCart.Object, _mockProductRepository.Object, _mockOrderRepository.Object, null);
        }

        [Fact]
        public void Product_Creation_Should_Be_Reflected_For_Client()
        {
            // Arrange
            var newProduct = new ProductViewModel
            {
                Id = 1,
                Name = "New Product",
                Price = "20,00",
                Stock = "10"
            };

            
            // Simulate the product Creation in the repo
            var productList = new List<Product>();
            _mockProductRepository.Setup(repo => repo.GetAllProducts()).Returns(productList);
            _mockProductRepository.Setup(repo => repo.SaveProduct(It.IsAny<Product>())).Callback<Product>(p => productList.Add(p));

            // Act:
            //The administrator add a new product
            _productService.SaveProduct(newProduct);

           
            // Take the product list available for the client
            var allProducts = _productService.GetAllProducts();

            // Assert
            Assert.NotNull(allProducts);
            Assert.Contains(allProducts, p => p.Name == "New Product" && p.Price == 20 && p.Quantity == 10);
        }
    }
}