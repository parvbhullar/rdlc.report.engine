/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Represents the report item for a line.
	///</summary>
	[Serializable]
	public class Line : ReportItem
	{
		// Line has no additional elements/attributes beyond ReportItem
		public Line(ReportDefn r, ReportLink p, XmlNode xNode) : base(r,p,xNode)
		{
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				// nothing beyond reportitem for now
				if (!ReportItemElement(xNodeLoop))	// try at ReportItem level
				{					
					// don't know this element - log it
					OwnerReport.rl.LogError(4, "Unknown Line element " + xNodeLoop.Name + " ignored.");
				}
			}
		}
		override public void Run(IPresent ip, Row row)
		{
			ip.Line(this, row);
		}

		override public void RunPage(Pages pgs, Row row)
		{
			Report r = pgs.Report;
            bool bHidden = IsHidden(r, row);

			SetPagePositionBegin(pgs);
			PageLine pl = new PageLine();
            SetPagePositionAndStyle(r, pl, row);
            if (!bHidden)
			    pgs.CurrentPage.AddObject(pl);
			SetPagePositionEnd(pgs, pl.Y);
		}

		public float GetX2(Report rpt)
		{
			float x2=GetOffsetCalc(rpt)+LeftCalc(rpt);
			if (Width != null)
				x2 += Width.Points;
			return x2;
		}		

		public int iX2
		{
			get 
			{
				int x2=0;
				if (Left != null)
					x2 = Left.Size;
				if (Width != null)
					x2 += Width.Size;
				return x2;
			}
		}

        public int iY2
        {
            get
            {
                int y2 = 0;
                if (Top != null)
                    y2 = Top.Size;
                if (Height != null)
                    y2 += Height.Size;
                return y2;
            }
        }

        public float Y2
        {
            get
            {
                float y2 = 0;
                if (Top != null)
                    y2 = Top.Points;
                if (Height != null)
                    y2 += Height.Points;
                return y2;
            }
        }
    }
}
