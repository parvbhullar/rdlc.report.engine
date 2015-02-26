using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Skybound.ComponentModel;
using Skybound.VisualTips;
using Skybound.Windows.Forms;

namespace Skybound.VisualTips.Rendering
{

    [System.ComponentModel.TypeConverter(typeof(Skybound.VisualTips.Rendering.VisualTipRenderer.VisualTipRendererConverter))]
    [Skybound.ComponentModel.DisplayName("Standard")]
    public class VisualTipRenderer : Skybound.ComponentModel.IPropertyListContainer, System.ComponentModel.ICustomTypeDescriptor
    {

        [System.Flags]
        internal enum BorderCorners
        {
            All = 15,
            TopLeft = 1,
            TopRight = 2,
            BottomLeft = 4,
            BottomRight = 8
        }

        private class ImageAndTextLayout
        {

            public System.Drawing.Rectangle Bounds;
            public System.Drawing.Rectangle ImageBounds;
            public System.Drawing.Rectangle TextBounds;
            public int TitleTextHeight;

            public ImageAndTextLayout(Skybound.VisualTips.Rendering.VisualTipRenderer renderer, System.Drawing.Image image, System.Drawing.Font titleFont, string titleText, System.Drawing.Font textFont, string text, int width, int pad)
            {
                ImageBounds = System.Drawing.Rectangle.Empty;
                TextBounds = System.Drawing.Rectangle.Empty;
                ImageBounds.Size = image != null ? image.Size : System.Drawing.Size.Empty;
                if (image != null)
                {
                    TextBounds.X = ImageBounds.Width + pad;
                    TextBounds.Width = width - ImageBounds.Width - pad;
                }
                else
                {
                    TextBounds.Width = width;
                }
                TextBounds.Height = 0;
                if (titleText.Length > 0)
                {
                    System.Drawing.Size size1 = Skybound.Windows.Forms.TextRenderer.MeasureText(titleText, titleFont, new System.Drawing.Size(TextBounds.Width, 0), Skybound.Windows.Forms.TextFormatFlags.NoPrefix | Skybound.Windows.Forms.TextFormatFlags.WordBreak);
                    int i = size1.Height;
                    TitleTextHeight = size1.Height;
                    TextBounds.Height = i;
                }
                if (text.Length > 0)
                {
                    System.Drawing.Size size2 = Skybound.Windows.Forms.TextRenderer.MeasureText(text, textFont, new System.Drawing.Size(TextBounds.Width, 0), Skybound.Windows.Forms.TextFormatFlags.NoPrefix | Skybound.Windows.Forms.TextFormatFlags.WordBreak);
                    TextBounds.Height += size2.Height;
                }
                Bounds = System.Drawing.Rectangle.FromLTRB(0, 0, width, System.Math.Max(ImageBounds.Bottom, TextBounds.Bottom));
            }

            public void Offset(int x, int y)
            {
                ImageBounds.Offset(x, y);
                TextBounds.Offset(x, y);
                Bounds.Offset(x, y);
            }

            public void SwapRtl()
            {
                TextBounds.X = Bounds.X;
                ImageBounds.X = Bounds.Right - ImageBounds.Width;
            }

        } // class ImageAndTextLayout

        internal class VisualTipRendererConverter : Skybound.ComponentModel.RendererConverter
        {

            public VisualTipRendererConverter() : base(typeof(Skybound.VisualTips.Rendering.VisualTipRenderer))
            {
            }

        } // class VisualTipRendererConverter

        [Skybound.ComponentModel.DisplayName("(Default)")]
        private class VisualTipsDefaultRenderer : Skybound.VisualTips.Rendering.VisualTipRenderer
        {

            public VisualTipsDefaultRenderer()
            {
            }

            protected override System.Drawing.Font GetElementFont(Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipRenderElement element)
            {
                if (Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault() != null)
                    return Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault().GetElementFont(tip, element);
                return base.GetElementFont(tip, element);
            }

