using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace MarsRover
{
    public class Position : ValueObject
    {
        public int X { get; }
        public int Y { get; }

        private readonly int _maxX;
        private readonly int _maxY;

        public Position(int x, int y, int maxX, int maxY)
        {
            X = x;
            Y = y;
            _maxX = maxX;
            _maxY = maxY;
        }

        public Position GetNeighbouringPosition(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.North:
                    return new Position(X, (Y + 1) % _maxY, _maxX, _maxY);
                case Orientation.East:
                    return new Position((X + 1) % _maxX, Y, _maxX, _maxY);
                case Orientation.South:
                    return new Position(X, Y - 1 >= 0 ? Y - 1 : _maxY, _maxX, _maxY);
                case Orientation.West:
                    return new Position(X - 1 >= 0 ? X - 1 : _maxX, Y, _maxX, _maxY);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return X;
            yield return Y;
        }
    }
}