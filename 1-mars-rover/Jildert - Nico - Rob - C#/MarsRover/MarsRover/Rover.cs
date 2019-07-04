using System;

namespace MarsRover
{
    public class Rover : IRover
    {
        private (int X, int Y) _location;
        private Direction _orientation;

        public Rover()
        : this((0,0), Direction.North)
        {
        }

        public Rover((int, int) location, Direction orientation)
        {
            _location = location;
            _orientation = orientation;
        }
        
        public void MoveBackward(Func<(int, int), bool> isValid, Action<(int, int)> hasMovedTo)
        {
            Move(isValid, hasMovedTo, -1);
        }

        private void Move(Func<(int, int), bool> isValid, Action<(int, int)> hasMovedTo, int amount)
        {
            (int, int) newPosition;
            switch (_orientation)
            {
                case Direction.East:
                    newPosition = (_location.X - amount, _location.Y);
                    break;
                case Direction.North:
                    newPosition = (_location.X, _location.Y + amount);
                    break;
                case Direction.South:
                    newPosition = (_location.X, _location.Y - amount);
                    break;
                case Direction.West:
                    newPosition = (_location.X + amount, _location.Y);
                    break;
                default:
                    throw new NotImplementedException($"The direction ('{_orientation}') is not yet implemented.");
            }

            if (!(isValid?.Invoke(newPosition)).GetValueOrDefault(true))
            {
                throw new InvalidOperationException("The rover cannot move to this position.");
            }

            _location = newPosition;
            hasMovedTo(newPosition);
        }

        public void MoveForward(Func<(int, int), bool> isValid, Action<(int, int)> hasMovedTo)
        {
            Move(isValid, hasMovedTo, 1);
        }

        public void TurnLeft(Action<Direction> turnedTo)
        {
            switch (_orientation)
            {
                case Direction.East:
                    _orientation = Direction.North;
                    break;
                case Direction.North:
                    _orientation = Direction.West;
                    break;
                case Direction.South:
                    _orientation = Direction.East;
                    break;
                case Direction.West:
                    _orientation = Direction.South;
                    break;
                default:
                    throw new NotImplementedException($"The direction ('{_orientation}') is not yet implemented.");
            }

            turnedTo(_orientation);
        }

        public void TurnRight(Action<Direction> turnedTo)
        {
            switch (_orientation)
            {
                case Direction.East:
                    _orientation = Direction.South;
                    break;
                case Direction.North:
                    _orientation = Direction.East;
                    break;
                case Direction.South:
                    _orientation = Direction.West;
                    break;
                case Direction.West:
                    _orientation = Direction.North;
                    break;
                default:
                    throw new NotImplementedException($"The direction ('{_orientation}') is not yet implemented.");
            }

            turnedTo(_orientation);
        }
    }
}
