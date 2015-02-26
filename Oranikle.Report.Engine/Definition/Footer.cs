/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;
using System.IO;

namespace Oranikle.Report.Engine
{
	///<summary>
	///  Definition of footer rows for a table or group.
	///</summary>
	[Serializable]
	public class Footer : ReportLink
	{
		TableRows _TableRows;	// The footer rows for the table or group
		bool _RepeatOnNewPage;	// Indicates this footer should be displayed on
								// each page that the table (or group) is displayed		
	
		public Footer(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
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
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Footer element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_TableRows == null)
				OwnerReport.rl.LogError(8, "TableRows element is required with a Footer but not specified.");
		}
		
		override public void FinalPass()
		{
			_TableRows.FinalPass();
			return;
		}

		public void Run(IPresent ip, Row row)
		{
			_TableRows.Run(ip, row);
			return;
		}

		public void RunPage(Pages pgs, Row row)
		{

			Page p = pgs.CurrentPage;
			if (p.YOffset + HeightOfRows(pgs, row) > pgs.BottomOfPage)
			{
				p = OwnerTable.RunPageNew(pgs, p);
				OwnerTable.RunPageHeader(pgs, row, false, null);
			}
			_TableRows.RunPage(pgs, row);

			return;
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

		public Table OwnerTable
		{
			get 
			{ 
				ReportLink rl = this.Parent;
				while (rl != null)
				{
					if (rl is Table)
						return rl as Table;
					rl = rl.Parent;
				}
				return null;
			}
		}
	}
}
