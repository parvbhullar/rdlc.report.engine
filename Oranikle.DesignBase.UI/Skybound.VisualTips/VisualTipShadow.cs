using System.ComponentModel;

namespace Skybound.VisualTips
{

    [System.ComponentModel.Editor(typeof(Skybound.ComponentModel.EnumTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum VisualTipShadow
    {
        [System.ComponentModel.Description("A soft shadow is displayed when it is supported by the operating system and window shadows are enabled.")]
        SystemDefault,
        [System.ComponentModel.Description("No soft shadows are displayed.")]
        Disabled,
        [System.ComponentModel.Description("A soft shadow is always displayed when it is supported by the operating system.")]
        Enabled
    }

}

