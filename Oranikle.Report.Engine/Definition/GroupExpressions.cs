/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Collection of group expressions.
	///</summary>
	[Serializable]
	public class GroupExpressions : ReportLink
	{
        List<GroupExpression> _Items;			// list of GroupExpression

		public GroupExpressions(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			GroupExpression g;
            _Items = new List<GroupExpression>();
			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "GroupExpression":
						g = new GroupExpression(r, this, xNodeLoop);
						break;
					default:	
						g=null;		// don't know what this is
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown GroupExpressions element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
				if (g != null)
					_Items.Add(g);
			}
			if (_Items.Count == 0)
				OwnerReport.rl.LogError(8, "GroupExpressions require at least one GroupExpression be defined.");
			else
                _Items.TrimExcess();
		}
		
		override public void FinalPass()
		{
			foreach (GroupExpression g in _Items)
			{
				g.FinalPass();
			}
			return;
		}

        public List<GroupExpression> Items
		{
			get { return  _Items; }
		}
	}
}
