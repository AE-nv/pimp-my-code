using System;

namespace MarsRover.Util
{
    public class ReverseAttribute : Attribute
    {
        public Orientation Orientation { get; }

        public ReverseAttribute(Orientation orientation)
        {
            Orientation = orientation;
        }
    }
}