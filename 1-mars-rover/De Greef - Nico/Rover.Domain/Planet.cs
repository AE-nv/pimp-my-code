using System;
using System.Collections.Generic;
using System.Linq;

namespace Rover.Domain
{
    public class Planet
    {
        private List<Sector> _sectors = new List<Sector>();

        public class Sector
        {
            public bool IsBlocked { get; internal set; }
            public int X { get; internal set; }
            public int Y { get; internal set; }
        }

        // Discussable if throwing exceptions in a ctor is a bad practice
        public Planet(int xSectors, int ySectors)
        {
            if (xSectors < 1) throw new ArgumentOutOfRangeException("xSectors", "xSectors must be at least 1");
            if (ySectors < 1) throw new ArgumentOutOfRangeException("ySectors", "ySectors must be at least 1");

            for (int x = 0; x < xSectors; x++)
            {
                for (int y = 0; y < ySectors; y++)
                {
                    _sectors.Add(new Sector() { X = x, Y = y, IsBlocked = false });
                }
            }
        }

        public IReadOnlyCollection<Sector> GetSectors ()
        {
            return _sectors.AsReadOnly();
        }

        public Sector GetSector(int x, int y)
        {
            return _sectors.Single(s => s.X == x && s.Y == y);
        }

        public Planet Block(int x, int y)
        {
            GetSector(x, y).IsBlocked = true;
            return this;
        }

        public int MaxX()
        {
            return _sectors.Max((s) => s.X);
        }

        public int MaxY()
        {
            return _sectors.Max((s) => s.Y);
        }
    }
}
