/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// A parameter for a subreport.
	///</summary>
	[Serializable]
	public class SubreportParameter : ReportLink
	{
		Name _Name;		// Name of the parameter
		Expression _Value;	// (Variant) An expression that evaluates to the value to
							// hand in for the parameter to the Subreport.
	
		public SubreportParameter(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Name=null;
			_Value=null;
			// Run thru the attributes
			foreach(XmlAttribute xAttr in xNode.Attributes)
			{
				switch (xAttr.Name)
				{
					case "Name":
						_Name = new Name(xAttr.Value);
						break;
				}
			}

			if (_Name == null)
			{	// Name is required for parameters
				OwnerReport.rl.LogError(8, "Parameter Name attribute required.");
			}

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
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Subreport parameter element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}

			if (_Value == null)
			{	// Value is required for parameters
				OwnerReport.rl.LogError(8, "The Parameter Value element is required but was not specified.");
			}
		}

		// Handle parsing of function in final pass
		override public void FinalPass()
		{
			if (_Value != null)
				_Value.FinalPass();
			return;
		}

		public Name Name
		{
			get { return  _Name; }
			set {  _Name = value; }
		}

		public Expression Value
		{
			get { return  _Value; }
			set {  _Value = value; }
		}

		public string ValueValue(Report rpt, Row r)
		{
			if (_Value == null)
				return "";

			return _Value.EvaluateString(rpt, r);
		}
	}
}
