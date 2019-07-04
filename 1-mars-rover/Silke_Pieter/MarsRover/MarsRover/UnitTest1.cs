using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace MarsRover
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var grid = new Grid(1, 2, Heading.E);

            var commands = new[]
            {
                Command.F,
                Command.L,
                Command.F,
                Command.L,
                Command.B,
                Command.L,
                Command.F,
                Command.F,
                Command.R,
                Command.F,
                Command.R,
                Command.B,
                Command.B,
                Command.L,
                Command.F,
            };
            var result = grid.move(commands);
            Assert.Equal(result.X, 1);
            Assert.Equal(result.Y, 4);
            Assert.Equal(result.Heading, Heading.W);
        }
    }

    internal class Grid
    {
        public Grid(int x, int y, Heading heading)
        {
            this.X = (x+5) % 5;
            this.Y = (y+5) % 5;
            this.state = this.CreateState(heading);
            this.Heading = heading;
        }

        public override string ToString()
        {
            return $"{X} {Y} {Heading}";
        }

        private State CreateState(Heading e)
        {
            switch (e)
            {
                case Heading.N:
                    return new North();
                case Heading.E:
                    return new East();
                case Heading.S:
                    return new South();
                case Heading.W:
                    return new West();
                default:
                    throw new Exception();
            }
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public Heading Heading { get; private set; }

        private State state;

        public Grid move(Command[] commands)
        {
            Grid result = this;

            foreach (var command in commands)
            {
                var newResult = result.state.Process(command, result);
                result = newResult;
                Debug.WriteLine($"{command}: {result}");
            }

            return result;
        }

        private Grid process(Command command)
        {
            var result = this.state.Process(command, this);

            return result;
        }

        abstract class State
        {
            public abstract Grid Process(Command command, Grid grid);
        }

        class North : State
        {
            public override Grid Process(Command command, Grid grid)
            {
                switch (command)
                {
                    case Command.F:
                        return  new Grid(grid.X, grid.Y + 1, Heading.N);
                    case Command.B:
                        return new Grid(grid.X, grid.Y - 1, Heading.N);
                    case Command.L:
                        return  new Grid(grid.X, grid.Y, Heading.W);
                    case Command.R:
                        return  new Grid(grid.X, grid.Y, Heading.E);
                    default:
                        throw new Exception();

                }
            }
        }
        class West : State
        {
            public override Grid Process(Command command, Grid grid)
            {
                switch (command)
                {
                    case Command.F:
                        return new Grid(grid.X -1, grid.Y, Heading.W);
                    case Command.B:
                        return new Grid(grid.X + 1, grid.Y, Heading.W);
                    case Command.L:
                        return new Grid(grid.X, grid.Y, Heading.S);
                    case Command.R:
                        return new Grid(grid.X, grid.Y, Heading.N);
                    default:
                        throw new Exception();

                }
            }
        }

        class East : State
        {
            public override Grid Process(Command command, Grid grid)
            {
                switch (command)
                {
                    case Command.F:
                        return new Grid(grid.X + 1, grid.Y, Heading.E);
                    case Command.B:
                        return new Grid(grid.X - 1, grid.Y, Heading.E);
                    case Command.L:
                        return new Grid(grid.X, grid.Y, Heading.N);
                    case Command.R:
                        return new Grid(grid.X, grid.Y, Heading.S);
                    default:
                        throw new Exception();

                }
            }
        }

        class South : State
        {
            public override Grid Process(Command command, Grid grid)
            {
                switch (command)
                {
                    case Command.F:
                        return new Grid(grid.X, grid.Y - 1, Heading.S);
                    case Command.B:
                        return new Grid(grid.X, grid.Y + 1, Heading.S);
                    case Command.L:
                        return new Grid(grid.X, grid.Y, Heading.E);
                    case Command.R:
                        return new Grid(grid.X, grid.Y, Heading.W);
                    default:
                        throw new Exception();

                }
            }
        }
    }


    public enum Heading
    {
        N, E, S, W
    }

    public enum Command
    {
        F, B, L, R
    }

}
