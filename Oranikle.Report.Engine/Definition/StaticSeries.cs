/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Collection of chart static series.
	///</summary>
	[Serializable]
	public class StaticSeries : ReportLink
	{
        List<StaticMember> _Items;			// list of StaticMember

		public StaticSeries(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			StaticMember sm;
            _Items = new List<StaticMember>();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "StaticMember":
						sm = new StaticMember(r, this, xNodeLoop);
						break;
					default:	
						sm=null;		// don't know what this is
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown StaticSeries element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
				if (sm != null)
					_Items.Add(sm);
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "For StaticSeries at least one StaticMember is required.");
			else
                _Items.TrimExcess();
		}
		
		override public void FinalPass()
		{
			foreach (StaticMember sm in _Items)
			{
				sm.FinalPass();
			}
			return;
		}

        public List<StaticMember> Items
		{
			get { return  _Items; }
		}
	
	}
}
