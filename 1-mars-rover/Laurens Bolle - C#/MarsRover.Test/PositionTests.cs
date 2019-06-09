using Xunit;

namespace MarsRover.Test
{
    public class PositionTests
    {
        [Theory]
        [InlineData(0, 0, Orientation.North, 0, 1)]
        [InlineData(0, 0, Orientation.East, 1, 0)]
        [InlineData(0, 0, Orientation.South, 0, 4)]
        [InlineData(0, 0, Orientation.West, 4, 0)]
        [InlineData(2, 1, Orientation.East, 3, 1)]
        [InlineData(4, 1, Orientation.South, 4, 0)]
        [InlineData(2, 0, Orientation.South, 2, 4)]
        public void Test1(int x, int y, Orientation orientation, int expectedX, int expectedY)
        {
            // Arrange
            var sut = new Position(x, y, 4, 4);

            // Act
            var result = sut.GetNeighbouringPosition(orientation);

            // Assert
            Assert.Equal(expectedX, result.X);
            Assert.Equal(expectedY, result.Y);
        }
    }
}
