using System;

namespace MarsRover.Util
{
    public class LeftAttribute : Attribute
    {
        public Orientation Orientation { get; }

        public LeftAttribute(Orientation orientation)
        {
            Orientation = orientation;
        }
    }
}