/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Linking mechanism defining the tree of the report.
	///</summary>
	[Serializable]
	abstract public class ReportLink
	{
		public ReportDefn OwnerReport;			// Main Report instance
		public ReportLink Parent;			// Parent instance
		public int ObjectNumber;

		public ReportLink(ReportDefn r, ReportLink p)
		{
			OwnerReport = r;
			Parent = p;
			ObjectNumber = r.GetObjectNumber();
		}

		// Give opportunity for report elements to do additional work
		//   e.g.  expressions should be parsed at this point
		abstract public void FinalPass();

		public bool InPageHeaderOrFooter()
		{
			for (ReportLink rl = this.Parent; rl != null; rl = rl.Parent)
			{
				if (rl is PageHeader || rl is PageFooter)
					return true;
			}
			return false;
		}
	}
}
