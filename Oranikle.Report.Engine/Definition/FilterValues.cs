/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	///  Collection of Filter values to compare against in a filter.  Cardinality depends 
	///  filter operater.
	///</summary>
	[Serializable]
	public class FilterValues : ReportLink
	{
        List<FilterValue> _Items;			// list of FilterValue

		public FilterValues(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			FilterValue f;
            _Items = new List<FilterValue>();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "FilterValue":
						f = new FilterValue(r, this, xNodeLoop);
						break;
					default:	
						f=null;		// don't know what this is
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown FilterValues element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
				if (f != null)
					_Items.Add(f);
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "For FilterValues at least one FilterValue is required.");
			else
                _Items.TrimExcess();
		}
		
		override public void FinalPass()
		{
			foreach (FilterValue f in _Items)
			{
				f.FinalPass();
			}
			return;
		}

		public List<FilterValue> Items
		{
			get { return  _Items; }
		}
	}
}
