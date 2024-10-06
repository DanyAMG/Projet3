using Xunit;
using Moq;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {
        
        private readonly Mock<IStringLocalizer<ProductService>> _mockLocalizer;
        private readonly Mock<ICart> _mockCart;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IOrderRepository> _mockOrderRepository;

        public ProductServiceTests()
        {
            // Setup the mock to simulate the localizer, cart and repo
            _mockLocalizer = new Mock<IStringLocalizer<ProductService>>();
            _mockCart = new Mock<ICart>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockOrderRepository = new Mock<IOrderRepository>();

            // Configuration des valeurs de localisation
            _mockLocalizer.Setup(l => l["MissingName"]).Returns(new LocalizedString("MissingName", "Name is required"));
            _mockLocalizer.Setup(l => l["MissingPrice"]).Returns(new LocalizedString("MissingPrice", "Price is required"));
            _mockLocalizer.Setup(l => l["PriceNotANumber"]).Returns(new LocalizedString("PriceNotANumber", "Price must be a number"));
            _mockLocalizer.Setup(l => l["PriceNotGreaterThanZero"]).Returns(new LocalizedString("PriceNotGreaterThanZero", "Price must be greater than zero"));
            _mockLocalizer.Setup(l => l["MissingQuantity"]).Returns(new LocalizedString("MissingQuantity", "Quantity is required"));
            _mockLocalizer.Setup(l => l["StockNotAnInteger"]).Returns(new LocalizedString("StockNotAnInteger", "Stock must be an integer"));
            _mockLocalizer.Setup(l => l["StockNotGreaterThanZero"]).Returns(new LocalizedString("StockNotGreaterThanZero", "Stock must be greater than zero"));
        }

        [Fact]
        public void CheckProductModelErrors_Should_Return_MissingName_Error_When_Name_Is_Null()
        {
            // Arrange
            var product = new ProductViewModel { Name = null, Price = "10.99", Stock = "5" };
            var productService = new ProductService(_mockCart.Object, _mockProductRepository.Object, _mockOrderRepository.Object, _mockLocalizer.Object);

            // Act
            var result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("Name is required", result);
        }

        [Fact]
        public void CheckProductModelErrors_Should_Return_MissingPrice_Error_When_Price_Is_Null()
        {
            // Arrange
            var product = new ProductViewModel { Name = "Valid Name", Price = null, Stock = "5" };
            var productService = new ProductService(_mockCart.Object, _mockProductRepository.Object, _mockOrderRepository.Object, _mockLocalizer.Object);

            // Act
            var result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("Price is required", result);
        }

        [Fact]
        public void CheckProductModelErrors_Should_Return_PriceNotANumber_Error_When_Price_Is_Not_A_Number()
        {
            // Arrange
            var product = new ProductViewModel { Name = "Valid Name", Price = "invalid", Stock = "5" };
            var productService = new ProductService(_mockCart.Object, _mockProductRepository.Object, _mockOrderRepository.Object, _mockLocalizer.Object);

            // Act
            var result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("Price must be a number", result);
        }

        [Fact]
        public void CheckProductModelErrors_Should_Return_PriceNotGreaterThanZero_Error_When_Price_Is_Zero()
        {
            // Arrange
            var product = new ProductViewModel { Name = "Valid Name", Price = "0", Stock = "5" };
            var productService = new ProductService(_mockCart.Object, _mockProductRepository.Object, _mockOrderRepository.Object, _mockLocalizer.Object);

            // Act
            var result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("Price must be greater than zero", result);
        }

        [Fact]
        public void CheckProductModelErrors_Should_Return_MissingQuantity_Error_When_Stock_Is_Null()
        {
            // Arrange
            var product = new ProductViewModel { Name = "Valid Name", Price = "10.99", Stock = null };
            var productService = new ProductService(_mockCart.Object, _mockProductRepository.Object, _mockOrderRepository.Object, _mockLocalizer.Object);

            // Act
            var result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("Quantity is required", result);
        }

        [Fact]
        public void CheckProductModelErrors_Should_Return_StockNotAnInteger_Error_When_Stock_Is_Not_An_Integer()
        {
            // Arrange
            var product = new ProductViewModel { Name = "Valid Name", Price = "10,99", Stock = "10,99" };
            var productService = new ProductService(_mockCart.Object, _mockProductRepository.Object, _mockOrderRepository.Object, _mockLocalizer.Object);

            // Act
            var result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("Stock must be an integer", result);
        }

        [Fact]
        public void CheckProductModelErrors_Should_Return_StockNotGreaterThanZero_Error_When_Stock_Is_Zero()
        {
            // Arrange
            var product = new ProductViewModel { Name = "Valid Name", Price = "10,99", Stock = "0" };
            var productService = new ProductService(_mockCart.Object, _mockProductRepository.Object, _mockOrderRepository.Object, _mockLocalizer.Object);

            // Act
            var result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("Stock must be greater than zero", result);
        }

        [Fact]
        public void CheckProductModelErrors_Should_Return_No_Errors_For_Valid_Product()
        {
            // Arrange
            var product = new ProductViewModel { Name = "Valid Name", Price = "10,99", Stock = "5" };
            var productService = new ProductService(_mockCart.Object, _mockProductRepository.Object, _mockOrderRepository.Object, _mockLocalizer.Object);

            // Act
            var result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Empty(result);
        }
    }

}
