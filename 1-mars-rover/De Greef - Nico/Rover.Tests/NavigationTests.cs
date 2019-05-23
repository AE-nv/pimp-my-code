using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rover.Api;
using Rover.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Tests
{
    [TestClass]
    public class NavigationTests
    {
        [TestMethod]
        public void NavigateParams()
        {
            new RoverControl().Navigate(new Domain.Planet(3, 3), 1, 1, 'S', "FBLR".ToCharArray(), null, null); // Normal
            Assert.ThrowsException<ApplicationException>(() => new RoverControl().Navigate(new Domain.Planet(3, 3), 3, 1, 'S', "FBLR".ToCharArray(), null, null)); // X out of range
            Assert.ThrowsException<ApplicationException>(() => new RoverControl().Navigate(new Domain.Planet(3, 3), 1, 3, 'S', "FBLR".ToCharArray(), null, null)); // Y out of range
            Assert.ThrowsException<ApplicationException>(() => new RoverControl().Navigate(new Domain.Planet(3, 3).Block(1, 2), 1, 2, 'S', "FBLR".ToCharArray(), null, null)); // Start on blocked sector
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new RoverControl().Navigate(new Domain.Planet(3, 3), 1, 1, 'A', "FBLR".ToCharArray(), null, null)); // Invalid direction
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new RoverControl().Navigate(new Domain.Planet(3, 3), 1, 1, 'A', "FBLR".ToCharArray(), null, null)); // Invalid movements

        }

        [TestMethod]
        public void NavigateScenarios()
        {
            // no obstacles
            new RoverControl().Navigate(new Planet(5, 5), 1, 2, 'E', "FLFLBLFFRFRBBLF".ToCharArray(),
                (int X, int Y, char direction) => Assert.AreEqual(string.Format("{0}-{1}-{2}", X, Y, direction), "1-4-W"), // final location
                null); // intermediate locations

            //obstacles
            Planet planet = new Planet(5, 5)
                .Block(2, 0)
                .Block(0, 3)
                .Block(4, 3);
            new RoverControl().Navigate(planet, 1, 2, 'E', "FLFLBLFFRFRBBLF".ToCharArray(),
                (int X, int Y, char direction) => Assert.AreEqual(string.Format("{0}-{1}-{2}", X, Y, direction), "2-1-N"), // final location
                null); // intermediate locations
        }
    }
}
