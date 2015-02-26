/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// A value used in a filter.
	///</summary>
	[Serializable]
	public class FilterValue : ReportLink
	{
		Expression _Expression;			// 

		public FilterValue(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Expression = new Expression(r,this,xNode, ExpressionType.Variant);
		}

		// Handle parsing of function in final pass
		override public void FinalPass()
		{
			if (_Expression != null)
				_Expression.FinalPass();
			return;
		}

		public Expression Expression
		{
			get { return  _Expression; }
			set {  _Expression = value; }
		}
	}
}
