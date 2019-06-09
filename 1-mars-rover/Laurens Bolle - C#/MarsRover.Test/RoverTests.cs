using Xunit;

namespace MarsRover.Test
{
    public class RoverTests
    {
        [Theory]
        [InlineData(Orientation.North, Orientation.East)]
        [InlineData(Orientation.East, Orientation.South)]
        [InlineData(Orientation.South, Orientation.West)]
        [InlineData(Orientation.West, Orientation.North)]
        public void TurnRightTurnsClockwise(Orientation orientation, Orientation expected)
        {
            // Arrange
            var sut = new Rover(null, orientation, null);

            // Act
            sut.TurnRight();

            // Assert
            Assert.Equal(expected, sut.Orientation);
        }

        [Theory]
        [InlineData(Orientation.North, Orientation.West)]
        [InlineData(Orientation.East, Orientation.North)]
        [InlineData(Orientation.South, Orientation.East)]
        [InlineData(Orientation.West, Orientation.South)]
        public void TurnLeftTurnsCounterClockwise(Orientation orientation, Orientation expected)
        {
            // Arrange
            var sut = new Rover(null, orientation, null);

            // Act
            sut.TurnLeft();

            // Assert
            Assert.Equal(expected, sut.Orientation);
        }

        [Fact]
        public void MovingToAnObstacleFails()
        {
            // Arrange
            var planetDimension = 3;
            var mars = new Mars(planetDimension, planetDimension)
                .CrashMeteor(0, 1);
            var startPosition = new Position(0, 0, planetDimension - 1, planetDimension - 1);
            var sut = new Rover(startPosition, Orientation.North, mars);

            // Act
            var result = sut.DriveForward();

            // Assert
            Assert.True(result.IsFailure);
        }
    }
}