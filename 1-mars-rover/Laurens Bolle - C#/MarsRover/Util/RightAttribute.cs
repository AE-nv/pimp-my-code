using System;

namespace MarsRover.Util
{
    public class RightAttribute : Attribute
    {
        public Orientation Orientation { get; }

        public RightAttribute(Orientation orientation)
        {
            Orientation = orientation;
        }
    }
}