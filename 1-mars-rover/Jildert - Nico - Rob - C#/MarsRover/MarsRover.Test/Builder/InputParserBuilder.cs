using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;

namespace MarsRover.Test.Builder
{
    public class InputParserBuilder
    {
        private IRover _rover;
        private IGrid _grid;

        public InputParserBuilder()
        {
            _rover = Substitute.For<IRover>();
            _grid = Substitute.For<IGrid>();
        }

        public InputParser Build()
        {
            return new InputParser(_rover, _grid);
        }

        public InputParserBuilder With(IRover rover)
        {
            _rover = rover;
            return this;
        }

        public InputParserBuilder With(Grid grid)
        {
            _grid = grid;
            return this;
        }
    }
}
