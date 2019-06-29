using MarsRover.Util;

namespace MarsRover
{
    public enum Orientation
    {
        [Reverse(South)]
        [Left(West)]
        [Right(East)]
        North = 'N',

        [Reverse(West)]
        [Left(North)]
        [Right(South)]
        East = 'E',

        [Reverse(North)]
        [Left(East)]
        [Right(West)]
        South = 'S',

        [Reverse(East)]
        [Left(South)]
        [Right(North)]
        West = 'W'
    }
}