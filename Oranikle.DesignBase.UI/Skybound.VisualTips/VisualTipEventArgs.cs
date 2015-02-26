using System;

namespace Skybound.VisualTips
{

    public class VisualTipEventArgs : System.EventArgs
    {

        private bool _Cancel;
        private object _Instance;
        private Skybound.VisualTips.VisualTip _Tip;

        public bool Cancel
        {
            get
            {
                return _Cancel;
            }
            set
            {
                _Cancel = value;
            }
        }

        public object Instance
        {
            get
            {
                return _Instance;
            }
        }

        public Skybound.VisualTips.VisualTip Tip
        {
            get
            {
                return _Tip;
            }
        }

        public VisualTipEventArgs(Skybound.VisualTips.VisualTip tip, object instance)
        {
            _Tip = tip;
            _Instance = instance;
        }

    } // class VisualTipEventArgs

}

