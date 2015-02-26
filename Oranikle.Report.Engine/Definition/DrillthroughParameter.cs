/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// A drillthrough parameter.
	///</summary>
	[Serializable]
	public class DrillthroughParameter : ReportLink
	{
		Name _Name;			// Name of the parameter
		Expression _Value;	// (Variant) An expression that evaluates to the value to
							// hand in for the parameter to the Drillthough.
		Expression _Omit;	// (Boolean) Indicates the parameter should be skipped.
	
		public DrillthroughParameter(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_Name=null;
			_Value=null;
			_Omit=null;
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
				OwnerReport.rl.LogError(8, "Parameter Name attribute required.'");
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
					case "Omit":
						_Omit = new Expression(r, this, xNodeLoop, ExpressionType.Boolean);
						break;
					default:
						// don't know this element - log it
						OwnerReport.rl.LogError(4, "Unknown Parameter element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
		}

		// Handle parsing of function in final pass
		override public void FinalPass()
		{
			if (_Value != null)
				_Value.FinalPass();
			if (_Omit != null)
				_Omit.FinalPass();
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

		public Expression Omit
		{
			get { return  _Omit; }
			set {  _Omit = value; }
		}

		public bool OmitValue(Report rpt, Row r)
		{
			if (_Omit == null)
				return false;

			return _Omit.EvaluateBoolean(rpt, r);
		}
	}
}
