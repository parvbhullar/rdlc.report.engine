/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Action definition and processing.
	///</summary>
	[Serializable]
	public class Action : ReportLink
	{
		Expression _Hyperlink;	// (URL)An expression that evaluates to the URL of the hyperlink
		Drillthrough _Drillthrough;	// The drillthrough report that should be executed
									// by clicking on the hyperlink
		Expression _BookmarkLink;	// (string)
								//An expression that evaluates to the ID of a
								//bookmark within the report to go to when this
								//report item is clicked on.
								//(If no bookmark with this ID is found, the link
								//will not be included in the report. If the
								//bookmark is hidden, the link will go to the start
								//of the page the bookmark is on. If multiple
								//bookmarks with this ID are found, the link will
								//go to the first one)		
		// Constructor
		public Action(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Hyperlink = null;
			_Drillthrough = null;	
			_BookmarkLink = null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Hyperlink":
						_Hyperlink = new Expression(r, this, xNodeLoop, ExpressionType.URL);
						break;
					case "Drillthrough":
						_Drillthrough = new Drillthrough(r, this, xNodeLoop);
						break;
					case "BookmarkLink":
						_BookmarkLink = new Expression(r, this, xNodeLoop, ExpressionType.String);
						break;
					default:
						break;
				}
			}
		}

		// Handle parsing of function in final pass
		override public void FinalPass()
		{
			if (_Hyperlink != null) 
				_Hyperlink.FinalPass();
			if (_Drillthrough != null) 
				_Drillthrough.FinalPass();
			if (_BookmarkLink != null) 
				_BookmarkLink.FinalPass();
			return;
		}

		public Expression Hyperlink
		{
			get { return _Hyperlink; }
			set { _Hyperlink = value; }
		}

		public String HyperLinkValue(Report rpt, Row r)
		{
			if (_Hyperlink == null)
				return null;

			return _Hyperlink.EvaluateString(rpt, r);
		}

		public Drillthrough Drill
		{
			get { return _Drillthrough; }
			set { _Drillthrough = value; }
		}

		public Expression BookmarkLink
		{
			get { return _BookmarkLink; }
			set { _BookmarkLink = value; }
		}

		public String BookmarkLinkValue(Report rpt, Row r)
		{
			if (_BookmarkLink == null)
				return null;

			return _BookmarkLink.EvaluateString(rpt, r);
		}
	}
}