            protected override System.Drawing.Color GetElementTextColor(Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipRenderElement element)
            {
                if (Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault() != null)
                    return Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault().GetElementTextColor(tip, element);
                return base.GetElementTextColor(tip, element);
            }

            protected override Skybound.VisualTips.Rendering.VisualTipLayout OnCreateLayout(Skybound.VisualTips.VisualTip tip)
            {
                if (Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault() != null)
                    return Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault().OnCreateLayout(tip);
                return base.OnCreateLayout(tip);
            }

            protected override System.Drawing.Region OnCreateMaskRegion(Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout)
            {
                if (Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault() != null)
                    return Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault().OnCreateMaskRegion(tip, layout);
                return base.OnCreateMaskRegion(tip, layout);
            }

            protected override void OnDrawElement(System.Windows.Forms.PaintEventArgs e, Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout, Skybound.VisualTips.Rendering.VisualTipRenderElement element)
            {
                if (Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault() == null)
                {
                    base.OnDrawElement(e, tip, layout, element);
                    return;
                }
                Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault().OnDrawElement(e, tip, layout, element);
            }

            protected override void OnDrawShadow(System.Windows.Forms.PaintEventArgs e, Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout)
            {
                if (Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault() == null)
                {
                    base.OnDrawShadow(e, tip, layout);
                    return;
                }
                Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault().OnDrawShadow(e, tip, layout);
            }

            protected override void OnDrawWindow(System.Windows.Forms.PaintEventArgs e, Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout)
            {
                if (Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault() == null)
                {
                    base.OnDrawWindow(e, tip, layout);
                    return;
                }
                Skybound.VisualTips.Rendering.VisualTipRenderer.GetDefault().OnDrawWindow(e, tip, layout);
            }

        } // class VisualTipsDefaultRenderer

        private const int Pad = 6;

        internal Skybound.ComponentModel.PropertyList Properties;

        private static Skybound.VisualTips.Rendering.VisualTipRenderer _Default;
        private static Skybound.VisualTips.Rendering.VisualTipRenderer _DefaultRenderer;

        public static event System.EventHandler DefaultRendererChanged;

        public static Skybound.VisualTips.Rendering.VisualTipRenderer DefaultRenderer
        {
            get
            {
                return Skybound.VisualTips.Rendering.VisualTipRenderer._DefaultRenderer;
            }
        }

        internal static bool LayeredWindowsSupported
        {
            get
            {
                return System.Windows.Forms.OSFeature.Feature.IsPresent(System.Windows.Forms.OSFeature.LayeredWindows);
            }
        }

        public VisualTipRenderer()
        {
            Properties = new Skybound.ComponentModel.PropertyList();
        }

        static VisualTipRenderer()
        {
            Skybound.VisualTips.Rendering.VisualTipRenderer._DefaultRenderer = new Skybound.VisualTips.Rendering.VisualTipRenderer.VisualTipsDefaultRenderer();
        }

        public Skybound.VisualTips.Rendering.VisualTipLayout CreateLayout(Skybound.VisualTips.VisualTip tip)
        {
            ValidateTip(tip);
            Skybound.VisualTips.Rendering.VisualTipLayout visualTipLayout = OnCreateLayout(tip);
            if (visualTipLayout == null)
                throw new System.InvalidOperationException("A null value may not be returned by OnCreateLayout.");
            return visualTipLayout;
        }

        public System.Drawing.Region CreateMaskRegion(Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout)
        {
            ValidateTip(tip);
            if (layout == null)
                layout = OnCreateLayout(tip);
            return OnCreateMaskRegion(tip, layout);
        }

        public void Draw(System.Windows.Forms.PaintEventArgs e, Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout)
        {
            ValidateParameters(e, tip, ref layout);
            OnDrawShadow(e, tip, layout);
            OnDrawWindow(e, tip, layout);
            foreach (Skybound.VisualTips.Rendering.VisualTipRenderElement visualTipRenderElement in System.Enum.GetValues(typeof(Skybound.VisualTips.Rendering.VisualTipRenderElement)))
            {
                OnDrawElement(e, tip, layout, visualTipRenderElement);
            }
        }

