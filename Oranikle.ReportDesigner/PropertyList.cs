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
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Oranikle.Report.Engine;

namespace Oranikle.ReportDesigner
{
    /// <summary>
    /// PropertyList - The List Properties
    /// </summary>
    [TypeConverter(typeof(PropertyListConverter))]
    internal class PropertyList : PropertyDataRegion
    {
        internal PropertyList(DesignXmlDraw d, DesignCtl dc, List<XmlNode> ris)
            : base(d, dc, ris)
        {
        }
        [CategoryAttribute("List"),
            DescriptionAttribute("Grouping data allows each repeated list region to represent a summarization of the rows in the group.")]
        public PropertyGrouping Grouping
        {
            get
            {
                return new PropertyGrouping(this);
            }
        }
        [CategoryAttribute("List"),
            DescriptionAttribute("Sorting controls the order of the repeated list regions.")]
        public PropertySorting Sorting
        {
            get
            {
                return new PropertySorting(this);
            }
        }
        #region XML
        [CategoryAttribute("XML"),
   DescriptionAttribute("The name to use for the data element for each instance of this list when exporting to XML.")]
        public string DataInstanceName
        {
            get
            {
                return GetValue("DataInstanceName", "");
            }
            set
            {
                SetValue("DataInstanceName", value);
            }
        }
        [CategoryAttribute("XML"),
   DescriptionAttribute("Determines whether list instances appear in the XML.")]
        public DataInstanceElementOutputEnum DataInstanceElementOutput
        {
            get
            {
                string v = GetValue("DataInstanceElementOutput", "Output");
                return Oranikle.Report.Engine.DataInstanceElementOutput.GetStyle(v);
            }
            set
            {
                SetValue("DataInstanceElementOutput", value.ToString());
            }
        }
        #endregion
    }
    internal class PropertyListConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
            CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is PropertyList)
            {
                PropertyList pe = value as PropertyList;
                return pe.Name;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

    }

}
