using System.ComponentModel;

namespace Skybound.VisualTips
{

    [System.ComponentModel.Editor(typeof(Skybound.ComponentModel.EnumTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public enum VisualTipDisplayMode
    {
        [System.ComponentModel.Description("Tips are displayed when the mouse hovers over the tool area.")]
        MouseHover,
        [System.ComponentModel.Description("Tips are displayed when F1 is pressed or the dialog box help button is used.")]
        HelpRequested,
        [System.ComponentModel.Description("Tips must be displayed manually by calling VisualTipProvider.ShowTip.")]
        Manual
    }

}

