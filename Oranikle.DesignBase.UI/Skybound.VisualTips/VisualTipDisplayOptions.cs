using System;

namespace Skybound.VisualTips
{

    [System.Flags]
    public enum VisualTipDisplayOptions
    {
        Default = 0,
        PositionMask = 3,
        PositionBottom = 0,
        PositionRight = 1,
        PositionLeft = 2,
        PositionTop = 3,
        HideOnMouseLeave = 8,
        HideOnKeyDown = 16,
        HideOnKeyPress = 32,
        HideOnMouseDown = 64,
        HideOnLostFocus = 128,
        HideOnTextChanged = 256,
        ForwardEscapeKey = 512
    }

}

