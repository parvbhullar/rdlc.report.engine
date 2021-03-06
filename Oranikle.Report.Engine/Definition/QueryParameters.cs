/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Collection of query parameters.
	///</summary>
	[Serializable]
	public class QueryParameters : ReportLink
	{
        List<QueryParameter> _Items;			// list of QueryParameter
        bool _ContainsArray;                   // true if any of the parameters is an array reference

		public QueryParameters(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
            _ContainsArray = false;
			QueryParameter q;
            _Items = new List<QueryParameter>();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "QueryParameter":
						q = new QueryParameter(r, this, xNodeLoop);
						break;
					default:	
						q=null;		// don't know what this is
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown QueryParameters element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
				if (q != null)
					_Items.Add(q);
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "For QueryParameters at least one QueryParameter is required.");
			else
                _Items.TrimExcess();
		}
		
		override public void FinalPass()
		{
			foreach (QueryParameter q in _Items)
			{
				q.FinalPass();
                if (q.IsArray)
                    _ContainsArray = true;
			}
			return;
		}

        public List<QueryParameter> Items
		{
			get { return  _Items; }
		}
        public bool ContainsArray
        {
            get { return _ContainsArray; }
        }
	}
}
