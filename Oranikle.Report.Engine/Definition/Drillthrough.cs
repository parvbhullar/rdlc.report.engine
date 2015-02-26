/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Defines information needed for creating links to URLs in a report.  Primarily HTML.
	///</summary>
	[Serializable]
	public class Drillthrough : ReportLink
	{
		string _ReportName;	// URL The path of the drillthrough report. Paths may be
							// absolute or relative.
		DrillthroughParameters _DrillthroughParameters;	// Parameters to the drillthrough report		
	
		public Drillthrough(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_ReportName=null;
			_DrillthroughParameters=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "ReportName":
						_ReportName = xNodeLoop.InnerText;
						break;
					case "Parameters":
						_DrillthroughParameters = new DrillthroughParameters(r, this, xNodeLoop);
						break;
					default:
						break;
				}
			}
			if (_ReportName == null)
				OwnerReport.rl.LogError(8, "Drillthrough requires the ReportName element.");
		}
		
		override public void FinalPass()
		{
			if (_DrillthroughParameters != null)
				_DrillthroughParameters.FinalPass();
			return;
		}

		public string ReportName
		{
			get { return  _ReportName; }
			set {  _ReportName = value; }
		}

		public DrillthroughParameters DrillthroughParameters
		{
			get { return  _DrillthroughParameters; }
			set {  _DrillthroughParameters = value; }
		}
	}
}
