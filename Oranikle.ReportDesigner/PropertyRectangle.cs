/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;            // need this for the properties metadata
using System.Xml;
using System.Text.RegularExpressions;
using Oranikle.Report.Engine;

namespace Oranikle.ReportDesigner
{
    /// <summary>
    /// PropertyRectangle - The System.Drawing.Rectangle specific Properties
    /// </summary>
    
    internal class PropertyRectangle : PropertyReportItem
    {
        internal PropertyRectangle(DesignXmlDraw d, DesignCtl dc, List<XmlNode> ris) : base(d, dc, ris)
        {
        }
        [CategoryAttribute("System.Drawing.Rectangle"),
           DescriptionAttribute("Determines if report will start a new page at the top of the rectangle.")]
        public bool PageBreakAtStart
        {
            get { return this.Draw.GetElementValue(this.Node, "PageBreakAtStart", "false").ToLower() == "true" ? true : false; }
            set
            {
                this.SetValue("PageBreakAtStart", value ? "true" : "false");
            }
        }
        [CategoryAttribute("System.Drawing.Rectangle"),
           DescriptionAttribute("Determines if report will start a new page after the bottom of the rectangle.")]
        public bool PageBreakAtEnd
        {
            get { return this.Draw.GetElementValue(this.Node, "PageBreakAtEnd", "false").ToLower() == "true" ? true : false; }
            set
            {
                this.SetValue("PageBreakAtEnd", value ? "true" : "false");
            }
        }

    }
}
