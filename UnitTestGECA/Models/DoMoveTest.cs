using GECA_Control.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGECA.Models
{
    public class DoMoveTest
    {
        [Theory]
        [InlineData("A")]
        [InlineData("B")]
        [InlineData("O")]
        [InlineData("K")]
        public void ControlKey_NotValidKeys_ReturnKey(string key)
        {
            //Arrange
            int value = 5;

            //Act && Assert
            var exception = Assert.Throws<ArgumentException>(() => new DoMove(key, value));

            //Assert
            Assert.Equal("One or more keys are invalid.", exception.Message);
        }
        [Theory]
        [InlineData("U")]
        [InlineData("D")]
        [InlineData("L")]
        [InlineData("R")]
        public void ControlKey_ValidKeys_ReturnKey(string key)
        {
            //Arrange
            int value = 5;

            //Act
            var doMove = new DoMove(key, value);

            //Assert
            Assert.Equal(key, doMove.Key);
        }
        [Fact]
        public void ControlKey_ValidKey_ReturnKey()
        {
            //Arrange
            string validKey = "U";
            int value = 5;

            //Act
            var doMove = new DoMove(validKey, value);

            //Assert
            Assert.Equal(validKey, doMove.Key);
        }
    }
}
