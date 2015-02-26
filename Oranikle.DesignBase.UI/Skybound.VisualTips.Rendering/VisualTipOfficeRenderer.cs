using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Skybound.ComponentModel;
using Skybound.VisualTips;

namespace Skybound.VisualTips.Rendering
{

    [Skybound.ComponentModel.DisplayName("Office")]
    public class VisualTipOfficeRenderer : Skybound.VisualTips.Rendering.VisualTipRenderer
    {

        private Skybound.VisualTips.Rendering.VisualTipOfficePreset _Preset;

        [System.ComponentModel.Description("The background color of a tip.")]
        [Skybound.ComponentModel.PropertyListValue]
        public System.Drawing.Color BackColor
        {
            get
            {
                return (System.Drawing.Color)Properties.GetValue("BackColor", Preset.BackColor);
            }
            set
            {
                Properties.SetValue("BackColor", MakeSolidColor(value), System.Drawing.Color.Empty);
            }
        }

        [System.ComponentModel.Description("The color into which the background gradient blends on a tip.")]
        [Skybound.ComponentModel.PropertyListValue]
        public System.Drawing.Color BackColorGradient
        {
            get
            {
                return (System.Drawing.Color)Properties.GetValue("BackColorGradient", Preset.BackColorGradient);
            }
            set
            {
                Properties.SetValue("BackColorGradient", MakeSolidColor(value), System.Drawing.Color.Empty);
            }
        }

        [System.ComponentModel.Description("The effect used to draw a tip background.")]
        [Skybound.ComponentModel.PropertyListValue]
        public Skybound.VisualTips.Rendering.VisualTipOfficeBackgroundEffect BackgroundEffect
        {
            get
            {
                return (Skybound.VisualTips.Rendering.VisualTipOfficeBackgroundEffect)Properties.GetValue("BackgroundEffect", 0);
            }
            set
            {
                Properties.SetValue("BackgroundEffect", value, 0);
            }
        }

        [System.ComponentModel.Description("The color of the tip border.")]
        [Skybound.ComponentModel.PropertyListValue]
        public System.Drawing.Color BorderColor
        {
            get
            {
                return (System.Drawing.Color)Properties.GetValue("BorderColor", Preset.BorderColor);
            }
            set
            {
                Properties.SetValue("BorderColor", MakeSolidColor(value), System.Drawing.Color.Empty);
            }
        }

        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Description("The angle of the background gradient, in degrees clockwise from the x-axis (0-360).")]
        public int GradientAngle
        {
            get
            {
                return (int)Properties.GetValue("GradientAngle", 90);
            }
            set
            {
                Properties.SetValue("GradientAngle", value, 90);
            }
        }

        [System.ComponentModel.Description("The preset which determines the default colors used by the renderer.")]
        [System.ComponentModel.DefaultValue(typeof(Skybound.VisualTips.Rendering.VisualTipOfficePreset), "AutoSelect")]
        [System.ComponentModel.ParenthesizePropertyName(true)]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        private Skybound.VisualTips.Rendering.VisualTipOfficePreset Preset
        {
            get
            {
                if (_Preset != null)
                    return _Preset;
                return Skybound.VisualTips.Rendering.VisualTipOfficePreset.AutoSelect;
            }
            set
            {
                _Preset = value;
                OnPropertyChanged("Preset");
            }
        }

        [Skybound.ComponentModel.PropertyListValue]
        [System.ComponentModel.Description("Whether a tip is displayed with round corners.")]
        public bool RoundCorners
        {
            get
            {
                return (bool)Properties.GetValue("RoundCorners", 1);
            }
            set
            {
                Properties.SetValue("RoundCorners", value, 1);
            }
        }

        [System.ComponentModel.Description("The color of the text on a tip.")]
        [Skybound.ComponentModel.PropertyListValue]
        public System.Drawing.Color TextColor
        {
            get
            {
                return (System.Drawing.Color)Properties.GetValue("TextColor", Preset.TextColor);
            }
            set
            {
                Properties.SetValue("TextColor", MakeSolidColor(value), System.Drawing.Color.Empty);
            }
        }

        public VisualTipOfficeRenderer()
        {
        }