        System.ComponentModel.AttributeCollection System.ComponentModel.ICustomTypeDescriptor.GetAttributes()
        {
            return System.ComponentModel.TypeDescriptor.GetAttributes(this, true);
        }

        protected string GetBodyText(Skybound.VisualTips.VisualTip tip)
        {
            ValidateTip(tip);
            if (tip.Enabled || (tip.DisabledText.Length <= 0))
                return tip.Text;
            return tip.DisabledText;
        }

        string System.ComponentModel.ICustomTypeDescriptor.GetClassName()
        {
            return System.ComponentModel.TypeDescriptor.GetClassName(this, true);
        }

        string System.ComponentModel.ICustomTypeDescriptor.GetComponentName()
        {
            return System.ComponentModel.TypeDescriptor.GetComponentName(this, true);
        }

        System.ComponentModel.TypeConverter System.ComponentModel.ICustomTypeDescriptor.GetConverter()
        {
            return System.ComponentModel.TypeDescriptor.GetConverter(this, true);
        }

        System.ComponentModel.EventDescriptor System.ComponentModel.ICustomTypeDescriptor.GetDefaultEvent()
        {
            return System.ComponentModel.TypeDescriptor.GetDefaultEvent(this, true);
        }

        System.ComponentModel.PropertyDescriptor System.ComponentModel.ICustomTypeDescriptor.GetDefaultProperty()
        {
            return System.ComponentModel.TypeDescriptor.GetDefaultProperty(this, true);
        }

        object System.ComponentModel.ICustomTypeDescriptor.GetEditor(System.Type editorBaseType)
        {
            return System.ComponentModel.TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        System.ComponentModel.EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents(System.Attribute[] attributes)
        {
            return System.ComponentModel.TypeDescriptor.GetEvents(this, attributes, true);
        }

        System.ComponentModel.EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents()
        {
            return System.ComponentModel.TypeDescriptor.GetEvents(this, true);
        }

        System.ComponentModel.PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties(System.Attribute[] attributes)
        {
            return Skybound.ComponentModel.PropertyList.GetProperties(this, attributes);
        }

        System.ComponentModel.PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties()
        {
            return null;//GetProperties(null);
        }

        Skybound.ComponentModel.PropertyList Skybound.ComponentModel.IPropertyListContainer.GetPropertyList()
        {
            return Properties;
        }

        object System.ComponentModel.ICustomTypeDescriptor.GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
        {
            return this;
        }

        protected string GetTitleText(Skybound.VisualTips.VisualTip tip)
        {
            ValidateTip(tip);
            string s = "";
            if (tip.Shortcut != System.Windows.Forms.Shortcut.None)
                s = "(" + System.ComponentModel.TypeDescriptor.GetConverter(typeof(System.Windows.Forms.Keys)).ConvertToString(tip.Shortcut) + ")";
            if (tip.Title.Length <= 0)
                return s;
            return tip.Title + " " + s;
        }

        protected bool HasFooter(Skybound.VisualTips.VisualTip tip)
        {
            ValidateTip(tip);
            if (!tip.HideFooter)
            {
                if (tip.FooterText.Length <= 0)
                    return tip.FooterImage != null;
                return true;
            }
            return false;
        }

        protected bool HasShadow(Skybound.VisualTips.VisualTip tip)
        {
            ValidateTip(tip);
            if (Skybound.VisualTips.Rendering.VisualTipRenderer.LayeredWindowsSupported)
            {
                if (tip.Provider.Shadow == Skybound.VisualTips.VisualTipShadow.Enabled)
                    return true;
                if (tip.Provider.Shadow == Skybound.VisualTips.VisualTipShadow.SystemDefault)
                    return Skybound.VisualTips.SystemParameters.ToolTipShadow;
            }
            return false;
        }

