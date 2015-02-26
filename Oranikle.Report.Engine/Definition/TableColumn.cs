/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;
using System.IO;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// TableColumn definition and processing.
	///</summary>
	[Serializable]
	public class TableColumn : ReportLink
	{
		RSize _Width;		// Width of the column
		Visibility _Visibility;	// Indicates if the column should be hidden	
		bool _FixedHeader=false;	// Header of this column should be display even when scrolled
	
		public TableColumn(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Width=null;
			_Visibility=null;

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
					case "Visibility":
						_Visibility = new Visibility(r, this, xNodeLoop);
						break;
					case "FixedHeader":
						_FixedHeader = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown TableColumn element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_Width == null)
				OwnerReport.rl.LogError(8, "TableColumn requires the Width element.");
		}
		
		override public void FinalPass()
		{
			if (_Visibility != null)
				_Visibility.FinalPass();
			return;
		}

		public void Run(IPresent ip, Row row)
		{
		}

		public RSize Width
		{
			get { return  _Width; }
			set {  _Width = value; }
		}

		public float GetXPosition(Report rpt)
		{
			WorkClass wc = GetWC(rpt);
			return wc.XPosition;
		}

		public void SetXPosition(Report rpt, float xp)
		{
			WorkClass wc = GetWC(rpt);
			wc.XPosition = xp;
		}

		public Visibility Visibility
		{
			get { return  _Visibility; }
			set {  _Visibility = value; }
		}

		public bool IsHidden(Report rpt, Row r)
		{
			if (_Visibility == null)
				return false;
			return _Visibility.IsHidden(rpt, r);
		}

		private WorkClass GetWC(Report rpt)
		{
			if (rpt == null)	
				return new WorkClass();

			WorkClass wc = rpt.Cache.Get(this, "wc") as WorkClass;
			if (wc == null)
			{
				wc = new WorkClass();
				rpt.Cache.Add(this, "wc", wc);
			}
			return wc;
		}

		private void RemoveWC(Report rpt)
		{
			rpt.Cache.Remove(this, "wc");
		}

		class WorkClass
		{
			public float XPosition;	// Set at runtime by Page processing; potentially dynamic at runtime
			//  since visibility is an expression
			public WorkClass()
			{
				XPosition=0;
			}
		}
	}
}
