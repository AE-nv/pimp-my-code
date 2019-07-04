using System;

namespace MarsRover
{
    public interface IRover
    {
        void MoveBackward(Func<(int, int), bool> isValid, Action<(int, int)> hasMovedTo);
        void MoveForward(Func<(int, int), bool> isValid, Action<(int, int)> hasMovedTo);
        void TurnLeft(Action<Direction> turnedTo);
        void TurnRight(Action<Direction> turnedTo);
    }
}