        internal System.Drawing.Color MakeSolidColor(System.Drawing.Color color)
        {
            if (color.Equals(System.Drawing.Color.Empty) || (color.A == 255))
                return color;
            return System.Drawing.Color.FromArgb(255, color);
        }

        protected System.Drawing.Size MeasureElement(Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipRenderElement element, int maximumWidth)
        {
            ValidateTip(tip);
            if (element == Skybound.VisualTips.Rendering.VisualTipRenderElement.Image)
            {
                if (tip.Image != null)
                    return tip.Image.Size;
                return System.Drawing.Size.Empty;
            }
            if (element == Skybound.VisualTips.Rendering.VisualTipRenderElement.FooterImage)
            {
                if (tip.FooterImage != null)
                    return tip.FooterImage.Size;
                return System.Drawing.Size.Empty;
            }
            if (element == Skybound.VisualTips.Rendering.VisualTipRenderElement.TitleImage)
            {
                if (tip.TitleImage != null)
                    return tip.TitleImage.Size;
                return System.Drawing.Size.Empty;
            }
            string s = null;
            switch (element)
            {
                case Skybound.VisualTips.Rendering.VisualTipRenderElement.DisabledMessage:
                    s = tip.DisabledMessage;
                    break;

                case Skybound.VisualTips.Rendering.VisualTipRenderElement.FooterText:
                    s = tip.FooterText;
                    break;

                case Skybound.VisualTips.Rendering.VisualTipRenderElement.Text:
                    s = GetBodyText(tip);
                    break;

                case Skybound.VisualTips.Rendering.VisualTipRenderElement.Title:
                    s = GetTitleText(tip);
                    break;
            }
            if (s != null)
                return MeasureText(s, GetElementFont(tip, element), tip.Provider.MaximumWidth);
            return System.Drawing.Size.Empty;
        }

        private System.Drawing.Size MeasureText(string text, System.Drawing.Font font, int maximumWidth)
        {
            return Skybound.Windows.Forms.TextRenderer.MeasureText(text, font, new System.Drawing.Size(maximumWidth == 0 ? 32767 : maximumWidth, 0), Skybound.Windows.Forms.TextFormatFlags.NoPrefix | Skybound.Windows.Forms.TextFormatFlags.WordBreak);
        }

        internal void OnPropertyChanged(string propertyName)
        {
            Properties.OnPropertyChanged(propertyName);
        }

        private void ValidatePaintEventArgs(System.Windows.Forms.PaintEventArgs e)
        {
            if (e == null)
                throw new System.ArgumentNullException("e");
            if (e.Graphics == null)
                throw new System.ArgumentException("The Graphics specified by PaintEventArgs may not be a null reference.", "e");
        }

        private void ValidateParameters(System.Windows.Forms.PaintEventArgs e, Skybound.VisualTips.VisualTip tip, ref Skybound.VisualTips.Rendering.VisualTipLayout layout)
        {
            ValidatePaintEventArgs(e);
            ValidateTip(tip);
            if (layout == null)
                layout = CreateLayout(tip);
        }

        private void ValidateTip(Skybound.VisualTips.VisualTip tip)
        {
            if (tip == null)
                throw new System.ArgumentNullException("tip");
            if (tip.Provider == null)
                throw new System.ArgumentException("The tip must be associated with a VisualTipProvider before it is rendered.");
        }

        protected virtual System.Drawing.Font GetElementFont(Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipRenderElement element)
        {
            ValidateTip(tip);
            if ((element == Skybound.VisualTips.Rendering.VisualTipRenderElement.Title) || (element == Skybound.VisualTips.Rendering.VisualTipRenderElement.DisabledMessage) || (element == Skybound.VisualTips.Rendering.VisualTipRenderElement.FooterText))
                return new System.Drawing.Font(tip.Font, System.Drawing.FontStyle.Bold);
            return tip.Font;
        }

