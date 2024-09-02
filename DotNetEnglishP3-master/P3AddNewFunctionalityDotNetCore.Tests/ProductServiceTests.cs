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


        // This test validate that the name of the product is required by entering an empty name
        [Theory]
        [InlineData(null, "MissingName")]
        [InlineData("", "MissingName")]
        public void Validate_Product_Name(string inputValue, string expectedValue)
        {
            //Arrange
            var product = new ProductViewModel
            {
                Name = inputValue,
            };
            var validationContext = new ValidationContext(product);
            var validationResults = new List<ValidationResult>();

            //Act
            bool isValid =Validator.TryValidateObject(product, validationContext, validationResults, true);

            //Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == expectedValue);
        }
    }
}