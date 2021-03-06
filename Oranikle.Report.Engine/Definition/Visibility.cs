/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Visibility definition and processing.
	///</summary>
	[Serializable]
	public class Visibility : ReportLink
	{
		Expression _Hidden;		// (Boolean) Indicates if the item should be initially hidden.
		string _ToggleItem;		// The name of the textbox used to
					// hide/unhide this report item. Clicking on
					//an instance of the ToggleItem will toggle
					//the hidden state of every corresponding
					//instance of this item. If the Toggle item
					//becomes hidden, this item should become
					//hidden.
					//Must be a textbox in the same grouping
					//scope as this item or in any containing (ancestor) grouping scope
					//If omitted, no item will toggle the hidden
					//state of this item.
					//Not allowed on and cannot refer to report
					//items contained in a page header or
					//footer.
					//Cannot refer to a report item contained
					//within the current report item unless
					//current grouping scope has a Parent.		
	
		public Visibility(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Hidden=null;
			_ToggleItem=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Hidden":
						_Hidden = new Expression(r, this, xNodeLoop, ExpressionType.Boolean);
						break;
					case "ToggleItem":
						_ToggleItem = xNodeLoop.InnerText;
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Visibility element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
		}

		// Handle parsing of function in final pass
		override public void FinalPass()
		{
			if (_Hidden != null)
				_Hidden.FinalPass();
			return;
		}

		public Expression Hidden
		{
			get { return  _Hidden; }
			set {  _Hidden = value; }
		}

		public bool IsHidden(Report rpt, Row r)
		{
			if (_Hidden == null)
				return false;

			return _Hidden.EvaluateBoolean(rpt, r);
		}

		public string ToggleItem
		{
			get { return  _ToggleItem; }
			set {  _ToggleItem = value; }
		}
	}
}
