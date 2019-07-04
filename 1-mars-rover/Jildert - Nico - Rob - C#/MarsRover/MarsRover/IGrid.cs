namespace MarsRover
{
    public interface IGrid
    {
        Grid AddObstacle((int X, int Y) obstacleCoordinate);
        bool IsBlocked((int X, int Y) position);
        bool IsOutOfRange((int X, int Y) newPosition);
    }
}