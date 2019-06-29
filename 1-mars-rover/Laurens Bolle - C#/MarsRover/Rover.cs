using CSharpFunctionalExtensions;
using EnumsNET;
using MarsRover.Util;

namespace MarsRover
{
    public class Rover
    {
        public Position Position { get; private set; }

        public Orientation Orientation { get; private set; }

        private readonly Mars _mars;

        public Rover(Position position, Orientation orientation, Mars mars)
        {
            Position = position;
            Orientation = orientation;
            _mars = mars;
        }

        public Result DriveForward()
        {
            return Move(Orientation);
        }

        public Result DriveBackward()
        {
            var reverseOrientation = Orientation.GetAttributes().Get<ReverseAttribute>().Orientation;
            return Move(reverseOrientation);
        }

        public Result TurnLeft()
        {
            var leftOrientation = Orientation.GetAttributes().Get<LeftAttribute>().Orientation;
            Orientation = leftOrientation;
            return Result.Ok();
        }

        public Result TurnRight()
        {
            var rightOrientation = Orientation.GetAttributes().Get<RightAttribute>().Orientation;
            Orientation = rightOrientation;
            return Result.Ok();
        }

        private Result Move(Orientation orientation)
        {
            var newPosition = Position.GetNeighbouringPosition(orientation);
            if (_mars.HasObstacle(newPosition))
            {
                return Result.Fail($"Obstacle detected on position {newPosition}.");
            }

            Position = newPosition;
            return Result.Ok();
        }
    }
}