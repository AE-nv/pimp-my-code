using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Domain;
using System;

namespace Rover.Tests
{
    [TestClass]
    public class PlanetTests
    {
        [TestMethod]
        public void PlanetRangeCheck()
        {
            Assert.IsInstanceOfType(new Planet(1, 1), typeof(Planet));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Planet(0, 1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Planet(-1, 1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Planet(1, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Planet(1, -1));

            Planet planet = new Planet(4, 3);
            Assert.AreEqual(planet.GetSectors().Count,12);
        }
    }
}

