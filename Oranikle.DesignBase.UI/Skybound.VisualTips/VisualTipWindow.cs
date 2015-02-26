using System;
using System.Drawing;
using System.Windows.Forms;
using Skybound.VisualTips.Rendering;
using Skybound.Windows.Forms;

namespace Skybound.VisualTips
{

    internal class VisualTipWindow : System.Windows.Forms.Form
    {

        private struct BLENDFUNCTION
        {

            public byte bytAlphaFormat;
            public byte bytBlendFlags;
            public byte bytBlendOp;
            public byte bytSourceConstantAlpha;

        }

        private struct POINT
        {

            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                X = x;
                Y = y;
            }

        }

        private struct SIZE
        {

            public int CX;
            public int CY;

            public SIZE(int cx, int cy)
            {
                CX = cx;
                CY = cy;
            }

        }

        private const byte AC_SRC_ALPHA = 1;
        private const byte AC_SRC_OVER = 0;
        private const int SW_HIDE = 0;
        private const int SW_NORMAL = 1;
        private const int SW_SHOW = 5;
        private const int SW_SHOWNOACTIVATE = 4;
        private const int ULW_ALPHA = 2;
        private const int ULW_COLORKEY = 1;
        private const int ULW_OPAQUE = 4;

        private Skybound.VisualTips.VisualTip _DisplayedTip;
        private bool _IsDisplayed;
        private Skybound.VisualTips.VisualTipDisplayOptions _Options;
        private Skybound.Windows.Forms.Animator Animator;
        private Skybound.Windows.Forms.BufferedGraphics Buffer;
        private System.IntPtr HLayeredWindowBitmap;
        private System.Drawing.Size LayeredWindowSize;
        private Skybound.VisualTips.VisualTipProvider Provider;
        private double StartingOpacity;

        public Skybound.VisualTips.VisualTip DisplayedTip
        {
            get
            {
                if (!IsDisplayed)
                    return null;
                return _DisplayedTip;
            }
        }

        public bool IsDisplayed
        {
            get
            {
                return _IsDisplayed;
            }
        }