        protected virtual System.Drawing.Color GetElementTextColor(Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipRenderElement element)
        {
            ValidateTip(tip);
            return System.Drawing.SystemColors.InfoText;
        }

        protected virtual Skybound.VisualTips.Rendering.VisualTipLayout OnCreateLayout(Skybound.VisualTips.VisualTip tip)
        {
            Skybound.VisualTips.Rendering.VisualTipRenderer.ImageAndTextLayout imageAndTextLayout2;

            Skybound.VisualTips.Rendering.VisualTipLayout visualTipLayout = new Skybound.VisualTips.Rendering.VisualTipLayout();
            string s1 = GetTitleText(tip);
            string s2 = GetBodyText(tip);
            bool flag1 = s1.Length > 0;
            bool flag2 = (s2.Length > 0) || (tip.Image != null);
            System.Drawing.Font font1 = GetElementFont(tip, Skybound.VisualTips.Rendering.VisualTipRenderElement.Title);
            System.Drawing.Font font2 = GetElementFont(tip, Skybound.VisualTips.Rendering.VisualTipRenderElement.Text);
            System.Drawing.Font font3 = GetElementFont(tip, Skybound.VisualTips.Rendering.VisualTipRenderElement.FooterText);
            System.Drawing.Font font4 = GetElementFont(tip, Skybound.VisualTips.Rendering.VisualTipRenderElement.DisabledMessage);
            System.Drawing.Size size1 = MeasureText(s1, font1, 0);
            int i1 = flag1 ? size1.Width : 0;
            System.Drawing.Size size2 = MeasureText(s2, font2, 0);
            int i2 = flag2 ? size2.Width : 0;
            System.Drawing.Size size3 = MeasureText(tip.FooterText, font3, 0);
            int i3 = HasFooter(tip) ? size3.Width : 0;
            if (!tip.Enabled && (tip.DisabledMessage.Length > 0))
            {
                System.Drawing.Size size4 = MeasureText(tip.DisabledMessage, font4, 0);
                i2 = System.Math.Max(size4.Width, i2);
            }
            if (tip.TitleImage != null)
                i1 += tip.TitleImage.Width + 6;
            if (tip.Image != null)
                i2 += tip.Image.Width + 6;
            if (HasFooter(tip) && (tip.FooterImage != null))
                i3 += tip.FooterImage.Width + 6;
            int i4 = System.Math.Max(System.Math.Max(i1, i2), i3);
            int i5 = System.Math.Min(tip.MaximumWidth - 6 - 6, i4);
            int i6 = 6;
            Skybound.VisualTips.Rendering.VisualTipRenderer.ImageAndTextLayout imageAndTextLayout1 = new Skybound.VisualTips.Rendering.VisualTipRenderer.ImageAndTextLayout(this, tip.TitleImage, GetElementFont(tip, Skybound.VisualTips.Rendering.VisualTipRenderElement.Title), s1, null, "", i5, 6);
            imageAndTextLayout1.Offset(6, 6);
            if (imageAndTextLayout1.Bounds.Height > 0)
                i6 += imageAndTextLayout1.Bounds.Height + 6;
            if (tip.Enabled)
                imageAndTextLayout2 = new Skybound.VisualTips.Rendering.VisualTipRenderer.ImageAndTextLayout(this, tip.Image, null, "", GetElementFont(tip, Skybound.VisualTips.Rendering.VisualTipRenderElement.Text), s2, i5, 6);
            else
                imageAndTextLayout2 = new Skybound.VisualTips.Rendering.VisualTipRenderer.ImageAndTextLayout(this, tip.Image, GetElementFont(tip, Skybound.VisualTips.Rendering.VisualTipRenderElement.DisabledMessage), tip.DisabledMessage, GetElementFont(tip, Skybound.VisualTips.Rendering.VisualTipRenderElement.Text), s2, i5, 6);
            imageAndTextLayout2.Offset(6, i6);
            if (tip.RightToLeft == System.Windows.Forms.RightToLeft.Yes)
                imageAndTextLayout2.SwapRtl();
            if (imageAndTextLayout2.Bounds.Height > 0)
                i6 += imageAndTextLayout2.Bounds.Height + 6;
            Skybound.VisualTips.Rendering.VisualTipRenderer.ImageAndTextLayout imageAndTextLayout3 = HasFooter(tip) ? new Skybound.VisualTips.Rendering.VisualTipRenderer.ImageAndTextLayout(this, tip.FooterImage, GetElementFont(tip, Skybound.VisualTips.Rendering.VisualTipRenderElement.FooterText), tip.FooterText, null, "", i5, 6) : new Skybound.VisualTips.Rendering.VisualTipRenderer.ImageAndTextLayout(this, null, null, "", null, "", i5, 6);
            imageAndTextLayout3.Offset(6, i6);
            if (tip.RightToLeft == System.Windows.Forms.RightToLeft.Yes)
                imageAndTextLayout3.SwapRtl();
            if (imageAndTextLayout3.Bounds.Height > 0)
                imageAndTextLayout3.Offset(0, 6);
            visualTipLayout.WindowBounds = new System.Drawing.Rectangle(0, 0, 6 + i5 + 6, imageAndTextLayout3.Bounds.Bottom + (HasFooter(tip) ? 6 : 0));
            visualTipLayout.SetElementBounds(Skybound.VisualTips.Rendering.VisualTipRenderElement.TitleImage, imageAndTextLayout1.ImageBounds);
            visualTipLayout.SetElementBounds(Skybound.VisualTips.Rendering.VisualTipRenderElement.Title, imageAndTextLayout1.TextBounds);
            visualTipLayout.SetElementBounds(Skybound.VisualTips.Rendering.VisualTipRenderElement.Image, imageAndTextLayout2.ImageBounds);
            visualTipLayout.SetElementBounds(Skybound.VisualTips.Rendering.VisualTipRenderElement.FooterImage, imageAndTextLayout3.ImageBounds);
            visualTipLayout.SetElementBounds(Skybound.VisualTips.Rendering.VisualTipRenderElement.FooterText, imageAndTextLayout3.TextBounds);
            if (imageAndTextLayout2.TitleTextHeight > 0)
            {
                System.Drawing.Rectangle rectangle1 = imageAndTextLayout2.TextBounds;
                rectangle1.Height = imageAndTextLayout2.TitleTextHeight;
                visualTipLayout.SetElementBounds(Skybound.VisualTips.Rendering.VisualTipRenderElement.DisabledMessage, rectangle1);
                rectangle1.Y = rectangle1.Bottom;
                rectangle1.Height = imageAndTextLayout2.TextBounds.Height - imageAndTextLayout2.TitleTextHeight;
                visualTipLayout.SetElementBounds(Skybound.VisualTips.Rendering.VisualTipRenderElement.Text, rectangle1);
            }
            else
            {
                visualTipLayout.SetElementBounds(Skybound.VisualTips.Rendering.VisualTipRenderElement.DisabledMessage, System.Drawing.Rectangle.Empty);
                visualTipLayout.SetElementBounds(Skybound.VisualTips.Rendering.VisualTipRenderElement.Text, imageAndTextLayout2.TextBounds);
            }
            if (HasShadow(tip))
            {
                System.Drawing.Rectangle rectangle2 = visualTipLayout.WindowBounds;
                System.Drawing.Rectangle rectangle3 = visualTipLayout.WindowBounds;
                visualTipLayout.ShadowBounds = new System.Drawing.Rectangle(rectangle2.Location, rectangle3.Size + (new System.Drawing.Size(5, 5)));
                if (tip.RightToLeft == System.Windows.Forms.RightToLeft.Yes)
                    visualTipLayout.OffsetWindow(5, 0);
            }
            else
            {
                visualTipLayout.ShadowBounds = visualTipLayout.WindowBounds;
            }
            return visualTipLayout;
        }

