/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Oranikle.Report.Engine;

namespace Oranikle.Report.Engine
{
	/// <summary>
	/// ICustomReportItem defines the protocol for implementing a CustomReportItem
	/// </summary>

	public interface ICustomReportItem : IDisposable    
	{
        bool IsDataRegion();                            // Does CustomReportItem require DataRegions
        void DrawImage(System.Drawing.Bitmap bm);       // Draw the image in the passed bitmap; do SetParameters first
        void DrawDesignerImage(System.Drawing.Bitmap bm);   // Design time: Draw the designer image in the passed bitmap;
        void SetProperties(IDictionary<string, object> parameters); // Set the runtime properties
        object GetPropertiesInstance(XmlNode node);     // Design time: return class representing properties
        void SetPropertiesInstance(XmlNode node, object inst);  // Design time: given class representing properties set the XML custom properties
        string GetCustomReportItemXml();                // Design time: return string with <CustomReportItem> ... </CustomReportItem> syntax for the insert
    }

}
