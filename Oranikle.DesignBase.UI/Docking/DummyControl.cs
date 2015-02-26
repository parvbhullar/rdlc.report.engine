using System;
using System.Windows.Forms;

namespace Oranikle.DesignBase.UI.Docking
{
	internal class DummyControl : Control
	{
		public DummyControl()
		{
			SetStyle(ControlStyles.Selectable, false);
		}
	}
}
