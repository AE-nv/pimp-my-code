using System;
using AutoFixture;
using MarsRover.Test.Builder;
using NUnit.Framework;

namespace MarsRover.Test
{
    public class WhenParsingInvalidCommands
    {
        private string _command;
        private InputParser _inputParser;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _inputParser = new InputParserBuilder().Build();
            _command = fixture.Create<string>();
        }

        [Test]
        public void ThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentOutOfRangeException>(WhenParsingCommand);
        }

        private void WhenParsingCommand()
        {
            _inputParser.Parse(_command);
        }
    }
}