using System;

namespace LB2_Photos
{
    public class FramedPhoto : Photo
    {
        //  Material Enumerator
        public enum Material
        {
            PINE    = 0,
            OAK     = 1,
            STEEL   = 2,
            SILVER  = 5,
            GOLD    = 4
        }

        //  Style Enumerator
        public enum Style
        {
            SIMPLE  = 0,
            MODERN  = 1,
            ANTIQUE = 2,
            VINTAGE = 5,
            ECLECTIC = 4
        }

        //  Instance variables
        Material _theMaterial;
        Style    _theStyle;

        //  Constructor
        public FramedPhoto(decimal w, decimal h, Material m, Style s)
                    :base (w, h)
        {
            _theMaterial = m;
            _theStyle    = s;
        }

        //  Getters and Setters
        public Material TheMaterial => _theMaterial;
        public Style    TheStyle    => _theStyle;

        //  Override the ToString() Method
        public override string ToString()
        {
            return base.ToString() + " Framed " +
                        TheMaterial.ToString() + "  " +
                        TheStyle.ToString();  
        }
    }
}
