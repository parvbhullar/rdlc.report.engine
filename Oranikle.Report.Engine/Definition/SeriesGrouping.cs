/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Chart Series grouping (both dynamic and static).
	///</summary>
	[Serializable]
	public class SeriesGrouping : ReportLink
	{
		DynamicSeries _DynamicSeries;	// Dynamic Series headings for this grouping
		StaticSeries _StaticSeries;		// Static Series headings for this grouping	
		Style _Style;					// border and background properties for series legend itmes and data points
										//   when dynamic exprs are evaluated per group instance
	
		public SeriesGrouping(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_DynamicSeries=null;
			_StaticSeries=null;
			_Style=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "DynamicSeries":
						_DynamicSeries = new DynamicSeries(r, this, xNodeLoop);
						break;
					case "StaticSeries":
						_StaticSeries = new StaticSeries(r, this, xNodeLoop);
						break;
					case "Style":
						_Style = new Style(OwnerReport, this, xNodeLoop);
						OwnerReport.rl.LogError(4, "Style element in SeriesGrouping is currently ignored."); // TODO
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown SeriesGrouping element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
		}
		
		override public void FinalPass()
		{
			if (_DynamicSeries != null)
				_DynamicSeries.FinalPass();
			if (_StaticSeries != null)
				_StaticSeries.FinalPass();
			if (_Style != null)
				_Style.FinalPass();

			return;
		}

		public DynamicSeries DynamicSeries
		{
			get { return  _DynamicSeries; }
			set {  _DynamicSeries = value; }
		}

		public StaticSeries StaticSeries
		{
			get { return  _StaticSeries; }
			set {  _StaticSeries = value; }
		}

		public Style Style
		{
			get { return  _Style; }
			set {  _Style = value; }
		}

	}
}
