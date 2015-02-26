/* ====================================================================

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Oranikle.Report.Engine
{
    public class DrawBase
    {
        protected const float SCALEFACTOR = 72f / 96f;
        protected System.Collections.Hashtable ObjectTable;
        protected Single X;
        protected Single Y;
        protected Single Width;
        protected Single Height;
        protected List<PageItem> items;

        protected static BorderStyleEnum getLineStyle(Pen p)
        {
            BorderStyleEnum ls = BorderStyleEnum.Solid;
            switch (p.DashStyle)
            {               
                case DashStyle.Dash:
                    ls = BorderStyleEnum.Dashed;
                    break;
                case DashStyle.DashDot:
                    ls = BorderStyleEnum.Dashed;
                    break;
                case DashStyle.DashDotDot:
                    ls = BorderStyleEnum.Dashed;
                    break;
                case DashStyle.Dot: 
                    ls = BorderStyleEnum.Dotted;
                    break;
                case DashStyle.Solid:
                    ls = BorderStyleEnum.Solid;
                    break;
                case DashStyle.Custom:
                    ls = BorderStyleEnum.Solid;
                    break;
                default:                   
                    break;
            }  
            return ls;
        }

       
    }

    
}
