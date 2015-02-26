/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// CategoryGrouping definition and processing.
	///</summary>
	[Serializable]
	public class CategoryGrouping : ReportLink
	{
		// A CategoryGrouping must have either DynamicCategories or StaticCategories
		//  but can't have both.
		DynamicCategories _DynamicCategories;	// Dynamic Category headings for this grouping
		StaticCategories _StaticCategories;		// Category headings for this grouping		
	
		public CategoryGrouping(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_DynamicCategories=null;
			_StaticCategories=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "DynamicCategories":
						_DynamicCategories = new DynamicCategories(r, this, xNodeLoop);
						break;
					case "StaticCategories":
						_StaticCategories = new StaticCategories(r, this, xNodeLoop);
						break;
					default:	
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown CategoryGrouping element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if ((_DynamicCategories == null && _StaticCategories == null) ||
				(_DynamicCategories != null && _StaticCategories != null))
				OwnerReport.rl.LogError(8, "CategoryGrouping requires either DynamicCategories element or StaticCategories element, but not both.");
		}
		
		override public void FinalPass()
		{
			if (_DynamicCategories != null)
				_DynamicCategories.FinalPass();
			if (_StaticCategories != null)
				_StaticCategories.FinalPass();
			return;
		}

		public DynamicCategories DynamicCategories
		{
			get { return  _DynamicCategories; }
			set {  _DynamicCategories = value; }
		}

		public StaticCategories StaticCategories
		{
			get { return  _StaticCategories; }
			set {  _StaticCategories = value; }
		}
	}
}
