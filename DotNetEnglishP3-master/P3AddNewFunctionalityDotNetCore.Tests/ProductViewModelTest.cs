using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductViewModelTests
    {
        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>



        // This test validate that the name of the product is required by entering an empty name or a null value in the field
        [Theory]
        [InlineData(null, "MissingName")]
        [InlineData("", "MissingName")]
        public void Validate_Product_Name_Is_Not_Null_Or_Empty(string inputValue, string expectedValue)
        {
            //Arrange
            var product = new ProductViewModel
            {
                Name = inputValue,
            };
            var validationContext = new ValidationContext(product);
            var validationResults = new List<ValidationResult>();

            //Act
            bool isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

            //Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == expectedValue);
        }

        // This test validate that the price of the product is required by entering an empty price or a null value in the field
        [Theory]
        [InlineData(null, "MissingPrice")]
        [InlineData("", "MissingPrice")]
        public void Validate_Product_Price_Is_Not_Null_Or_Empty(string inputValue, string expectedValue)
        {
            //Arrange
            var product = new ProductViewModel
            {
                Price = inputValue,
            };
            var validationContext = new ValidationContext(product);
            var validationResults = new List<ValidationResult>();

            //Act
            bool isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

            //Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == expectedValue);
        }

        // This test validate that the stock number of the product is required by entering an empty stock number or a null value in the field
        [Theory]
        [InlineData(null, "MissingStock")]
        [InlineData("", "MissingStock")]
        public void Validate_Product_Stock_Is_Not_Null_Or_Empty(string inputValue, string expectedValue)
        {
            //Arrange
            var product = new ProductViewModel
            {
                Stock = inputValue,
            };
            var validationContext = new ValidationContext(product);
            var validationResults = new List<ValidationResult>();

            //Act
            bool isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

            //Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == expectedValue);
        }

        // This test validate that the price of the product is a float number with 2 digits
        // by trying to enter different type of values that are note in the ["number","two digits"]
        [Theory]
        [InlineData("not a number", "PriceNotANumber")]
        [InlineData("10.99", "PriceNotANumber")]
        [InlineData("100", "PriceNotANumber")]
        [InlineData("10,1", "PriceNotANumber")]
        public void Validate_Product_Price_Is_A_Number(string inputValue, string expectedValue)
        {
            //Arrange
            var product = new ProductViewModel
            {
                Price = inputValue,
            };
            var validationContext = new ValidationContext(product);
            var validationResults = new List<ValidationResult>();

            //Act
            bool isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

            //Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == expectedValue);
        }

        // This test validate that the price of the product is a float number with 2 digits
        // by trying to enter different type of values that are note an Integer
        [Theory]
        [InlineData("not a number", "StockNotAnInteger")]
        [InlineData("10.99", "StockNotAnInteger")]
        [InlineData("10,99", "StockNotAnInteger")]
        [InlineData("10,0", "StockNotAnInteger")]
        public void Validate_Product_Stock_Is_An_Integer(string inputValue, string expectedValue)
        {
            //Arrange
            var product = new ProductViewModel
            {
                Stock = inputValue,
            };
            var validationContext = new ValidationContext(product);
            var validationResults = new List<ValidationResult>();

            //Act
            bool isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

            //Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == expectedValue);
        }

        // This test validate that the price of the product is a greater than 0
        // by trying to enter a 0 value or a negative value
        [Theory]
        [InlineData("0", "PriceNotGreaterThanZero")]
        [InlineData("-1", "PriceNotGreaterThanZero")]
        public void Validate_Product_Price_Is_Greater_Than_Zero(string inputValue, string expectedValue)
        {
            //Arrange
            var product = new ProductViewModel
            {
                Price = inputValue,
            };
            var validationContext = new ValidationContext(product);
            var validationResults = new List<ValidationResult>();

            //Act
            bool isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

            //Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == expectedValue);
        }

        // This test validate that the stock of the product is a greater than 0
        // by trying to enter a 0 value or a negative value
        [Theory]
        [InlineData("0", "StockNotGreaterThanZero")]
        [InlineData("-1", "StockNotGreaterThanZero")]
        public void Validate_Product_Stock_Is_Greater_Than_Zero(string inputValue, string expectedValue)
        {
            //Arrange
            var product = new ProductViewModel
            {
                Stock = inputValue,
            };
            var validationContext = new ValidationContext(product);
            var validationResults = new List<ValidationResult>();

            //Act
            bool isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

            //Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == expectedValue);
        }

        [Theory]
        [InlineData("10,00")]
        [InlineData("99,99")]
        [InlineData("0,01")]
        public void Validate_Product_Price_Is_Valid(string inputValue)
        {
            // Arrange
            var product = new ProductViewModel
            {
                Price = inputValue,
                Name = "TestName",
                Stock = "10"
            };
            var validationContext = new ValidationContext(product) { MemberName = "Price" };
            var validationResults = new List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateProperty(product.Price, validationContext, validationResults);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("10")]
        [InlineData("99")]
        [InlineData("1")]
        public void Validate_Product_Stock_Is_Valid(string inputValue)
        {
            // Arrange
            var product = new ProductViewModel
            {
                Stock = inputValue,
                Name = "TestName",
                Price = "10,99"
            };
            var validationContext = new ValidationContext(product) { MemberName = "Stock" };
            var validationResults = new List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateProperty(product.Stock, validationContext, validationResults);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void Validate_Product_Is_Valid_With_All_Valid_Fields()
        {
            // Arrange
            var product = new ProductViewModel
            {
                Name = "Testing Product",
                Price = "10,99",
                Stock = "20",
                Description = "A valid description",
                Details = "Some details"
            };
            var validationContext = new ValidationContext(product);
            var validationResults = new List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateObject(product, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid);
        }
    }
}
