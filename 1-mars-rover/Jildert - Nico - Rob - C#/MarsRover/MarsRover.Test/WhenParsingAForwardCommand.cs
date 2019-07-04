using System;
using MarsRover.Test.Builder;
using NSubstitute;
using NUnit.Framework;

namespace MarsRover.Test
{
    public class WhenParsingAForwardCommand
    {
        private string _command;
        private InputParser _inputParser;
        private IRover _rover;

        [SetUp]
        public void Setup()
        {
            _rover = Substitute.For<IRover>();
            _inputParser = new InputParserBuilder().With(_rover).Build();
            _command = "F";
        }

        [Test]
        public void ThenNoExceptionIsThrown()
        {
            Assert.DoesNotThrow(WhenParsingCommand);
        }

        [Test]
        public void ThenTheRoverMovesForward()
        {
            WhenParsingCommand();

            _rover.Received(1).MoveForward(Arg.Any<Func<(int, int), bool>>(), Arg.Any<Action<(int, int)>>());
        }

        private void WhenParsingCommand()
        {
            _inputParser.Parse(_command);
        }
    }
}