using Rover.Api;
using Rover.Domain;
using System;

namespace Rover.ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // No obstacles scenario
            Console.WriteLine("*** No obstacles ***");
            new RoverControl().Navigate(new Planet(5, 5), 1, 2, 'E', "FLFLBLFFRFRBBLF".ToCharArray(),
                (int X, int Y, char direction) => Console.WriteLine("{0}-{1}-{2}", X, Y, direction), // final location
                (string location) => Console.WriteLine(location) // intermediate locations
            );
            Console.ReadLine();
            Console.Clear();


            // Obstacles scenario
            Console.WriteLine("*** Obstacles ***");
            Planet planet = new Planet(5, 5)
                .Block(2, 0)
                .Block(0, 3)
                .Block(4, 3);
            new RoverControl().Navigate(planet, 1, 2, 'E', "FLFLBLFFRFRBBLF".ToCharArray(),
                (int X, int Y, char direction) => Console.WriteLine("{0}-{1}-{2}", X, Y, direction), // final location
                (string location) => Console.WriteLine(location) // intermediate locations
            );

            Console.ReadLine();
        }
    }
}
