using System;
using System.Linq;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input
            var inputX = 1;
            var inputY = 2;
            var inputOrientation = 'E';
            var inputCommands = new[] { 'F', 'L', 'F', 'L', 'B', 'L', 'F', 'F', 'R', 'F', 'R', 'B', 'B', 'L', 'F' };
            var dimensionX = 5;
            var dimensionY = 5;


            // Setup
            var mars = new Mars(dimensionX, dimensionY)
                .CrashMeteor(2, 0)
                .CrashMeteor(0, 3)
                .CrashMeteor(4, 3);
            var startPosition = new Position(inputX, inputY, dimensionX - 1, dimensionY - 1);
            var startOrientation = (Orientation) inputOrientation;
            var curiosity = new Rover(startPosition, startOrientation, mars);
            var commands = inputCommands.Select(_ => (Command)_);
            var navigator = new Navigator(curiosity);

            // Do it
            var result = navigator.Navigate(commands);

            Console.WriteLine($"{nameof(Rover)} ended on position {result.Position}, facing {result.Orientation}. " +
                              $"He did {(result.Error ? "" : "not ")}encounter a problem.");
            Console.ReadKey();
        }
    }
}
