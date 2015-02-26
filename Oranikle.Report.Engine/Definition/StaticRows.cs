/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Collection of matrix static rows.
	///</summary>
	[Serializable]
	public class StaticRows : ReportLink
	{
        List<StaticRow> _Items;			// list of StaticRow

		public StaticRows(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			StaticRow sr;
            _Items = new List<StaticRow>();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "StaticRow":
						sr = new StaticRow(r, this, xNodeLoop);
						break;
					default:	
						sr=null;		// don't know what this is
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown StaticRows element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
				if (sr != null)
					_Items.Add(sr);
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "For StaticRows at least one StaticRow is required.");
			else
                _Items.TrimExcess();
		}
		
		override public void FinalPass()
		{
			foreach (StaticRow r in _Items)
			{
				r.FinalPass();
			}
			return;
		}

        public List<StaticRow> Items
		{
			get { return  _Items; }
		}
	}
}