        protected virtual System.Drawing.Region OnCreateMaskRegion(Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout)
        {
            return null;
        }

        protected virtual void OnDrawElement(System.Windows.Forms.PaintEventArgs e, Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout, Skybound.VisualTips.Rendering.VisualTipRenderElement element)
        {
            System.Drawing.Rectangle rectangle1 = layout.GetElementBounds(element);
            string s = "";
            System.Drawing.Image image = null;
            System.Drawing.Size size = rectangle1.Size;
            if (size.IsEmpty)
                return;
            switch (element)
            {
                case Skybound.VisualTips.Rendering.VisualTipRenderElement.DisabledMessage:
                    s = tip.DisabledMessage;
                    break;

                case Skybound.VisualTips.Rendering.VisualTipRenderElement.FooterText:
                    s = tip.FooterText;
                    break;

                case Skybound.VisualTips.Rendering.VisualTipRenderElement.Text:
                    s = GetBodyText(tip);
                    break;

                case Skybound.VisualTips.Rendering.VisualTipRenderElement.Title:
                    s = GetTitleText(tip);
                    break;

                case Skybound.VisualTips.Rendering.VisualTipRenderElement.TitleImage:
                    image = tip.TitleImage;
                    break;

                case Skybound.VisualTips.Rendering.VisualTipRenderElement.Image:
                    image = tip.Image;
                    break;

                case Skybound.VisualTips.Rendering.VisualTipRenderElement.FooterImage:
                    image = tip.FooterImage;
                    break;
            }
            if (s.Length > 0)
            {
                using (Skybound.Windows.Forms.BufferedGraphics bufferedGraphics = new Skybound.Windows.Forms.BufferedGraphics())
                {
                    bufferedGraphics.SetTarget(e.Graphics, rectangle1);
                    System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle(0, 0, rectangle1.Width, rectangle1.Height);
                    layout.Offset(-rectangle1.X, -rectangle1.Y);
                    OnDrawWindow(new System.Windows.Forms.PaintEventArgs(bufferedGraphics.Graphics, rectangle2), tip, layout);
                    layout.Offset(rectangle1.X, rectangle1.Y);
                    Skybound.Windows.Forms.TextFormatFlags textFormatFlags = Skybound.Windows.Forms.TextFormatFlags.NoPrefix | Skybound.Windows.Forms.TextFormatFlags.WordBreak;
                    if (tip.RightToLeft == System.Windows.Forms.RightToLeft.Yes)
                        textFormatFlags = (Skybound.Windows.Forms.TextFormatFlags)(textFormatFlags | (Skybound.Windows.Forms.TextFormatFlags.Right | Skybound.Windows.Forms.TextFormatFlags.RightToLeft));
                    Skybound.Windows.Forms.TextRenderer.DrawText(bufferedGraphics.Graphics, s, GetElementFont(tip, element), GetElementTextColor(tip, element), System.Drawing.Color.Transparent, rectangle2, textFormatFlags);
                    bufferedGraphics.Render();
                    return;
                }
            }
            if (image != null)
                e.Graphics.DrawImage(image, rectangle1);
        }

