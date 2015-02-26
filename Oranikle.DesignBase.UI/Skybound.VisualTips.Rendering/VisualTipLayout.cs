using System;
using System.ComponentModel;
using System.Drawing;

namespace Skybound.VisualTips.Rendering
{

    public class VisualTipLayout
    {

        private System.Drawing.Rectangle[] Rectangles;

        public System.Drawing.Rectangle ShadowBounds
        {
            get
            {
                return Rectangles[0];
            }
            set
            {
                Rectangles[0] = value;
            }
        }

        public System.Drawing.Rectangle WindowBounds
        {
            get
            {
                return Rectangles[1];
            }
            set
            {
                Rectangles[1] = value;
            }
        }

        public VisualTipLayout()
        {
            Rectangles = new System.Drawing.Rectangle[9];
        }

        public System.Drawing.Rectangle GetElementBounds(Skybound.VisualTips.Rendering.VisualTipRenderElement element)
        {
            if (!System.Enum.IsDefined(typeof(Skybound.VisualTips.Rendering.VisualTipRenderElement), element))
                throw new System.ComponentModel.InvalidEnumArgumentException("element", (int)element, typeof(Skybound.VisualTips.Rendering.VisualTipRenderElement));
            return Rectangles[(int)element + 2];
        }

        public System.Drawing.Size GetSize()
        {
            System.Drawing.Rectangle rectangle = System.Drawing.Rectangle.Union(Rectangles[0], Rectangles[1]);
            return rectangle.Size;
        }

        public void Offset(int x, int y)
        {
            for (int i = 0; i < Rectangles.Length; i++)
            {
                Rectangles[i].Offset(x, y);
            }
        }

        internal void OffsetWindow(int x, int y)
        {
            for (int i = 1; i < Rectangles.Length; i++)
            {
                Rectangles[i].Offset(x, y);
            }
        }

        public void SetElementBounds(Skybound.VisualTips.Rendering.VisualTipRenderElement element, System.Drawing.Rectangle value)
        {
            if (!System.Enum.IsDefined(typeof(Skybound.VisualTips.Rendering.VisualTipRenderElement), element))
                throw new System.ComponentModel.InvalidEnumArgumentException("element", (int)element, typeof(Skybound.VisualTips.Rendering.VisualTipRenderElement));
            Rectangles[(int)element + 2] = value;
        }

    } // class VisualTipLayout

}

