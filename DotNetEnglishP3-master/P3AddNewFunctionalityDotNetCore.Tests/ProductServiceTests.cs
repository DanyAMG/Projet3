using Xunit;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {
        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>
        [Fact]
        public void ExampleMethod()
        {
            // Arrange

            // Act


            // Assert
            Assert.Equal(1, 1);
        }

        // TODO write test methods to ensure a correct coverage of all possibilities


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

        [Theory]
        [InlineData("not a number", "PriceNotANumber")]
        [InlineData("10.99", "PriceNotANumber")]
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

        [Theory]
        [InlineData("not a number", "PriceNotAnInteger")]
        [InlineData("10.99", "PriceNotAnInteger")]
        [InlineData("10,99", "StockNotAnInteger")]
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

        [Theory]
        [InlineData("0", "StockNotGreaterThanZero")]
        [InlineData("-1", "StockNotGreaterThanZero")]
        public void Validate_Product_Stock_Is_Greater_Than_Zero(string inputValue, string expectedValue)
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
    }
}