        private System.Drawing.Drawing2D.GraphicsPath CreateGlarePath(System.Drawing.Rectangle bounds, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
            if (radius == 0)
            {
                graphicsPath.AddLine(bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
                graphicsPath.AddLine(bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 1);
            }
            else
            {
                graphicsPath.AddLine(bounds.X + radius, bounds.Y, bounds.Right - radius - 1, bounds.Y);
                graphicsPath.AddArc(bounds.Right - radius - 1, bounds.Y, radius, radius, 270.0F, 90.0F);
                graphicsPath.AddLine(bounds.Right - 1, bounds.Y + radius, bounds.Right - 1, bounds.Bottom - radius);
            }
            graphicsPath.AddBezier(bounds.Right - 1, bounds.Bottom - 1, bounds.Right, bounds.Y + (bounds.Height / 2), bounds.X + (bounds.Width / 2), bounds.Y, bounds.X, bounds.Y);
            graphicsPath.CloseAllFigures();
            return graphicsPath;
        }

        protected override System.Drawing.Color GetElementTextColor(Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipRenderElement element)
        {
            return TextColor;
        }

        protected override System.Drawing.Region OnCreateMaskRegion(Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout)
        {
            if (RoundCorners)
            {
                System.Drawing.Rectangle rectangle = layout.WindowBounds;
                rectangle.Width++;
                rectangle.Height++;
                System.Drawing.Drawing2D.GraphicsPath graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
                System.Drawing.Point[] pointArr = new System.Drawing.Point[8];
                pointArr[0] = new System.Drawing.Point(rectangle.X + 2, rectangle.Y);
                pointArr[1] = new System.Drawing.Point(rectangle.Right - 3, rectangle.Y);
                pointArr[2] = new System.Drawing.Point(rectangle.Right - 1, rectangle.Y + 2);
                pointArr[3] = new System.Drawing.Point(rectangle.Right - 1, rectangle.Bottom - 4);
                pointArr[4] = new System.Drawing.Point(rectangle.Right - 4, rectangle.Bottom - 1);
                pointArr[5] = new System.Drawing.Point(rectangle.X + 2, rectangle.Bottom - 1);
                pointArr[6] = new System.Drawing.Point(rectangle.X, rectangle.Bottom - 4);
                pointArr[7] = new System.Drawing.Point(rectangle.X, rectangle.Y + 2);
                graphicsPath.AddPolygon(pointArr);
                return new System.Drawing.Region(graphicsPath);
            }
            return base.OnCreateMaskRegion(tip, layout);
        }

        protected override void OnDrawWindow(System.Windows.Forms.PaintEventArgs e, Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout)
        {
            System.Drawing.Drawing2D.GraphicsPath graphicsPath1;

            System.Drawing.Rectangle rectangle1 = layout.WindowBounds;
            bool flag = BackgroundEffect == Skybound.VisualTips.Rendering.VisualTipOfficeBackgroundEffect.Glass;
            if (RoundCorners)
            {
                graphicsPath1 = Skybound.VisualTips.Rendering.VisualTipRenderer.CreateRoundRectPath(rectangle1, Skybound.VisualTips.Rendering.VisualTipRenderer.LayeredWindowsSupported ? 5 : 7, Skybound.VisualTips.Rendering.VisualTipRenderer.BorderCorners.All);
            }
            else
            {
                graphicsPath1 = new System.Drawing.Drawing2D.GraphicsPath();
                graphicsPath1.AddRectangle(new System.Drawing.Rectangle(rectangle1.Location, rectangle1.Size - (new System.Drawing.Size(1, 1))));
            }
            using (System.Drawing.Brush brush = new System.Drawing.Drawing2D.LinearGradientBrush(rectangle1, BackColor, BackColorGradient, (float)(GradientAngle + (flag ? 180 : 0)), false))
            {
                e.Graphics.FillPath(brush, graphicsPath1);
            }
            if (flag)
            {
                System.Drawing.Color color3 = BackColor;
                int i1 = (int)((1.0F - ((1.0F - color3.GetBrightness()) * 0.75F)) * 255.0F);
                System.Drawing.Color color1 = System.Drawing.Color.FromArgb(i1, i1, i1);
                using (System.Drawing.Drawing2D.LinearGradientBrush linearGradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(rectangle1, System.Drawing.Color.FromArgb(128, color1), System.Drawing.Color.FromArgb(0, color1), System.Drawing.Drawing2D.LinearGradientMode.Vertical))
                using (System.Drawing.Drawing2D.GraphicsPath graphicsPath2 = CreateGlarePath(rectangle1, Skybound.VisualTips.Rendering.VisualTipRenderer.LayeredWindowsSupported ? 5 : 7))
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    e.Graphics.FillPath(linearGradientBrush, graphicsPath2);
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                }
            }
            using (System.Drawing.Pen pen1 = new System.Drawing.Pen(BorderColor))
            {
                if (Skybound.VisualTips.Rendering.VisualTipRenderer.LayeredWindowsSupported)
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                else
                    pen1.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                e.Graphics.DrawPath(pen1, graphicsPath1);
                if (Skybound.VisualTips.Rendering.VisualTipRenderer.LayeredWindowsSupported)
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            }
            if (HasFooter(tip))
            {
                System.Drawing.Rectangle rectangle2 = layout.GetElementBounds(Skybound.VisualTips.Rendering.VisualTipRenderElement.FooterImage);
                int i2 = rectangle2.Top - 6;
                System.Drawing.Color color4 = BackColorGradient;
                System.Drawing.Color color5 = BackColorGradient;
                System.Drawing.Color color6 = BackColorGradient;
                System.Drawing.Color color2 = System.Drawing.Color.FromArgb((int)((float)color4.R * 0.9F), (int)((float)color5.G * 0.9F), (int)((float)color6.B * 0.9F));
                using (System.Drawing.Pen pen2 = new System.Drawing.Pen(color2))
                {
                    e.Graphics.DrawLine(pen2, rectangle1.X + 5, i2, rectangle1.Right - 6, i2);
                    pen2.Color = System.Windows.Forms.ControlPaint.Light(BackColor);
                    e.Graphics.DrawLine(pen2, rectangle1.X + 5, i2 + 1, rectangle1.Right - 6, i2 + 1);
                }
            }
        }

    } // class VisualTipOfficeRenderer

}

