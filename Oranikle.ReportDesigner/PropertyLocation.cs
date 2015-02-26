/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;            // need this for the properties metadata
using System.Drawing.Design;
using System.Xml;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Oranikle.ReportDesigner
{
    /// <summary>
    /// PropertyExpr - 
    /// </summary>
    [TypeConverter(typeof(PropertyLocationConverter))]
    internal class PropertyLocation
    {
        PropertyReportItem _pri;
        string _left;
        string _top;

        internal PropertyLocation(PropertyReportItem pri, string x, string y)
        {
            _pri = pri;
            _left = x;
            _top = y;
        }
        [RefreshProperties(RefreshProperties.Repaint)]
        public string Left
        {
            get { return _left; }
            set 
            {
                DesignerUtility.ValidateSize(value, true, false);
                _left = value;
                _pri.SetValue("Left", value);
            }
        }
        [RefreshProperties(RefreshProperties.Repaint)]
        public string Top
        {
            get { return _top; }
            set 
            {
                DesignerUtility.ValidateSize(value, true, false);
                _top = value;
                _pri.SetValue("Top", value);
            }
        }
    }

    internal class PropertyLocationConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, 
            CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is PropertyLocation)
            {
                PropertyLocation pe = value as PropertyLocation;
                return string.Format("({0}, {1})", pe.Left, pe.Top);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

    }
}