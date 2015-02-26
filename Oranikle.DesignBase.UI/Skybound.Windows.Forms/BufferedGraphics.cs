using System;
using System.Drawing;

namespace Skybound.Windows.Forms
{

    internal class BufferedGraphics : System.IDisposable
    {

        private System.Drawing.Graphics _Graphics;
        private System.IntPtr _Hbitmap;
        private System.IntPtr _Hdc;
        private System.IntPtr _HoldBitmap;
        private System.Drawing.Size CreatedSize;
        private System.Drawing.Rectangle TargetBounds;
        private System.Drawing.Graphics TargetGraphics;

        public System.Drawing.Graphics Graphics
        {
            get
            {
                if (_Graphics == null)
                    throw new System.InvalidOperationException("No target has been defined.");
                return _Graphics;
            }
        }

        public BufferedGraphics()
        {
        }

        private void CreateBitmap(int width, int height)
        {
            System.IntPtr intPtr = Skybound.Windows.Forms.BufferedGraphics.CreateDC("DISPLAY", System.IntPtr.Zero, System.IntPtr.Zero, System.IntPtr.Zero);
            try
            {
                _Hbitmap = Skybound.Windows.Forms.BufferedGraphics.CreateCompatibleBitmap(intPtr, width, height);
                _Hdc = Skybound.Windows.Forms.BufferedGraphics.CreateCompatibleDC(intPtr);
                _HoldBitmap = Skybound.Windows.Forms.BufferedGraphics.SelectObject(_Hdc, _Hbitmap);
                _Graphics = System.Drawing.Graphics.FromHdc(_Hdc);
                CreatedSize = new System.Drawing.Size(width, height);
            }
            finally
            {
                Skybound.Windows.Forms.BufferedGraphics.DeleteDC(intPtr);
            }
            System.GC.KeepAlive(this);
        }

        private void DestroyBitmap()
        {
            if (_Hbitmap != System.IntPtr.Zero)
            {
                if (_Graphics != null)
                    _Graphics.Dispose();
                Skybound.Windows.Forms.BufferedGraphics.SelectObject(_Hdc, _HoldBitmap);
                Skybound.Windows.Forms.BufferedGraphics.DeleteObject(_Hbitmap);
                Skybound.Windows.Forms.BufferedGraphics.DeleteDC(_Hdc);
                _Hbitmap = System.IntPtr.Zero;
                _Hdc = System.IntPtr.Zero;
                CreatedSize = System.Drawing.Size.Empty;
                System.GC.KeepAlive(this);
            }
        }

        public void Render()
        {
            if (TargetGraphics == null)
                throw new System.InvalidOperationException("No target has been defined.");
            System.IntPtr intPtr1 = TargetGraphics.GetHdc();
            System.IntPtr intPtr2 = Graphics.GetHdc();
            try
            {
                Skybound.Windows.Forms.BufferedGraphics.BitBlt(intPtr1, TargetBounds.X, TargetBounds.Y, TargetBounds.Width, TargetBounds.Height, intPtr2, 0, 0, 13369376);
            }
            finally
            {
                Graphics.ReleaseHdc(intPtr2);
                TargetGraphics.ReleaseHdc(intPtr1);
            }
        }

        public void SetTarget(System.Drawing.Graphics graphics, System.Drawing.Rectangle bounds)
        {
            if (graphics == null)
                throw new System.ArgumentException("graphics");
            if ((CreatedSize.Width < bounds.Width) || (CreatedSize.Height < bounds.Height))
            {
                DestroyBitmap();
                System.Drawing.Size size1 = bounds.Size;
                System.Drawing.Size size2 = bounds.Size;
                System.Drawing.Size size3 = bounds.Size;
                System.Drawing.Size size4 = bounds.Size;
                CreateBitmap(size1.Width + (size2.Width % 16), size3.Height + (size4.Height % 16));
            }
            TargetGraphics = graphics;
            TargetBounds = bounds;
        }

        public virtual void Dispose()
        {
            DestroyBitmap();
            System.GC.SuppressFinalize(this);
        }

        ~BufferedGraphics()
        {
            DestroyBitmap();
        }

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int BitBlt(System.IntPtr hDestDC, int x, int y, int nWidth, int nHeight, System.IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern System.IntPtr CreateCompatibleBitmap(System.IntPtr hdc, int width, int height);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern System.IntPtr CreateCompatibleDC(System.IntPtr hdc);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern System.IntPtr CreateDC(string lpDriverName, System.IntPtr lpDeviceName, System.IntPtr lpOutput, System.IntPtr lpInitData);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int DeleteDC(System.IntPtr hdc);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int DeleteObject(System.IntPtr hObject);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern System.IntPtr SelectObject(System.IntPtr hdc, System.IntPtr hObject);

    } // class BufferedGraphics

}

