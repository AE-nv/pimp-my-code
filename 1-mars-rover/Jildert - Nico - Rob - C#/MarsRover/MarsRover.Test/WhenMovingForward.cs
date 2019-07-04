using System;
using NUnit.Framework;

namespace MarsRover.Test
{
    [TestFixture]
    public class WhenMovingForward
    {
        private Rover _rover;
        private Action<(int X, int Y)> _hasMovedTo;

        [SetUp]
        public void Setup()
        {
            _rover = new Rover();

            _hasMovedTo = tuple =>
            {
                if (tuple != (0, 1)) { throw new InvalidOperationException(); }
            };
        }

        [Test]
        public void ThenTheNewLocationIsCorrect()
        {
            Assert.DoesNotThrow(WhenMoving);
        }

        private void WhenMoving()
        {
            _rover.MoveForward(tuple => true, _hasMovedTo);
        }
    }
}
