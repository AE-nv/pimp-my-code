using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class Mars
    {
        private readonly IList<Position> _positions = new List<Position>();
        private readonly IList<Position> _obstacles = new List<Position>();

        private readonly int _planetDimensionX;
        private readonly int _planetDimensionY;

        public Mars(int planetDimensionX, int planetDimensionY)
        {
            _planetDimensionX = planetDimensionX;
            _planetDimensionY = planetDimensionY;
            
            for (var x = 0; x < planetDimensionX; x++)
            {
                for (var y = 0; y < planetDimensionY; y++)
                {
                    _positions.Add(new Position(x, y, planetDimensionX - 1, planetDimensionY - 1));
                }
            }
        }

        public Mars CrashMeteor(int x, int y)
        {
            var position = new Position(x, y, _planetDimensionX - 1, _planetDimensionY - 1);
            if (!_positions.Contains(position))
            {
                throw new Exception($"{position} is not a valid position on planet {nameof(Mars)}");
            }
            _obstacles.Add(position);
            return this;
        }

        public bool HasObstacle(Position position)
        {
            if (!_positions.Contains(position))
            {
                throw new Exception($"{position} is not a valid position on planet {nameof(Mars)}");
            }
            return _obstacles.FirstOrDefault(_ => _ == position) != null;
        }
    }
}