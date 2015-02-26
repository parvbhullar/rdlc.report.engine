using System.ComponentModel;

namespace Skybound.VisualTips
{

    [System.ComponentModel.Editor(typeof(Skybound.ComponentModel.EnumTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum VisualTipAnimation
    {
        [System.ComponentModel.Description("Tips are animated when it is supported by the operating system and window animation is enabled.")]
        SystemDefault,
        [System.ComponentModel.Description("Tips are not animated.")]
        Disabled,
        [System.ComponentModel.Description("Tips are always animated if it is supported by the operating system.")]
        Enabled
    }

}

