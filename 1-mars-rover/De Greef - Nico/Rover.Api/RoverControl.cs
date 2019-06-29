using Rover.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rover.Api
{
    public class RoverControl
    {
        private const string _compass = "NESW"; // Order of the directions is important, do not change

        private int _currentX = 0;
        private int _currentY = 0;
        private char _currentDirection = '0';
        private Planet _planet;

        public void Navigate(Planet planet, int startX, int startY, char direction, char[] movements, Action<int, int, char> endPointReached, Action<string> moved)
        {
            // Params check
            try
            {
                Planet.Sector s = planet.GetSector(startX, startY);
                if (s.IsBlocked) throw new ApplicationException("Invalid starting location");
            }
            catch (Exception ex)
            {
                // Catches the case where the starting location does not exist on the grid
                throw new ApplicationException("Invalid starting location", ex);
            }
            if (!_compass.Contains(direction)) throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction");
            if (movements.Count((m) => !"FBLR".Contains(m)) > 0) throw new ArgumentOutOfRangeException(nameof(movements), "Invalid movements specified, only F, B, L and R are allowed");
            _currentX = startX;
            _currentY = startY;
            _currentDirection = direction;
            _planet = planet;

            // Logic
            foreach (char movement in movements)
            {
                if (!DoMove(movement))
                {
                    moved?.Invoke("Blocked");
                    break;
                }
                moved?.Invoke(string.Format($"{_currentX}-{_currentY}-{_currentDirection}"));
            }

            // Callback final endpoint
            endPointReached?.Invoke(_currentX, _currentY, _currentDirection);
        }

        private bool DoMove(char movement)
        {
            var lastGoodLocation = new { x = _currentX, y = _currentY, direction = _currentDirection };

            int newIndex = 0;
            switch (movement)
            {
                case 'L':
                    // A linkedlist would have been nice as well
                    newIndex = _compass.IndexOf(_currentDirection) - 1;
                    if (newIndex < 0) newIndex = _compass.Length - 1;
                    _currentDirection = _compass[newIndex];
                    break;
                case 'R':
                    // A linkedlist would have been nice as well
                    newIndex = _compass.IndexOf(_currentDirection) + 1;
                    if (newIndex > _compass.Length-1) newIndex = 0;
                    _currentDirection = _compass[newIndex];
                    break;
                case 'F':
                    if (_currentDirection == 'N') _currentY++;
                    if (_currentDirection == 'E') _currentX++;
                    if (_currentDirection == 'S') _currentY--;
                    if (_currentDirection == 'W') _currentX--;
                    break;
                case 'B':
                    if (_currentDirection == 'N') _currentY--;
                    if (_currentDirection == 'E') _currentX--;
                    if (_currentDirection == 'S') _currentY++;
                    if (_currentDirection == 'W') _currentX++;
                    break;
            }

            // Off grid check
            if (_currentX < 0) _currentX = _planet.MaxX();
            if (_currentX > _planet.MaxX()) _currentX = 0;
            if (_currentY < 0) _currentY = _planet.MaxY();
            if (_currentY > _planet.MaxY()) _currentY = 0;
            
            // Undo the move if we arrived at a blocked location
            if (_planet.GetSector(_currentX, _currentY).IsBlocked)
            {
                _currentX = lastGoodLocation.x;
                _currentY = lastGoodLocation.y;
                _currentDirection = lastGoodLocation.direction;
                return false;
            }
            return true;
        }
    }
}
