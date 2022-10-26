using System;

namespace LB2_Photos
{
    public class Photo
    {
        //  Instance variables
        private decimal _width;
        private decimal _height;

        //  Constructor
        public Photo(decimal width, decimal height)
        {
            _width  = width;
            _height = height;
        }

        //  Getters and Setters
        public decimal Width => _width;
        public decimal Height => _height;

        //  Override the ToString() method
        public override string ToString()
        {
            return (Width + " x " + Height);
        }
    }
}
