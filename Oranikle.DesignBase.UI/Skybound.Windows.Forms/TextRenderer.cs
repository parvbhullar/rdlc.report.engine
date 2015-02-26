using System;
using System.Drawing;

namespace Skybound.Windows.Forms
{

    internal class TextRenderer
    {

        private struct LOGFONT
        {

            public byte lfCharSet;
            public byte lfClipPrecision;
            public int lfEscapement;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 31)]
            public string lfFaceName;
            public int lfHeight;
            public byte lfItalic;
            public int lfOrientation;
            public byte lfOutPrecision;
            public byte lfPitchAndFamily;
            public byte lfQuality;
            public byte lfStrikeOut;
            public byte lfUnderline;
            public int lfWeight;
            public int lfWidth;

        }

        private struct RECT
        {

            public int Bottom;
            public int Left;
            public int Right;
            public int Top;

            public RECT(System.Drawing.Rectangle r)
            {
                Left = r.Left;
                Top = r.Top;
                Right = r.Right;
                Bottom = r.Bottom;
            }

        }

        private const int DT_BOTTOM = 8;
        private const int DT_CALCRECT = 1024;
        private const int DT_CENTER = 1;
        private const int DT_HIDEPREFIX = 1048576;
        private const int DT_LEFT = 0;
        private const int DT_NOPREFIX = 2048;
        private const int DT_RIGHT = 2;
        private const int DT_RTLREADING = 131072;
        private const int DT_SINGLELINE = 32;
        private const int DT_TOP = 0;
        private const int DT_VCENTER = 4;
        private const int OPAQUE = 2;
        private const int TRANSPARENT = 1;
        private const int WM_ERASEBKGND = 20;

        private static System.Drawing.Font _CachedFont;
        private static System.IntPtr _CachedHFont;
        private static System.IntPtr _MeasureHdc;
        private static System.Drawing.Bitmap _MeasureHdcBitmap;
        private static System.Drawing.Graphics _MeasureHdcGraphics;

        private static System.IntPtr MeasureHdc
        {
            get
            {
                if (Skybound.Windows.Forms.TextRenderer._MeasureHdc == System.IntPtr.Zero)
                {
                    Skybound.Windows.Forms.TextRenderer._MeasureHdcBitmap = new System.Drawing.Bitmap(1, 1);
                    Skybound.Windows.Forms.TextRenderer._MeasureHdcGraphics = System.Drawing.Graphics.FromImage(Skybound.Windows.Forms.TextRenderer._MeasureHdcBitmap);
                    Skybound.Windows.Forms.TextRenderer._MeasureHdc = Skybound.Windows.Forms.TextRenderer._MeasureHdcGraphics.GetHdc();
                }
                return Skybound.Windows.Forms.TextRenderer._MeasureHdc;
            }
        }

        private TextRenderer()
        {
        }

        private static System.IntPtr CreateFont(System.Drawing.Font font)
        {
            Skybound.Windows.Forms.TextRenderer.LOGFONT logfont2;

            logfont2 = new Skybound.Windows.Forms.TextRenderer.LOGFONT();
            object obj = logfont2;
            font.ToLogFont(obj);
            Skybound.Windows.Forms.TextRenderer.LOGFONT logfont1 = (Skybound.Windows.Forms.TextRenderer.LOGFONT)obj;
            logfont1.lfFaceName = font.Name;
            logfont1.lfQuality = 0;
            return Skybound.Windows.Forms.TextRenderer.CreateFontIndirect(ref logfont1);
        }

        public static void DrawText(System.Drawing.Graphics graphics, string text, System.Drawing.Font font, System.Drawing.Color foreColor, System.Drawing.Color backColor, System.Drawing.Rectangle bounds, Skybound.Windows.Forms.TextFormatFlags formatFlags)
        {
            if ((text == null) || (text.Length == 0) || (graphics == null) || (font == null) || bounds.Size == System.Drawing.Size.Empty || foreColor.Equals(System.Drawing.Color.Empty) || foreColor.Equals(System.Drawing.Color.Transparent))
                return;
            System.IntPtr intPtr1 = graphics.GetHdc();
            try
            {
                System.IntPtr intPtr2 = Skybound.Windows.Forms.TextRenderer.GetCachedFont(font);
                System.IntPtr intPtr3 = Skybound.Windows.Forms.TextRenderer.SelectObject(intPtr1, intPtr2);
                if (backColor.Equals(System.Drawing.Color.Empty) || backColor.Equals(System.Drawing.Color.Transparent))
                {
                    Skybound.Windows.Forms.TextRenderer.SetBkMode(intPtr1, 1);
                }
                else
                {
                    Skybound.Windows.Forms.TextRenderer.SetBkMode(intPtr1, 2);
                    Skybound.Windows.Forms.TextRenderer.SetBkColor(intPtr1, System.Drawing.ColorTranslator.ToWin32(backColor));
                }
                int i = Skybound.Windows.Forms.TextRenderer.GetTextColor(intPtr1);
                Skybound.Windows.Forms.TextRenderer.SetTextColor(intPtr1, System.Drawing.ColorTranslator.ToWin32(foreColor));
                Skybound.Windows.Forms.TextRenderer.RECT rect = new Skybound.Windows.Forms.TextRenderer.RECT(bounds);
                Skybound.Windows.Forms.TextRenderer.DrawText(intPtr1, text, text.Length, ref rect, (int)formatFlags);
                Skybound.Windows.Forms.TextRenderer.SetTextColor(intPtr1, i);
                Skybound.Windows.Forms.TextRenderer.SelectObject(intPtr1, intPtr3);
            }
            finally
            {
                graphics.ReleaseHdc(intPtr1);
            }
        }

        private static System.IntPtr GetCachedFont(System.Drawing.Font font)
        {
            if (font == Skybound.Windows.Forms.TextRenderer._CachedFont)
                return Skybound.Windows.Forms.TextRenderer._CachedHFont;
            if (Skybound.Windows.Forms.TextRenderer._CachedHFont != System.IntPtr.Zero)
                Skybound.Windows.Forms.TextRenderer.DeleteObject(Skybound.Windows.Forms.TextRenderer._CachedHFont);
            Skybound.Windows.Forms.TextRenderer._CachedFont = font;
            Skybound.Windows.Forms.TextRenderer._CachedHFont = Skybound.Windows.Forms.TextRenderer.CreateFont(font);
            return Skybound.Windows.Forms.TextRenderer.CreateFont(font);
        }

        public static System.Drawing.Size MeasureText(string text, System.Drawing.Font font)
        {
            return Skybound.Windows.Forms.TextRenderer.MeasureText(text, font, System.Drawing.Size.Empty, Skybound.Windows.Forms.TextFormatFlags.Default);
        }

        public static System.Drawing.Size MeasureText(string text, System.Drawing.Font font, System.Drawing.Size extent, Skybound.Windows.Forms.TextFormatFlags formatFlags)
        {
            if ((text == null) || (text.Length == 0) || (font == null))
                return System.Drawing.Size.Empty;
            if (extent == System.Drawing.Size.Empty)
                extent = new System.Drawing.Size(32767, 0);
            System.IntPtr intPtr1 = Skybound.Windows.Forms.TextRenderer.GetCachedFont(font);
            System.IntPtr intPtr2 = Skybound.Windows.Forms.TextRenderer.SelectObject(Skybound.Windows.Forms.TextRenderer.MeasureHdc, intPtr1);
            Skybound.Windows.Forms.TextRenderer.RECT rect = new Skybound.Windows.Forms.TextRenderer.RECT(new System.Drawing.Rectangle(0, 0, extent.Width, extent.Height));
            Skybound.Windows.Forms.TextRenderer.DrawText(Skybound.Windows.Forms.TextRenderer.MeasureHdc, text, text.Length, ref rect, (int)formatFlags | 1024);
            Skybound.Windows.Forms.TextRenderer.SelectObject(Skybound.Windows.Forms.TextRenderer.MeasureHdc, intPtr2);
            return new System.Drawing.Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
        }

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", EntryPoint = "CreateFontIndirectA", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern System.IntPtr CreateFontIndirect(ref Skybound.Windows.Forms.TextRenderer.LOGFONT lpLogFont);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int DeleteObject(System.IntPtr hObject);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int DrawText(System.IntPtr hdc, string lpStr, int nCount, ref Skybound.Windows.Forms.TextRenderer.RECT lpRect, int wFormat);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int GetTextColor(System.IntPtr hdc);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern System.IntPtr SelectObject(System.IntPtr hdc, System.IntPtr hObject);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int SetBkColor(System.IntPtr hdc, int crColor);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int SetBkMode(System.IntPtr hdc, int nBkMode);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int SetTextColor(System.IntPtr hdc, int crColor);

    } // class TextRenderer

}

