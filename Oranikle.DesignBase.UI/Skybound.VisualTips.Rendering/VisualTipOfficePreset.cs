using System.ComponentModel;
using System.Drawing;
using Microsoft.Win32;
using Skybound.ComponentModel;

namespace Skybound.VisualTips.Rendering
{

    [System.ComponentModel.TypeConverter(typeof(Skybound.VisualTips.Rendering.VisualTipOfficePreset.TypeConverter))]
    public class VisualTipOfficePreset
    {

        internal class TypeConverter : Skybound.ComponentModel.StandardValueConverter
        {

            public TypeConverter() : base(typeof(Skybound.VisualTips.Rendering.VisualTipOfficePreset))
            {
            }

        } // class TypeConverter

        internal System.Drawing.Color BackColor;
        internal System.Drawing.Color BackColorGradient;
        internal System.Drawing.Color BorderColor;
        internal System.Drawing.Color TextColor;

        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _AutoSelect;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _Brown;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _Control;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _Cyan;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _DeepBlue;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _Hazel;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _Midnight;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _Pink;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _Red;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _Smoke;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _ToolTip;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _Violet;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _XPBlue;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _XPGreen;
        private static Skybound.VisualTips.Rendering.VisualTipOfficePreset _XPSilver;

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset AutoSelect
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._AutoSelect;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset Brown
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._Brown;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset Control
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._Control;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset Cyan
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._Cyan;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset DeepBlue
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._DeepBlue;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset Hazel
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._Hazel;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset Midnight
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._Midnight;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset Pink
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._Pink;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset Red
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._Red;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset Smoke
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._Smoke;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset ToolTip
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._ToolTip;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset Violet
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._Violet;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset XPBlue
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._XPBlue;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset XPGreen
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._XPGreen;
            }
        }

        [Skybound.ComponentModel.StandardValue]
        public static Skybound.VisualTips.Rendering.VisualTipOfficePreset XPSilver
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset._XPSilver;
            }
        }

        private VisualTipOfficePreset()
        {
        }

        private VisualTipOfficePreset(System.Drawing.Color backColor, System.Drawing.Color backColorGradient, System.Drawing.Color borderColor, System.Drawing.Color textColor)
        {
            BackColor = backColor;
            BackColorGradient = backColorGradient;
            BorderColor = borderColor;
            TextColor = textColor;
        }

        static VisualTipOfficePreset()
        {
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._XPBlue = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.Color.FromArgb(255, 255, 255), System.Drawing.Color.FromArgb(201, 217, 239), System.Drawing.Color.FromArgb(111, 121, 133), System.Drawing.Color.FromArgb(64, 64, 64));
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._XPGreen = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.Color.FromArgb(245, 255, 215), System.Drawing.Color.FromArgb(197, 210, 155), System.Drawing.Color.FromArgb(123, 140, 119), System.Drawing.Color.FromArgb(64, 64, 64));
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._XPSilver = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.Color.FromArgb(251, 251, 251), System.Drawing.Color.FromArgb(220, 220, 220), System.Drawing.Color.FromArgb(128, 128, 128), System.Drawing.Color.FromArgb(64, 64, 64));
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._Control = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.ControlDark, System.Drawing.SystemColors.ControlText);
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._Brown = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.Color.FromArgb(255, 249, 249), System.Drawing.Color.FromArgb(233, 222, 208), System.Drawing.Color.FromArgb(138, 128, 118), System.Drawing.Color.FromArgb(64, 64, 64));
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._Hazel = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.Color.FromArgb(255, 252, 249), System.Drawing.Color.FromArgb(232, 233, 208), System.Drawing.Color.FromArgb(140, 140, 119), System.Drawing.Color.FromArgb(64, 64, 64));
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._Cyan = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.Color.FromArgb(248, 254, 251), System.Drawing.Color.FromArgb(208, 228, 233), System.Drawing.Color.FromArgb(118, 135, 138), System.Drawing.Color.FromArgb(64, 64, 64));
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._Pink = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.Color.FromArgb(254, 248, 253), System.Drawing.Color.FromArgb(233, 208, 212), System.Drawing.Color.FromArgb(138, 118, 123), System.Drawing.Color.FromArgb(64, 64, 64));
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._Violet = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.Color.FromArgb(248, 248, 254), System.Drawing.Color.FromArgb(227, 208, 233), System.Drawing.Color.FromArgb(132, 118, 138), System.Drawing.Color.FromArgb(64, 64, 64));
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._Red = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.Color.FromArgb(243, 217, 207), System.Drawing.Color.IndianRed, System.Drawing.Color.FromArgb(132, 96, 96), System.Drawing.Color.FromArgb(48, 32, 32));
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._ToolTip = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.SystemColors.Info, System.Drawing.SystemColors.Info, System.Drawing.SystemColors.WindowFrame, System.Drawing.SystemColors.InfoText);
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._DeepBlue = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.Color.FromArgb(221, 236, 254), System.Drawing.Color.FromArgb(129, 169, 226), System.Drawing.Color.FromArgb(59, 97, 156), System.Drawing.Color.FromArgb(0, 0, 0));
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._Smoke = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.Color.FromArgb(240, 240, 240), System.Drawing.Color.FromArgb(224, 224, 224), System.Drawing.Color.FromArgb(128, 128, 128), System.Drawing.Color.FromArgb(0, 0, 0));
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._Midnight = new Skybound.VisualTips.Rendering.VisualTipOfficePreset(System.Drawing.Color.Gray, System.Drawing.Color.Black, System.Drawing.Color.Silver, System.Drawing.Color.White);
            Microsoft.Win32.SystemEvents.UserPreferenceChanged += new Microsoft.Win32.UserPreferenceChangedEventHandler(Skybound.VisualTips.Rendering.VisualTipOfficePreset.SystemEvents_UserPreferenceChanged);
            Skybound.VisualTips.Rendering.VisualTipOfficePreset.UpdateAutoSelect();
        }

        private static void SystemEvents_UserPreferenceChanged(object sender, Microsoft.Win32.UserPreferenceChangedEventArgs e)
        {
            if (e.Category == Microsoft.Win32.UserPreferenceCategory.Color)
                Skybound.VisualTips.Rendering.VisualTipOfficePreset.UpdateAutoSelect();
        }

        private static void UpdateAutoSelect()
        {
            Skybound.VisualTips.Rendering.VisualTipOfficePreset visualTipOfficePreset;

            System.Drawing.Color color1 = System.Drawing.SystemColors.ActiveCaption;
            float f = color1.GetHue();
            if ((f >= 200.0F) && (f <= 250.0F))
            {
                visualTipOfficePreset = Skybound.VisualTips.Rendering.VisualTipOfficePreset.XPBlue;
            }
            else if ((f >= 70.0F) && (f <= 120.0F))
            {
                visualTipOfficePreset = Skybound.VisualTips.Rendering.VisualTipOfficePreset.XPGreen;
            }
            else
            {
                if (f != 0.0F)
                {
                    System.Drawing.Color color2 = System.Drawing.SystemColors.ActiveCaption;
                    if (color2.GetSaturation() >= 0.1F) goto label_1;
                }
                visualTipOfficePreset = Skybound.VisualTips.Rendering.VisualTipOfficePreset.XPSilver;
                goto label_2;
            label_1:
                visualTipOfficePreset = Skybound.VisualTips.Rendering.VisualTipOfficePreset.Control;
            }
        label_2:
            Skybound.VisualTips.Rendering.VisualTipOfficePreset._AutoSelect = (Skybound.VisualTips.Rendering.VisualTipOfficePreset)visualTipOfficePreset.MemberwiseClone();
        }

    } // class VisualTipOfficePreset

}

