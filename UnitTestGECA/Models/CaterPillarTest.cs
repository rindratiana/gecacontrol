using GECA_Control.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGECA.Models
{
    public class CaterPillarTest
    {
        [Theory]
        [InlineData(3, 5, 3, 4, "R", 1, 4, 5, 3, 4)] //Move Right 1 step case head on the tail
        [InlineData(3, 3, 3, 4, "R", 1, 4, 3, 3, 4)] //Move Right 1 step case head under the tail
        [InlineData(4, 5, 3, 4, "R", 1, 5, 5, 4, 5)] //Move Right 1 step case head on and right the tail
        [InlineData(4, 3, 3, 4, "R", 1, 5, 3, 4, 3)] //Move Right 1 step case head under and right the tail
        [InlineData(2, 3, 3, 4, "R", 4, 6, 3, 5, 3)] //Move Right more step case head under and right the tail
        [InlineData(2, 1, 3, 2, "U", 1, 2, 2, 3, 2)] //Move Up 1 step head under Tail left
        [InlineData(4, 1, 3, 2, "U", 1, 4, 2, 3, 2)] //Move Up 1 step head under Tail right
        [InlineData(2, 2, 3, 2, "U", 1, 2, 3, 3, 2)] //Move Up 1 step head same Tail Y but X left
        [InlineData(4, 2, 3, 2, "U", 1, 4, 3, 3, 2)] //Move Up 1 step head same Tail Y but X right
        [InlineData(2, 1, 3, 2, "U", 5, 2, 6, 2, 5)] //Move more step
        [InlineData(3, 4, 4, 4, "L", 1, 2, 4, 3, 4)] //Move left same line
        [InlineData(2, 4, 3, 3, "L", 2, 0, 4, 1, 4)] //Move left diagonal
        [InlineData(3, 2, 2, 3, "L", 1, 2, 2, 2, 3)] //Move left diagonal 1 step
        [InlineData(6, 6, 5, 5, "D", 1, 6, 5, 5, 5)] //Move down only 1 step
        [InlineData(6, 5, 5, 5, "D", 1, 6, 4, 5, 5)] //Move down only 1 step
        [InlineData(6, 6, 5, 5, "D", 3, 6, 3, 6, 4)] //Move down > 1 step
        [InlineData(1, 4, 2, 5, "D", 2, 1, 2, 1, 3)] //Move down > 1 linear
        [InlineData(4, 3, 4, 2, "D", 2, 4, 1, 4, 2)] //Move down > 1 superposition
        [InlineData(4, 3, 4, 2, "D", 1, 4, 2, 4, 2)] //Move down 1 superposition head and tail
        public void Move_ValidMove_UpdatesPosition(int headX, int headY, int tailX, int tailY,string commandKey, int commandValue, int headXExpect, int headYExpect, int tailXExpect, int tailYExpect)
        {
            //Arrange
            Caterpillar caterpillar = new Caterpillar
            {
                Head = new Coordinates(headX, headY, 'H'),
                Tail = new Coordinates(tailX, tailY, 'T')
            };
            DoMove move = new DoMove(commandKey, commandValue);

            //Act
            caterpillar.Move(move);
            //Assert
            Assert.Equal(caterpillar.Head.X, headXExpect);
            Assert.Equal(caterpillar.Head.Y, headYExpect);
            Assert.Equal(caterpillar.Tail.X, tailXExpect);
            Assert.Equal(caterpillar.Tail.Y, tailYExpect);
        }

        [Theory]
        [InlineData(2, 3, 3, 2, true)]  // Head.X < Tail.X, Head.Y > Tail.Y diagonal up left
        [InlineData(2, 4, 3, 2, false)] // Head.X < Tail.X, Head.Y > Tail.Y diagonal up left far
        [InlineData(4, 3, 3, 2, true)]  // Head.X > Tail.X, Head.Y > Tail.Y diagonal up right
        [InlineData(5, 6, 3, 4, false)]  // Head.X > Tail.X, Head.Y > Tail.Y diagonal up right far
        [InlineData(2, 1, 3, 2, true)] // Head.X < Tail.X, Head.Y < Tail.Y diagonal down left
        [InlineData(2, 1, 4, 5, false)] // Head.X < Tail.X, Head.Y < Tail.Y diagonal down left far
        [InlineData(4, 1, 3, 2, true)] // Head.X > Tail.X, Head.Y < Tail.Y diagonal down right
        [InlineData(5, 1, 2, 6, false)] // Head.X > Tail.X, Head.Y < Tail.Y diagonal down right far
        public void Control_IsDiagonal_ReturnsExpectedResult(int headX, int headY, int tailX, int tailY, bool expected)
        {
            // Arrange
            var caterpillar = new Caterpillar
            {
                Head = new Coordinates (headX,headY,'*'),
                Tail = new Coordinates (tailX, tailY, '*')
            };

            // Act
            bool result = caterpillar.IsDiagonal();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
