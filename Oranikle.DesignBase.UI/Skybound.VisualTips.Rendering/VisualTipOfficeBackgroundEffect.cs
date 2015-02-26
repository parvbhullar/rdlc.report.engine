using System.ComponentModel;

namespace Skybound.VisualTips.Rendering
{

    [System.ComponentModel.Editor(typeof(Skybound.ComponentModel.EnumTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum VisualTipOfficeBackgroundEffect
    {
        [System.ComponentModel.Description("The background of a tip is drawn using a simple gradient.")]
        Gradient,
        [System.ComponentModel.Description("The background of a tip is drawn using a \"glass\" effect.")]
        Glass
    }

}

