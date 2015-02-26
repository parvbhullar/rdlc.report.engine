using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.DesignBase.UI
{
    public class TooltipBase : Skybound.VisualTips.VisualTipProvider
    {

        private const int DEFAULT_INITIAL_DELAY = 400;
        private const int DEFAULT_RESHOW_DELAY = 400;

        [System.ComponentModel.DefaultValue(400)]
        public new int InitialDelay
        {
            get
            {
                return base.InitialDelay;
            }
            set
            {
                base.InitialDelay = value;
            }
        }

        [System.ComponentModel.DefaultValue(400)]
        public new int ReshowDelay
        {
            get
            {
                return base.ReshowDelay;
            }
            set
            {
                base.ReshowDelay = value;
            }
        }

        public TooltipBase(System.ComponentModel.IContainer container)
            : base(container)
        {
            InitialDelay = 400;
            ReshowDelay = 400;
        }

        protected override void OnTipPopup(Skybound.VisualTips.VisualTipEventArgs e)
        {
            try
            {
                if ((e.Tip != null) && System.String.IsNullOrEmpty(e.Tip.Title) && (e.Tip.TitleImage == null) && System.String.IsNullOrEmpty(e.Tip.Text) && (e.Tip.Image == null) && System.String.IsNullOrEmpty(e.Tip.FooterText) && (e.Tip.FooterImage == null))
                {
                    e.Cancel = true;
                    return;
                }
                base.OnTipPopup(e);
            }
            catch (System.Exception)
            {
            }
        }

    }
}
