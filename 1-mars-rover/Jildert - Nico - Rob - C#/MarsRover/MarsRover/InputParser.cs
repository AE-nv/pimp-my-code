using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover
{
    public class InputParser
    {
        private readonly IRover _rover;
        private readonly IGrid _grid;

        public InputParser(IRover rover, IGrid grid)
        {
            _rover = rover;
            _grid = grid;
        }

        public void Parse(string command)
        {
            if (String.IsNullOrWhiteSpace(command))
            {
                return;
            }

            var commands = command.Split(",");
            foreach (var c in commands)
            {
                switch (c)
                {
                    case "F":
                        _rover.MoveForward(tuple => !_grid.IsBlocked(tuple) && !_grid.IsOutOfRange(tuple),
                            tuple => { });
                        break;
                    case "B": _rover.MoveBackward(tuple => !_grid.IsBlocked(tuple) && !_grid.IsOutOfRange(tuple),
                            tuple => { });
                        break;
                    case "L": _rover.TurnLeft(tuple => { });
                        break;
                    case "R": _rover.TurnRight(tuple => { });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(c));
                }
            }
        }
    }
}
