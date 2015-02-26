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
    /// PropertyTextbox - The Textbox Properties
    /// </summary>
    [DefaultPropertyAttribute("Value")]
    internal class PropertyTextbox : PropertyReportItem
    {
        internal PropertyTextbox(DesignXmlDraw d, DesignCtl dc, List<XmlNode> ris) : base(d, dc, ris)
        {
        }
        [CategoryAttribute("Textbox"),
           DescriptionAttribute("The value of the textbox.")]
        public PropertyExpr Value
        {
            get { return new PropertyExpr(this.GetValue("Value", "")); }
            set
            {
                this.SetValue("Value", value.Expression);
            }
        }
        [CategoryAttribute("Style"),
                   DescriptionAttribute("Font, color, alignment, ... of text.")]
        public PropertyAppearance Appearance
        {
            get { return new PropertyAppearance(this); }
        }
        [CategoryAttribute("Textbox"),
           DescriptionAttribute("CanGrow indicates the height of the Textbox can increase depending on its contents.")]
        public bool CanGrow
        {
            get { return this.GetValue("CanGrow", "false").ToLower() == "true"; }
            set
            {
                this.SetValue("CanGrow", value? "true": "false");
            }
        }
        [CategoryAttribute("Textbox"),
           DescriptionAttribute("CanShrink indicates the height of the Textbox can decrease depending on its contents.")]
        public bool CanShrink
        {
            get { return this.GetValue("CanShrink", "false").ToLower() == "true"; }
            set
            {
                this.SetValue("CanShrink", value ? "true" : "false");
            }
        }
        [CategoryAttribute("Textbox"),
            TypeConverter(typeof(HideDuplicatesConverter)),
            DescriptionAttribute("To HideDuplicate values provide the scope (dataset or group) over which you want to hide the Textbox.")]
        public string HideDuplicates
        {
            get { return this.GetValue("HideDuplicates", ""); }
            set
            {
                this.SetValue("HideDuplicates", value);
            }
        }
        [CategoryAttribute("XML"),
   DescriptionAttribute("Specifies whether Textbox renders as an Attribute or an Element.")]
        public DataElementStyleEnum DataElementStyle
        {
            get
            {
                string v = GetValue("DataElementStyle", "Auto");
                return Oranikle.Report.Engine.DataElementStyle.GetStyle(v);
            }
            set
            {
                SetValue("DataElementStyle", value.ToString());
            }
        }

        internal class HideDuplicatesConverter : StringConverter
        {

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                // returning false here means the property will
                // have a drop down and a value that can be manually
                // entered.      
                return false;
            }

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                if (context == null)
                    return base.GetStandardValues(context);

                PropertyTextbox pt = context.Instance as PropertyTextbox;
                if (pt == null)
                    return base.GetStandardValues(context);

                // Populate with the list of datasets and group names
                ArrayList ar = new ArrayList();
                ar.Add("");         // add an empty string to the collection
                object[] dsn = pt.Draw.DataSetNames;
                if (dsn != null)
                    ar.AddRange(dsn);
                object[] grps = pt.Draw.GroupingNames;
                if (grps != null)
                    ar.AddRange(grps);

                StandardValuesCollection svc = new StandardValuesCollection(ar);
                
                return svc;
            }
        }

    }
}
