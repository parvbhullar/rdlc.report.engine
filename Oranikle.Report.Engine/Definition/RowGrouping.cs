/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Matrix row grouping definition.
	///</summary>
	[Serializable]
	public class RowGrouping : ReportLink
	{
		RSize _Width;	// Width of the row header
		DynamicRows _DynamicRows;	// Dynamic row headings for this grouping
		StaticRows _StaticRows;	// Static row headings for this grouping		
	
		public RowGrouping(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Width=null;
			_DynamicRows=null;
			_StaticRows=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Width":
						_Width = new RSize(r, xNodeLoop);
						break;
					case "DynamicRows":
						_DynamicRows = new DynamicRows(r, this, xNodeLoop);
						break;
					case "StaticRows":
						_StaticRows = new StaticRows(r, this, xNodeLoop);
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown RowGrouping element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_Width == null)
				OwnerReport.rl.LogError(8, "RowGrouping requires the Width element.");
		}
		
		override public void FinalPass()
		{
			if (_DynamicRows != null)
				_DynamicRows.FinalPass();
			if (_StaticRows != null)
				_StaticRows.FinalPass();
			return;
		}

		public RSize Width
		{
			get { return  _Width; }
			set {  _Width = value; }
		}

		public DynamicRows DynamicRows
		{
			get { return  _DynamicRows; }
			set {  _DynamicRows = value; }
		}

		public StaticRows StaticRows
		{
			get { return  _StaticRows; }
			set {  _StaticRows = value; }
		}
	}
}
