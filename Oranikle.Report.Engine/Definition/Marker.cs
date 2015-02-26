/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Represents a marker on a chart.
	///</summary>
	[Serializable]
	public class Marker : ReportLink
	{
		MarkerTypeEnum _Type;	// Defines the marker type for values. Default: none
		RSize _Size;		// Represents the height and width of the
							//  plotting area of marker(s).
		Style _Style;		// Defines the border and background style
							//  properties for the marker(s).		
	
		public Marker(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Type=MarkerTypeEnum.None;
			_Size=null;
			_Style=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Type":
						_Type = MarkerType.GetStyle(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "Size":
						_Size = new RSize(r, xNodeLoop);
						break;
					case "Style":
						_Style = new Style(r, this, xNodeLoop);
						break;
					default:
						break;
				}
			}
		}
		
		override public void FinalPass()
		{
			if (_Style != null)
				_Style.FinalPass();
			return;
		}

		public MarkerTypeEnum Type
		{
			get { return  _Type; }
			set {  _Type = value; }
		}

		public RSize Size
		{
			get { return  _Size; }
			set {  _Size = value; }
		}

		public Style Style
		{
			get { return  _Style; }
			set {  _Style = value; }
		}
	}

}
