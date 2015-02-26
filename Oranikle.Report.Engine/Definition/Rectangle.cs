/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Represent the rectangle report item.
	///</summary>
	[Serializable]
	public class Rectangle : ReportItem
	{
		ReportItems _ReportItems;	// Report items contained within the bounds of the rectangle.
		bool _PageBreakAtStart;		// Indicates the report should page break at the start of the rectangle.
		bool _PageBreakAtEnd;		// Indicates the report should page break at the end of the rectangle.		

		// constructor that doesn't process syntax
		public Rectangle(ReportDefn r, ReportLink p, XmlNode xNode, bool bNoLoop):base(r,p,xNode)
		{
			_ReportItems=null;
			_PageBreakAtStart=false;
			_PageBreakAtEnd=false;
		}

		public Rectangle(ReportDefn r, ReportLink p, XmlNode xNode):base(r,p,xNode)
		{
			_ReportItems=null;
			_PageBreakAtStart=false;
			_PageBreakAtEnd=false;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "ReportItems":
						_ReportItems = new ReportItems(r, this, xNodeLoop);
						break;
					case "PageBreakAtStart":
						_PageBreakAtStart = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					case "PageBreakAtEnd":
						_PageBreakAtEnd = XmlUtil.Boolean(xNodeLoop.InnerText, OwnerReport.rl);
						break;
					default:	
						if (ReportItemElement(xNodeLoop))	// try at ReportItem level
							break;
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Rectangle element " + xNodeLoop.Name + " ignored.");
						break;
				}
			}
		}
 
		override public void FinalPass()
		{
			base.FinalPass();

			if (_ReportItems != null)
				_ReportItems.FinalPass();

			return;
		}
 
		override public void Run(IPresent ip, Row row)
		{
			base.Run(ip, row);

			if (_ReportItems == null)
				return;

			if (ip.RectangleStart(this, row))
			{
				_ReportItems.Run(ip, row);
				ip.RectangleEnd(this, row);
			}
		}

		override public void RunPage(Pages pgs, Row row)
		{
			Report r = pgs.Report;
            bool bHidden = IsHidden(r, row);

			SetPagePositionBegin(pgs);

            // Handle page breaking at start
            if (this.PageBreakAtStart && !IsTableOrMatrixCell(r) && !pgs.CurrentPage.IsEmpty() && !bHidden)
            {	// force page break at beginning of dataregion
                pgs.NextOrNew();
                pgs.CurrentPage.YOffset = OwnerReport.TopOfPage;
            }

			PageRectangle pr = new PageRectangle();
			SetPagePositionAndStyle(r, pr, row);
			if (pr.SI.BackgroundImage != null)
				pr.SI.BackgroundImage.H = pr.H;		//   and in the background image

            if (!bHidden)
            {
                Page p = pgs.CurrentPage;
                p.AddObject(pr);

                if (_ReportItems != null)
                {
                    float saveY = p.YOffset;
       //             p.YOffset += (Top == null ? 0 : this.Top.Points);
                    p.YOffset = pr.Y;       // top of rectangle is base for contained report items
                    _ReportItems.RunPage(pgs, row, GetOffsetCalc(pgs.Report) + LeftCalc(r));
                    p.YOffset = saveY;
                }

                // Handle page breaking at end
                if (this.PageBreakAtEnd && !IsTableOrMatrixCell(r) && !pgs.CurrentPage.IsEmpty())
                {	// force page break at beginning of dataregion
                    pgs.NextOrNew();
                    pgs.CurrentPage.YOffset = OwnerReport.TopOfPage;
                }
            }
//			SetPagePositionEnd(pgs, pgs.CurrentPage.YOffset);
            SetPagePositionEnd(pgs, pr.Y + pr.H);
        }

        public override void RemoveWC(Report rpt)
        {
            base.RemoveWC(rpt);

            if (this._ReportItems == null)
                return;

            foreach (ReportItem ri in this._ReportItems.Items)
            {
                ri.RemoveWC(rpt);
            }
        }

		public ReportItems ReportItems
		{
			get { return  _ReportItems; }
			set {  _ReportItems = value; }
		}

		public bool PageBreakAtStart
		{
			get { return  _PageBreakAtStart; }
			set {  _PageBreakAtStart = value; }
		}

		public bool PageBreakAtEnd
		{
			get { return  _PageBreakAtEnd; }
			set {  _PageBreakAtEnd = value; }
		}
	}
}
