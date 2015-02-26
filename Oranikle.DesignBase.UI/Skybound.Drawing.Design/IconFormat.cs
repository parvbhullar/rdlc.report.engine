using System;
using System.Drawing;
using System.Windows.Forms;

namespace Skybound.Drawing.Design
{

    internal struct IconFormat
    {

        private System.Windows.Forms.ColorDepth _ColorDepth;
        private System.Drawing.Size _Size;

        public static Skybound.Drawing.Design.IconFormat Empty;

        public System.Windows.Forms.ColorDepth ColorDepth
        {
            get
            {
                return _ColorDepth;
            }
            set
            {
                _ColorDepth = value;
            }
        }

        public System.Drawing.Size Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
            }
        }

        public IconFormat(System.Drawing.Size size, System.Windows.Forms.ColorDepth colorDepth)
        {
            _Size = size;
            _ColorDepth = colorDepth;
        }

        static IconFormat()
        {
            Skybound.Drawing.Design.IconFormat.Empty = new Skybound.Drawing.Design.IconFormat();
        }

        public override bool Equals(object obj)
        {
            if (obj is Skybound.Drawing.Design.IconFormat)
            {
                Skybound.Drawing.Design.IconFormat iconFormat = (Skybound.Drawing.Design.IconFormat)obj;
                if (iconFormat._Size == _Size)
                    return iconFormat._ColorDepth == _ColorDepth;
                return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _Size.GetHashCode() ^ _ColorDepth.GetHashCode();
        }

        public override string ToString()
        {
            System.Drawing.Size size1 = Size;
            System.Drawing.Size size2 = Size;
            return System.String.Format("{0}x{1} {2} bpp", size1.Width, size2.Height, ColorDepth);
        }

    }

}

