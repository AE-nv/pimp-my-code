using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace MarsRover
{
    public class Navigator
    {
        private readonly Rover _rover;

        public Navigator(Rover rover)
        {
            _rover = rover;
        }

        public (Position Position, Orientation Orientation, bool Error) Navigate(IEnumerable<Command> commands)
        {
            var encounteredProblem = commands.Any(command => ProcessCommand(command).IsFailure);
            return (_rover.Position, _rover.Orientation, encounteredProblem);
        }

        public Result ProcessCommand(Command command)
        {
            switch (command)
            {
                case Command.DriveForward:
                    return _rover.DriveForward();
                case Command.DriveBackward:
                    return _rover.DriveBackward();
                case Command.TurnLeft:
                    return _rover.TurnLeft();
                case Command.TurnRight:
                    return _rover.TurnRight();
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}