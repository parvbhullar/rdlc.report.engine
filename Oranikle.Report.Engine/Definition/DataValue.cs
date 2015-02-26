/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// In charts, the DataValue defines a single value for the DataPoint.
	///</summary>
	[Serializable]
	public class DataValue : ReportLink
	{
		Expression _Value;	// (Variant) Value expression. Same restrictions as
							//  the expressions in a matrix cell		
		public DataValue(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Value=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "Value":
						_Value = new Expression(r, this, xNodeLoop, ExpressionType.Variant);
						break;
					default:
						break;
				}
			}
			if (_Value == null)
				OwnerReport.rl.LogError(8, "DataValue requires the Value element.");
		}

		// Handle parsing of function in final pass
		override public void FinalPass()
		{
			if (_Value != null)
				_Value.FinalPass();
			return;
		}


		public Expression Value
		{
			get { return  _Value; }
			set {  _Value = value; }
		}
	}
}
