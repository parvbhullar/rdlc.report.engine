/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Collection of matrix static columns.
	///</summary>
	[Serializable]
	public class StaticColumns : ReportLink
	{
        List<StaticColumn> _Items;			// list of StaticColumn

		public StaticColumns(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			StaticColumn sc;
            _Items = new List<StaticColumn>();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "StaticColumn":
						sc = new StaticColumn(r, this, xNodeLoop);
						break;
					default:	
						sc=null;		// don't know what this is
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown StaticColumns element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
				if (sc != null)
					_Items.Add(sc);
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "For StaticColumns at least one StaticColumn is required.");
			else
                _Items.TrimExcess();
		}
		
		override public void FinalPass()
		{
			foreach (StaticColumn sc in _Items)
			{
				sc.FinalPass();
			}
			return;
		}

        public List<StaticColumn> Items
		{
			get { return  _Items; }
		}
	}
}
