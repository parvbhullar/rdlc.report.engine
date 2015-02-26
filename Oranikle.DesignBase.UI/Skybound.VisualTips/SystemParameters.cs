namespace Skybound.VisualTips
{

    internal sealed class SystemParameters
    {

        private const int SPI_GETTOOLTIPANIMATION = 4118;
        private const int SPI_GETTOOLTIPFADE = 4120;

        public static bool ToolTipAnimation
        {
            get
            {
                int i;

                Skybound.VisualTips.SystemParameters.SystemParametersInfo(4118, 0, out i, 0);
                return i != 0;
            }
        }

        public static bool ToolTipFade
        {
            get
            {
                int i;

                Skybound.VisualTips.SystemParameters.SystemParametersInfo(4120, 0, out i, 0);
                return i != 0;
            }
        }

        public static bool ToolTipShadow
        {
            get
            {
                return true;
            }
        }

        private SystemParameters()
        {
        }

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int SystemParametersInfo(int uAction, int uParam, out int lpvParam, int fuWinIni);

    } // class SystemParameters

}

