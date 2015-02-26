using System;
using System.Drawing;
using System.Windows.Forms;

namespace Skybound.VisualTips
{

    public interface IVisualTipExtender
    {

        System.Drawing.Rectangle GetBounds(object component);

        object GetChildAtPoint(System.Windows.Forms.Control control, int x, int y);

        System.Type[] GetChildTypes();

        object GetParent(object component);

    }

}

