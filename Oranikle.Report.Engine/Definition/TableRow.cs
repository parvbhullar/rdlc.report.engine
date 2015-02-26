/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// TableRow represents a Row in a table.  This can be part of a header, footer, or detail definition.
	///</summary>
	[Serializable]
	public class TableRow : ReportLink
	{
		TableCells _TableCells;	// Contents of the row. One cell per column
		RSize _Height;				// Height of the row
		Visibility _Visibility;		// Indicates if the row should be hidden		
		bool _CanGrow;			// indicates that row height can increase in size
		List<Textbox> _GrowList;	// list of TextBox's that need to be checked for growth

		public TableRow(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_TableCells=null;
			_Height=null;
			_Visibility=null;
			_CanGrow = false;
			_GrowList = null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "TableCells":
						_TableCells = new TableCells(r, this, xNodeLoop);
						break;
					case "Height":
						_Height = new RSize(r, xNodeLoop);
						break;
					case "Visibility":
						_Visibility = new Visibility(r, this, xNodeLoop);
						break;
					default:
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown TableRow element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_TableCells == null)
				OwnerReport.rl.LogError(8, "TableRow requires the TableCells element.");
			if (_Height == null)
				OwnerReport.rl.LogError(8, "TableRow requires the Height element.");
		}
		
		override public void FinalPass()
		{
			_TableCells.FinalPass();
			if (_Visibility != null)
				_Visibility.FinalPass();

			foreach (TableCell tc in _TableCells.Items)
			{
				ReportItem ri = tc.ReportItems.Items[0] as ReportItem;
				if (!(ri is Textbox))
					continue;
				Textbox tb = ri as Textbox;
				if (tb.CanGrow)
				{
					if (this._GrowList == null)
						_GrowList = new List<Textbox>();
					_GrowList.Add(tb);
					_CanGrow = true;
				}
			}

			if (_CanGrow)				// shrink down the resulting list
                _GrowList.TrimExcess();

			return;
		}

		public void Run(IPresent ip, Row row)
		{
			if (this.Visibility != null && Visibility.IsHidden(ip.Report(), row))
				return;

			ip.TableRowStart(this, row);
			_TableCells.Run(ip, row);
			ip.TableRowEnd(this, row);
			return ;
		}
 
		public void RunPage(Pages pgs, Row row)
		{
			if (this.Visibility != null && Visibility.IsHidden(pgs.Report, row))
				return;

			_TableCells.RunPage(pgs, row);

			WorkClass wc = GetWC(pgs.Report);
			pgs.CurrentPage.YOffset += wc.CalcHeight;
			return ;
		}

		public TableCells TableCells
		{
			get { return  _TableCells; }
			set {  _TableCells = value; }
		}

		public RSize Height
		{
			get { return  _Height; }
			set {  _Height = value; }
		}
        public float HeightOfRow(Pages pgs, Row r)
        {
            return HeightOfRow(pgs.Report, pgs.G, r);
        }
		public float HeightOfRow(Report rpt, System.Drawing.Graphics g, Row r)
		{
			WorkClass wc = GetWC(rpt);
			if (this.Visibility != null && Visibility.IsHidden(rpt, r))
			{
				wc.CalcHeight = 0;
				return 0;
			}

			float defnHeight = _Height.Points;
			if (!_CanGrow)
			{
				wc.CalcHeight = defnHeight;
				return defnHeight;
			}

            TableColumns tcs= this.Table.TableColumns;
			float height=0;
			foreach (Textbox tb in this._GrowList)
			{
                int ci = tb.TC.ColIndex;
                if (tcs[ci].IsHidden(rpt, r))    // if column is hidden don't use in calculation
                    continue;
				height = Math.Max(height, tb.RunTextCalcHeight(rpt, g, r));
			}
			wc.CalcHeight = Math.Max(height, defnHeight);
			return wc.CalcHeight;
		}

		public float HeightCalc(Report rpt)
		{
			WorkClass wc = GetWC(rpt);
			return wc.CalcHeight;
		}

        private Table Table
        {
            get
            {
                ReportLink p= this.Parent;
                while (p != null)
                {
                    if (p is Table)
                        return p as Table;
                    p = p.Parent;
                }
                throw new Exception("Internal error: TableRow not related to a Table");
            }
        }

            public Visibility Visibility
		{
			get { return  _Visibility; }
			set {  _Visibility = value; }
		}

		public bool CanGrow
		{
			get { return _CanGrow; }
		}

		public List<Textbox> GrowList
		{
			get { return _GrowList; }
		}

		private WorkClass GetWC(Report rpt)
		{
			WorkClass wc = rpt.Cache.Get(this, "wc") as WorkClass;
			if (wc == null)
			{
				wc = new WorkClass(this);
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
			public float CalcHeight;		// dynamic when CanGrow true
			public WorkClass(TableRow tr)
			{
				CalcHeight = tr.Height.Points;
			}
		}
	}
}