        protected virtual void OnDrawShadow(System.Windows.Forms.PaintEventArgs e, Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout)
        {
            if (HasShadow(tip) && System.Drawing.Rectangle.Intersect(layout.ShadowBounds, layout.WindowBounds) != layout.ShadowBounds)
                Skybound.VisualTips.Rendering.VisualTipRenderer.DrawDropShadow(e.Graphics, layout.ShadowBounds, 8, System.Drawing.Color.FromArgb(128, System.Drawing.Color.Black));
        }

        protected virtual void OnDrawWindow(System.Windows.Forms.PaintEventArgs e, Skybound.VisualTips.VisualTip tip, Skybound.VisualTips.Rendering.VisualTipLayout layout)
        {
            System.Drawing.Rectangle rectangle = layout.WindowBounds;
            e.Graphics.FillRectangle(System.Drawing.SystemBrushes.Info, rectangle);
            e.Graphics.DrawRectangle(System.Drawing.SystemPens.InfoText, new System.Drawing.Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1));
        }

        internal static System.Drawing.Drawing2D.GraphicsPath CreateRoundRectPath(System.Drawing.Rectangle bounds, int radius, Skybound.VisualTips.Rendering.VisualTipRenderer.BorderCorners corners)
        {
            System.Drawing.Drawing2D.GraphicsPath graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
            if ((corners & Skybound.VisualTips.Rendering.VisualTipRenderer.BorderCorners.TopLeft) == 0)
                graphicsPath.AddLine(bounds.X, bounds.Y, bounds.X, bounds.Y);
            else
                graphicsPath.AddArc(new System.Drawing.Rectangle(bounds.X, bounds.Y, radius, radius), 180.0F, 90.0F);
            if ((corners & Skybound.VisualTips.Rendering.VisualTipRenderer.BorderCorners.TopRight) == 0)
                graphicsPath.AddLine(bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Y);
            else
                graphicsPath.AddArc(new System.Drawing.Rectangle(bounds.Right - radius - 1, bounds.Y, radius, radius), 270.0F, 90.0F);
            if ((corners & Skybound.VisualTips.Rendering.VisualTipRenderer.BorderCorners.BottomRight) == 0)
                graphicsPath.AddLine(bounds.Right - 1, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
            else
                graphicsPath.AddArc(new System.Drawing.Rectangle(bounds.Right - radius - 1, bounds.Bottom - radius - 1, radius, radius), 0.0F, 90.0F);
            if ((corners & Skybound.VisualTips.Rendering.VisualTipRenderer.BorderCorners.BottomLeft) == 0)
                graphicsPath.AddLine(bounds.X, bounds.Bottom - 1, bounds.X, bounds.Bottom - 1);
            else
                graphicsPath.AddArc(new System.Drawing.Rectangle(bounds.X, bounds.Bottom - radius - 1, radius, radius), 90.0F, 90.0F);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        internal static void DrawDropShadow(System.Drawing.Graphics graphics, System.Drawing.Rectangle bounds, int radius, System.Drawing.Color color)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(color.A / 4, color.R, color.G, color.B)))
            {
                for (int i = 0; i < 4; i++)
                {
                    bounds.Inflate(-1, -1);
                    using (System.Drawing.Drawing2D.GraphicsPath graphicsPath = Skybound.VisualTips.Rendering.VisualTipRenderer.CreateRoundRectPath(bounds, radius, Skybound.VisualTips.Rendering.VisualTipRenderer.BorderCorners.All))
                    {
                        graphics.FillPath(brush, graphicsPath);
                    }
                }
            }
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
        }

        public static Skybound.VisualTips.Rendering.VisualTipRenderer GetDefault()
        {
            return Skybound.VisualTips.Rendering.VisualTipRenderer._Default;
        }

        private static void OnDefaultRendererChanged(System.EventArgs e)
        {
            if (Skybound.VisualTips.Rendering.VisualTipRenderer.DefaultRendererChanged != null)
                Skybound.VisualTips.Rendering.VisualTipRenderer.DefaultRendererChanged(null, e);
        }

        public static void SetDefault(Skybound.VisualTips.Rendering.VisualTipRenderer renderer)
        {
            if (renderer == Skybound.VisualTips.Rendering.VisualTipRenderer.DefaultRenderer)
                return;
            if (Skybound.VisualTips.Rendering.VisualTipRenderer._Default != renderer)
            {
                Skybound.VisualTips.Rendering.VisualTipRenderer._Default = renderer;
                Skybound.VisualTips.Rendering.VisualTipRenderer.OnDefaultRendererChanged(System.EventArgs.Empty);
            }
        }

    } // class VisualTipRenderer

}

