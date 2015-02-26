/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;
using System.IO;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Definition of the header rows for a table.
	///</summary>
	[Serializable]
	public class Header : ReportLink, ICacheData
	{
		TableRows _TableRows;	// The header rows for the table or group
		bool _RepeatOnNewPage;	// Indicates this header should be displayed on
								// each page that the table (or group) is displayed		

		public Header(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_TableRows=null;
			_RepeatOnNewPage=false;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "TableRows":
						_TableRows = new TableRows(r, this, xNodeLoop);
						break;
					case "RepeatOnNewPage":
						_RepeatOnNewPage = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:
						break;
				}
			}
			if (_TableRows == null)
				OwnerReport.rl.LogError(8, "Header requires the TableRows element.");
		}
		
		override public void FinalPass()
		{
			_TableRows.FinalPass();

			OwnerReport.DataCache.Add(this);
			return;
		}

		public void Run(IPresent ip, Row row)
		{
			_TableRows.Run(ip, row);
			return;
		}

		public void RunPage(Pages pgs, Row row)
		{
			WorkClass wc = this.GetValue(pgs.Report);

			if (wc.OutputRow == row && wc.OutputPage == pgs.CurrentPage)
				return;

			Page p = pgs.CurrentPage;

			float height = p.YOffset + HeightOfRows(pgs, row);
			if (height > pgs.BottomOfPage)
			{
				Table t = OwnerTable;
				p = t.RunPageNew(pgs, p);
				t.RunPageHeader(pgs, row, false, null);
				if (this.RepeatOnNewPage)
					return;		// should already be on the page
			}

			_TableRows.RunPage(pgs, row);
			wc.OutputRow = row;
			wc.OutputPage = pgs.CurrentPage;
			return;
		}

		public Table OwnerTable
		{
			get 
			{
				for (ReportLink rl = this.Parent; rl != null; rl = rl.Parent)
				{
					if (rl is Table)
						return rl as Table;
				}

				return null; 
			}
		}

		public TableRows TableRows
		{
			get { return  _TableRows; }
			set {  _TableRows = value; }
		}
 
		public float HeightOfRows(Pages pgs, Row r)
		{
			return _TableRows.HeightOfRows(pgs, r);
		}

		public bool RepeatOnNewPage
		{
			get { return  _RepeatOnNewPage; }
			set {  _RepeatOnNewPage = value; }
		}
		#region ICacheData Members

		public void ClearCache(Report rpt)
		{
			rpt.Cache.Remove(this, "wc");
		}

		#endregion

		private WorkClass GetValue(Report rpt)
		{
			WorkClass wc = rpt.Cache.Get(this, "wc") as WorkClass;
			if (wc == null)
			{
				wc = new WorkClass();
				rpt.Cache.Add(this, "wc", wc);
			}
			return wc;
		}

		private void SetValue(Report rpt, WorkClass w)
		{
			rpt.Cache.AddReplace(this, "wc", w);
		}

		class WorkClass
		{
			public Row OutputRow;		// the previous outputed row
			public Page OutputPage;	// the previous outputed row
			public WorkClass()
			{
				OutputRow = null;
				OutputPage = null;
			}
		}
	}
}
