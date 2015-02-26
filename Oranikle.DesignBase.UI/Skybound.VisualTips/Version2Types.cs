using System;
using System.Reflection;

namespace Skybound.VisualTips
{

    internal sealed class Version2Types
    {

        public static readonly System.Type ToolStrip;
        public static readonly System.Type ToolStripItem;

        private Version2Types()
        {
        }

        static Version2Types()
        {
            if (System.Environment.Version.Major >= 2)
            {
                Skybound.VisualTips.Version2Types.ToolStrip = System.Reflection.Assembly.LoadWithPartialName("System.Windows.Forms").GetType("System.Windows.Forms.ToolStrip", false);
                Skybound.VisualTips.Version2Types.ToolStripItem = System.Reflection.Assembly.LoadWithPartialName("System.Windows.Forms").GetType("System.Windows.Forms.ToolStripItem", false);
            }
        }

    } // class Version2Types

}

