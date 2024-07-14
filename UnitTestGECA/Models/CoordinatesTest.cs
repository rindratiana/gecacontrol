using GECA_Control.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnitTestGECA.Models
{
    public class CoordinatesTest
    {
        [Theory]
        [InlineData(-4, 5)]
        [InlineData(-3, 5)]
        [InlineData(-10, 5)]
        [InlineData(-1, 5)]
        public void Coordinates_InvalidXValue_Exception(int x, int y)
        {
            // Arrange, Act & Assert
            var exceptionX = Assert.Throws<ArgumentException>(() => new Coordinates(x, y,' '));
            Assert.EndsWith("X must be a positive number.", exceptionX.Message);
        }

        [Theory]
        [InlineData(5, -36)]
        [InlineData(5, -2)]
        [InlineData(5, -5)]
        public void Coordinates_InvalidYValue_Exception(int x, int y)
        {
            // Arrange, Act & Assert
            var exceptionY = Assert.Throws<ArgumentException>(() => new Coordinates(x, y, ' '));
            Assert.EndsWith("Y must be a positive number.", exceptionY.Message);
        }

        [Theory]
        [InlineData(2,3)]
        [InlineData(6,4)]
        [InlineData(0,3)]
        [InlineData(21,65)]
        [InlineData(9,0)]
        [InlineData(3,5)]
        public void Coordinates_ValidValuesWithTheory_Success(int x, int y)
        {
            // Arrange & Act
            var coordinates = new Coordinates(x, y, ' ');

            // Assert
            Assert.Equal(x, coordinates.X);
            Assert.Equal(y, coordinates.Y);
        }

        [Fact]
        public void Coordinates_ValidValues_Success()
        {
            // Arrange & Act
            var coordinates = new Coordinates(3, 5, ' ');

            // Assert
            Assert.Equal(3, coordinates.X);
            Assert.Equal(5, coordinates.Y);
        }
    }
}
