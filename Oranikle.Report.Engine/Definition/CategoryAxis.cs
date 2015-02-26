/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// CategoryAxis definition and processing.
	///</summary>
	[Serializable]
	public class CategoryAxis : ReportLink
	{
		Axis _Axis;		// Display properties for the category axis	
	
		public CategoryAxis(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Axis = null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Axis":
						_Axis = new Axis(r, this, xNodeLoop);
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown CategoryAxis element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
		}
		
		override public void FinalPass()
		{
			if (_Axis != null)
				_Axis.FinalPass();
			return;
		}


		public Axis Axis
		{
			get { return  _Axis; }
			set {  _Axis = value; }
		}
	}

}
