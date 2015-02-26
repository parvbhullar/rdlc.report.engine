/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// The collection of Drillthrough parameters.
	///</summary>
	[Serializable]
	public class DrillthroughParameters : ReportLink
	{
        List<DrillthroughParameter> _Items;			// list of report items

		public DrillthroughParameters(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			DrillthroughParameter d;
            _Items = new List<DrillthroughParameter>();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Parameter":
						d = new DrillthroughParameter(r, this, xNodeLoop);
						break;
					default:	
						d=null;		// don't know what this is
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Parameters element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
				if (d != null)
					_Items.Add(d);
			}
			if (_Items.Count > 0)
                _Items.TrimExcess();
		}
		
		override public void FinalPass()
		{
			foreach (DrillthroughParameter r in _Items)
			{
				r.FinalPass();
			}
			return;
		}

        public List<DrillthroughParameter> Items
		{
			get { return  _Items; }
		}
	}
}
