/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// In Matrix, the dynamic categories.
	///</summary>
	[Serializable]
	public class DynamicCategories : ReportLink
	{
		Grouping _Grouping;	// The expressions to group the data by. Page
							// breaks in the grouping are not allowed.
		Sorting _Sorting;	// The expressions to sort the data by
		Expression _Label;	//(Variant) The label displayed on the axis.		
	
		public DynamicCategories(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Grouping=null;
			_Sorting=null;
			_Label=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Grouping":
						_Grouping = new Grouping(r, this, xNodeLoop);
						break;
					case "Sorting":
						_Sorting = new Sorting(r, this, xNodeLoop);
						break;
					case "Label":
						_Label = new Expression(r, this, xNodeLoop, ExpressionType.Variant);
						break;
					default:
						break;
				}
			}
			if (_Grouping == null)
				OwnerReport.rl.LogError(8, "DynamicCategories requires the Grouping element.");
		}

		// Handle parsing of function in final pass
		override public void FinalPass()
		{
			if (_Grouping != null)
				_Grouping.FinalPass();
			if (_Sorting != null)
				_Sorting.FinalPass();
			if (_Label != null)
				_Label.FinalPass();
			return;
		}

		public Grouping Grouping
		{
			get { return  _Grouping; }
			set {  _Grouping = value; }
		}

		public Sorting Sorting
		{
			get { return  _Sorting; }
			set {  _Sorting = value; }
		}

		public Expression Label
		{
			get { return  _Label; }
			set {  _Label = value; }
		}
	}
}