        private bool IsLayeredWindow
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipRenderer.LayeredWindowsSupported;
            }
        }

        public Skybound.VisualTips.VisualTipDisplayOptions Options
        {
            get
            {
                return _Options;
            }
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams createParams = base.CreateParams;
                if (IsLayeredWindow)
                    createParams.ExStyle |= 524288;
                return createParams;
            }
        }

        public VisualTipWindow()
        {
            Buffer = new Skybound.Windows.Forms.BufferedGraphics();
            SetStyle(System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.DoubleBuffer, true);
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ShowInTaskbar = false;
            Animator = new Skybound.Windows.Forms.Animator(Skybound.Windows.Forms.Motion.Decelerate, 200);
            Animator.NextFrame += new System.EventHandler(Animator_NextFrame);
            Animator.SynchronizingObject = this;
        }

        private void Animator_NextFrame(object sender, System.EventArgs e)
        {
            if (Animator.IsComplete)
            {
                Skybound.VisualTips.VisualTipWindow.ShowWindow(Handle, 0);
                return;
            }
            UpdateAlphaMask((byte)((1.0 - Animator.Value) * (StartingOpacity * 255.0)));
        }

        private void DestroyLayeredWindowBitmap()
        {
            if (HLayeredWindowBitmap != System.IntPtr.Zero)
            {
                Skybound.VisualTips.VisualTipWindow.DeleteObject(HLayeredWindowBitmap);
                HLayeredWindowBitmap = System.IntPtr.Zero;
                LayeredWindowSize = System.Drawing.Size.Empty;
            }
        }

        public void Display(Skybound.VisualTips.VisualTipProvider provider, Skybound.VisualTips.VisualTip tip, System.Drawing.Rectangle toolArea, Skybound.VisualTips.VisualTipDisplayOptions options)
        {
            Provider = provider;
            _DisplayedTip = tip;
            _Options = options;
            RightToLeft = tip.RightToLeft;
            Skybound.VisualTips.Rendering.VisualTipLayout visualTipLayout = provider.Renderer.CreateLayout(tip);
            Size = visualTipLayout.GetSize();
            Location = GetBestLocation(toolArea, Size, options);
            toolArea.Location = toolArea.Location - (new System.Drawing.Size(Location));
            tip.SetRelativeToolArea(toolArea);
            if (IsLayeredWindow)
            {
                Animator.Stop();
                using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(Width, Height))
                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap))
                {
                    provider.Renderer.Draw(new System.Windows.Forms.PaintEventArgs(graphics, new System.Drawing.Rectangle(0, 0, Width, Height)), tip, visualTipLayout);
                    SetAlphaMask(bitmap, (byte)(Provider.Opacity * 255.0));
                }
            }
            DisplayAnimate();
            if (!IsLayeredWindow)
            {
                System.Drawing.Rectangle rectangle = visualTipLayout.WindowBounds;
                Width = rectangle.Width;
                Region = Provider.Renderer.CreateMaskRegion(tip, visualTipLayout);
            }
        }

        private void DisplayAnimate()
        {
            Skybound.VisualTips.VisualTipWindow.SetWindowPos(Handle, new System.IntPtr(-1), 0, 0, 0, 0, 83);
            _IsDisplayed = true;
            Opacity = 1.0;
        }

        private System.Drawing.Point GetBestLocation(System.Drawing.Rectangle toolArea, System.Drawing.Size tipSize, Skybound.VisualTips.VisualTipDisplayOptions options)
        {
            System.Drawing.Rectangle rectangle1 = System.Windows.Forms.Screen.GetBounds(toolArea);
            bool flag1 = RightToLeft == System.Windows.Forms.RightToLeft.No;
            System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle(toolArea.Location, tipSize);
            bool flag2 = (options & Skybound.VisualTips.VisualTipDisplayOptions.PositionMask) == Skybound.VisualTips.VisualTipDisplayOptions.PositionLeft;
            bool flag3 = (options & Skybound.VisualTips.VisualTipDisplayOptions.PositionMask) == Skybound.VisualTips.VisualTipDisplayOptions.PositionMask;
            bool flag4 = flag2 || ((options & Skybound.VisualTips.VisualTipDisplayOptions.PositionMask) == Skybound.VisualTips.VisualTipDisplayOptions.PositionRight);
            if (flag4)
            {
                if (flag1 && !flag2)
                {
                    if (((toolArea.Right + tipSize.Width) > rectangle1.Right) && ((toolArea.Left - tipSize.Width) >= rectangle1.X))
                        rectangle2.X -= tipSize.Width;
                    else
                        rectangle2.X += toolArea.Width;
                }
                else if (((toolArea.Left - tipSize.Width) < rectangle1.X) && ((toolArea.Right + tipSize.Width) <= rectangle1.Right))
                {
                    rectangle2.X += toolArea.Width;
                }
                else
                {
                    rectangle2.X -= tipSize.Width;
                }
            }
            else
            {
                if (!flag1)
                    rectangle2.X -= tipSize.Width - toolArea.Width;
                if (flag3)
                {
                    if (((toolArea.Y - tipSize.Height) < rectangle1.Y) && ((toolArea.Bottom + tipSize.Height) <= rectangle1.Bottom))
                        rectangle2.Y += toolArea.Height;
                    else
                        rectangle2.Y -= tipSize.Height;
                }
                else if (((toolArea.Bottom + tipSize.Height) > rectangle1.Bottom) && ((toolArea.Y - tipSize.Height) >= rectangle1.Y))
                {
                    rectangle2.Y -= tipSize.Height;
                }
                else
                {
                    rectangle2.Y += toolArea.Height;
                }
            }
            if (rectangle2.Right > rectangle1.Right)
                rectangle2.X = rectangle1.Right - rectangle2.Width;
            if (rectangle2.X < rectangle1.X)
                rectangle2.X = rectangle1.X;
            if (rectangle2.Bottom > rectangle1.Bottom)
                rectangle2.Y = rectangle1.Bottom - rectangle2.Height;
            if (rectangle2.Y < rectangle1.Y)
                rectangle2.Y = rectangle1.Y;
            return rectangle2.Location;
        }

        private void SetAlphaMask(System.Drawing.Bitmap bitmap, byte opacity)
        {
            DestroyLayeredWindowBitmap();
            HLayeredWindowBitmap = bitmap.GetHbitmap(System.Drawing.Color.FromArgb(0));
            LayeredWindowSize = bitmap.Size;
            UpdateAlphaMask(opacity);
        }

        public void SetToolArea(System.Drawing.Rectangle toolArea, Skybound.VisualTips.VisualTipDisplayOptions options)
        {
            Location = GetBestLocation(toolArea, Size, options);
            _Options = options;
        }

        public void Undisplay()
        {
            if (IsDisplayed)
            {
                _IsDisplayed = false;
                _DisplayedTip = null;
                if ((IsLayeredWindow && (Provider.Animation == Skybound.VisualTips.VisualTipAnimation.Enabled)) || ((Provider.Animation == Skybound.VisualTips.VisualTipAnimation.SystemDefault) && Skybound.VisualTips.SystemParameters.ToolTipAnimation))
                {
                    StartingOpacity = Provider.Opacity;
                    Animator.Start();
                }
                else
                {
                    Skybound.VisualTips.VisualTipWindow.ShowWindow(Handle, 0);
                }
                Provider.OnUndisplay(this);
            }
        }

        private void UpdateAlphaMask(byte opacity)
        {
            Skybound.VisualTips.VisualTipWindow.BLENDFUNCTION blendfunction;
            Skybound.VisualTips.VisualTipWindow.POINT point2;

            System.IntPtr intPtr1 = Skybound.VisualTips.VisualTipWindow.GetDC(System.IntPtr.Zero);
            System.IntPtr intPtr2 = Skybound.VisualTips.VisualTipWindow.CreateCompatibleDC(intPtr1);
            System.IntPtr intPtr3 = System.IntPtr.Zero;
            try
            {
                intPtr3 = Skybound.VisualTips.VisualTipWindow.SelectObject(intPtr2, HLayeredWindowBitmap);
                Skybound.VisualTips.VisualTipWindow.POINT point1 = new Skybound.VisualTips.VisualTipWindow.POINT(Left, Top);
                Skybound.VisualTips.VisualTipWindow.SIZE size = new Skybound.VisualTips.VisualTipWindow.SIZE(LayeredWindowSize.Width, LayeredWindowSize.Height);
                point2 = new Skybound.VisualTips.VisualTipWindow.POINT();
                blendfunction = new Skybound.VisualTips.VisualTipWindow.BLENDFUNCTION();
                blendfunction.bytBlendOp = 0;
                blendfunction.bytBlendFlags = 0;
                blendfunction.bytSourceConstantAlpha = opacity;
                blendfunction.bytAlphaFormat = 1;
                Skybound.VisualTips.VisualTipWindow.UpdateLayeredWindow(Handle, intPtr1, ref point1, ref size, intPtr2, ref point2, 0, ref blendfunction, 2);
            }
            finally
            {
                Skybound.VisualTips.VisualTipWindow.ReleaseDC(System.IntPtr.Zero, intPtr1);
                Skybound.VisualTips.VisualTipWindow.SelectObject(intPtr2, intPtr3);
                Skybound.VisualTips.VisualTipWindow.DeleteDC(intPtr2);
            }
        }

        protected override void Dispose(bool disposing)
        {
            DestroyLayeredWindowBitmap();
            base.Dispose(disposing);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (!IsLayeredWindow)
            {
                Buffer.SetTarget(e.Graphics, ClientRectangle);
                Provider.Renderer.Draw(new System.Windows.Forms.PaintEventArgs(Buffer.Graphics, e.ClipRectangle), DisplayedTip, null);
                Buffer.Render();
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 33)
            {
                Undisplay();
                m.Result = new System.IntPtr(4);
                return;
            }
            base.WndProc(ref m);
        }

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern System.IntPtr CreateCompatibleDC(System.IntPtr hdc);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int DeleteDC(System.IntPtr hdc);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int DeleteObject(System.IntPtr hObject);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern System.IntPtr GetDC(System.IntPtr hwnd);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int ReleaseDC(System.IntPtr hwnd, System.IntPtr hdc);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern System.IntPtr SelectObject(System.IntPtr hdc, System.IntPtr hObject);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int SetWindowPos(System.IntPtr hwnd, System.IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int ShowWindow(System.IntPtr hwnd, int nCmdShow);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern System.IntPtr UpdateLayeredWindow(System.IntPtr hwnd, System.IntPtr hdcDst, ref Skybound.VisualTips.VisualTipWindow.POINT pptDst, ref Skybound.VisualTips.VisualTipWindow.SIZE psize, System.IntPtr hdcSrc, ref Skybound.VisualTips.VisualTipWindow.POINT pprSrc, int crKey, ref Skybound.VisualTips.VisualTipWindow.BLENDFUNCTION pblend, int dwFlags);

    } // class VisualTipWindow

}

