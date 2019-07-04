using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover
{
    public class Grid : IGrid
    {
        private readonly HashSet<(int X, int Y)> _obstacles;
        private readonly int _maxX;
        private readonly int _maxY;

        public Grid(int maxX, int maxY)
        {
            _maxX = maxX;
            _maxY = maxY;
            _obstacles = new HashSet<(int, int)>(maxX*maxY);
        }

        public Grid AddObstacle((int X, int Y) obstacleCoordinate)
        {
            _obstacles.Add(obstacleCoordinate);
            return this;
        }

        public bool IsBlocked((int X, int Y) position) => _obstacles.Contains(position);

        /// <inheritdoc />
        public bool IsOutOfRange((int X, int Y) newPosition)
        {
            var (x, y) = newPosition;
            return x < 0 || x >= _maxX || y < 0 ||
                   y >= _maxY;
        }
    }
}
