using System;

namespace LB2_Photos
{
    public class MattedPhoto : Photo
    { 
        //  Color Enumerator
        public enum Color
        { 
            BLACK   = 0,
            RED     = 1,
            GREEN   = 2,
            BLUE    = 4,
            WHITE   = 7
        }

        //  Instance variables
        Color _theColor;

        //  Constructor
        public MattedPhoto(decimal w, decimal h, Color c)
                        : base(w, h)
        {
            _theColor = c;
        }

        //  Getter and Setter
        public Color TheColor => _theColor;

        //  Override the ToString() Method
        public override string ToString()
        { 
            return base.ToString() + " Matted " +
                   TheColor.ToString();
        }
    }
}
