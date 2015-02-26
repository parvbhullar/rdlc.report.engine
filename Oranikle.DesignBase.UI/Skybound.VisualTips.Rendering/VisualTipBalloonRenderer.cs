using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Skybound.ComponentModel;
using Skybound.VisualTips;

namespace Skybound.VisualTips.Rendering
{

    [Skybound.ComponentModel.DisplayName("Balloon")]
    public class VisualTipBalloonRenderer : Skybound.VisualTips.Rendering.VisualTipRenderer
    {

        [System.ComponentModel.Description("The background color of a tip.")]
        [Skybound.ComponentModel.PropertyListValue]
        public System.Drawing.Color BackColor
        {
            get
            {
                return (System.Drawing.Color)Properties.GetValue("BackColor", System.Drawing.SystemColors.Info);
            }
            set
            {
                Properties.SetValue("BackColor", MakeSolidColor(value), System.Drawing.Color.Empty);
            }
        }

        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Description("The color of the tip border.")]
        public System.Drawing.Color BorderColor
        {
            get
            {
                return (System.Drawing.Color)Properties.GetValue("BorderColor", System.Drawing.SystemColors.InfoText);
            }
            set
            {
                Properties.SetValue("BorderColor", MakeSolidColor(value), System.Drawing.Color.Empty);
            }
        }

        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Description("The color of the text on a tip.")]
        public System.Drawing.Color TextColor
        {
            get
            {
                return (System.Drawing.Color)Properties.GetValue("TextColor", System.Drawing.SystemColors.InfoText);
            }
            set
            {
                Properties.SetValue("TextColor", MakeSolidColor(value), System.Drawing.Color.Empty);
            }
        }

        public VisualTipBalloonRenderer()
        {
        }

        private System.Drawing.Point[] GetArrowPoints(Skybound.VisualTips.VisualTip tip, System.Drawing.Rectangle window)
        {
            System.Drawing.Rectangle rectangle = tip.GetRelativeToolArea();
            int i = System.Math.Min(System.Math.Max(rectangle.X, window.X + 16), window.Right - 32);
            bool flag = i > (window.X + (window.Width / 2));
            if (rectangle.Y <= 0)
            {
                System.Drawing.Point[] pointArr1 = new System.Drawing.Point[3];
                pointArr1[0] = new System.Drawing.Point(i, window.Y);
                pointArr1[1] = new System.Drawing.Point(i + (flag ? 16 : 0), window.Y - 16);
                pointArr1[2] = new System.Drawing.Point(i + 16, window.Y);
                return pointArr1;
            }
            System.Drawing.Point[] pointArr2 = new System.Drawing.Point[3];
            pointArr2[0] = new System.Drawing.Point(i + 16, window.Bottom - 1);
            pointArr2[1] = new System.Drawing.Point(i + (flag ? 16 : 0), window.Bottom - 1 + 16);
            pointArr2[2] = new System.Drawing.Point(i, window.Bottom - 1);
            return pointArr2;
        }

        protected override Skybound.VisualTips.Rendering.VisualTipLayout OnCreateLayout(Skybound.VisualTips.VisualTip tip)
        {
            Skybound.VisualTips.Rendering.VisualTipLayout visualTipLayout = base.OnCreateLayout(tip);
            visualTipLayout.OffsetWindow(4, 18);
            visualTipLayout.WindowBounds = System.Drawing.Rectangle.Inflate(visualTipLayout.WindowBounds, 4, 2);
            System.Drawing.Rectangle rectangle1 = visualTipLayout.ShadowBounds;
            System.Drawing.Rectangle rectangle2 = visualTipLayout.ShadowBounds;
            visualTipLayout.ShadowBounds = new System.Drawing.Rectangle(rectangle1.Location, rectangle2.Size + (new System.Drawing.Size(8, 32)));
            return visualTipLayout;
        }

        protected override System.Drawing.Region OnCreateMaskRegion(Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout)
        {
            System.Drawing.Region region;

            System.Drawing.Rectangle rectangle = layout.WindowBounds;
            System.Drawing.Point[] pointArr1 = GetArrowPoints(tip, rectangle);
            System.Drawing.Point[] pointArr2 = new System.Drawing.Point[6];
            if (pointArr1[0].Y == rectangle.Y)
            {
                pointArr1.CopyTo(pointArr2, 0);
                System.Drawing.Point point1 = new System.Drawing.Point(rectangle.X + 10, rectangle.Bottom);
                pointArr2[5] = new System.Drawing.Point(rectangle.X + 10, rectangle.Bottom);
                System.Drawing.Point point2 = point1;
                pointArr2[4] = point1;
                pointArr2[3] = point2;
            }
            else
            {
                System.Drawing.Point point3 = new System.Drawing.Point(rectangle.X + 10, rectangle.Y);
                pointArr2[2] = new System.Drawing.Point(rectangle.X + 10, rectangle.Y);
                System.Drawing.Point point4 = point3;
                pointArr2[1] = point3;
                pointArr2[0] = point4;
                pointArr1.CopyTo(pointArr2, 3);
            }
            using (System.Drawing.Drawing2D.GraphicsPath graphicsPath = new System.Drawing.Drawing2D.GraphicsPath())
            {
                System.Drawing.Point[] pointArr3 = new System.Drawing.Point[26];
                pointArr3[0] = new System.Drawing.Point(rectangle.X, rectangle.Y + 5);
                pointArr3[1] = new System.Drawing.Point(rectangle.X + 1, rectangle.Y + 3);
                pointArr3[2] = new System.Drawing.Point(rectangle.X + 2, rectangle.Y + 2);
                pointArr3[3] = new System.Drawing.Point(rectangle.X + 3, rectangle.Y + 1);
                pointArr3[4] = new System.Drawing.Point(rectangle.X + 5, rectangle.Y);
                pointArr3[5] = new System.Drawing.Point(pointArr2[0].X, pointArr2[0].Y);
                pointArr3[6] = new System.Drawing.Point(pointArr2[1].X, pointArr2[1].Y);
                pointArr3[7] = new System.Drawing.Point(pointArr2[1].X + 1, pointArr2[1].Y);
                pointArr3[8] = new System.Drawing.Point(pointArr2[2].X + 1, pointArr2[2].Y);
                pointArr3[9] = new System.Drawing.Point(rectangle.Right - 5, rectangle.Y);
                pointArr3[10] = new System.Drawing.Point(rectangle.Right - 2, rectangle.Y + 2);
                pointArr3[11] = new System.Drawing.Point(rectangle.Right - 1, rectangle.Y + 4);
                pointArr3[12] = new System.Drawing.Point(rectangle.Right, rectangle.Bottom - 7);
                pointArr3[13] = new System.Drawing.Point(rectangle.Right - 2, rectangle.Bottom - 3);
                pointArr3[14] = new System.Drawing.Point(rectangle.Right - 5, rectangle.Bottom - 1);
                pointArr3[15] = new System.Drawing.Point(rectangle.Right - 6, rectangle.Bottom);
                pointArr3[16] = new System.Drawing.Point(pointArr2[3].X + 1, pointArr2[3].Y);
                pointArr3[17] = new System.Drawing.Point(pointArr2[4].X + 1, pointArr2[4].Y);
                pointArr3[18] = new System.Drawing.Point(pointArr2[4].X, pointArr2[4].Y);
                pointArr3[19] = new System.Drawing.Point(pointArr2[5].X, pointArr2[5].Y);
                pointArr3[20] = new System.Drawing.Point(rectangle.X + 5, rectangle.Bottom);
                pointArr3[21] = new System.Drawing.Point(rectangle.X + 5, rectangle.Bottom - 1);
                pointArr3[22] = new System.Drawing.Point(rectangle.X + 4, rectangle.Bottom - 1);
                pointArr3[23] = new System.Drawing.Point(rectangle.X + 2, rectangle.Bottom - 3);
                pointArr3[24] = new System.Drawing.Point(rectangle.X + 1, rectangle.Bottom - 4);
                pointArr3[25] = new System.Drawing.Point(rectangle.X, rectangle.Bottom - 6);
                graphicsPath.AddLines(pointArr3);
                region = new System.Drawing.Region(graphicsPath);
            }
            return region;
        }

        protected override void OnDrawShadow(System.Windows.Forms.PaintEventArgs e, Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout)
        {
            if (HasShadow(tip) && System.Drawing.Rectangle.Intersect(layout.ShadowBounds, layout.WindowBounds) != layout.ShadowBounds)
            {
                System.Drawing.Rectangle rectangle = layout.ShadowBounds;
                rectangle.X += 4;
                rectangle.Width -= 4;
                rectangle.Y += 16;
                rectangle.Height -= 28;
                Skybound.VisualTips.Rendering.VisualTipRenderer.DrawDropShadow(e.Graphics, rectangle, 12, System.Drawing.Color.FromArgb(128, System.Drawing.Color.Black));
            }
        }

        protected override void OnDrawWindow(System.Windows.Forms.PaintEventArgs e, Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout)
        {
            System.Drawing.Rectangle rectangle = layout.WindowBounds;
            System.Drawing.Drawing2D.GraphicsPath graphicsPath = Skybound.VisualTips.Rendering.VisualTipRenderer.CreateRoundRectPath(rectangle, Skybound.VisualTips.Rendering.VisualTipRenderer.LayeredWindowsSupported ? 10 : 14, Skybound.VisualTips.Rendering.VisualTipRenderer.BorderCorners.All);
            using (System.Drawing.Brush brush1 = new System.Drawing.SolidBrush(BackColor))
            {
                e.Graphics.FillPath(brush1, graphicsPath);
            }
            using (System.Drawing.Pen pen1 = new System.Drawing.Pen(BorderColor))
            {
                if (Skybound.VisualTips.Rendering.VisualTipRenderer.LayeredWindowsSupported)
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                else
                    pen1.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                e.Graphics.DrawPath(pen1, graphicsPath);
                if (Skybound.VisualTips.Rendering.VisualTipRenderer.LayeredWindowsSupported)
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                System.Drawing.Point[] pointArr = GetArrowPoints(tip, rectangle);
                using (System.Drawing.Brush brush2 = new System.Drawing.SolidBrush(BackColor))
                {
                    e.Graphics.FillPolygon(brush2, pointArr);
                }
                using (System.Drawing.Pen pen2 = new System.Drawing.Pen(BackColor))
                {
                    e.Graphics.DrawPolygon(pen2, pointArr);
                }
                e.Graphics.DrawLines(pen1, pointArr);
            }
        }

    } // class VisualTipBalloonRenderer

}

