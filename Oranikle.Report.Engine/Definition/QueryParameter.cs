/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Represent query parameter.
	///</summary>
	[Serializable]
	public class QueryParameter : ReportLink, IComparable
	{
		Name _Name;		// Name of the parameter
		Expression _Value;	// (Variant or Variant Array)
					//An expression that evaluates to the value to
					//hand to the data source. The expression can
					//refer to report parameters but cannot contain
					//references to report elements, fields in the data
					//model or aggregate functions.
					//In the case of a parameter to a Values or
					//DefaultValue query, the expression can only
					//refer to report parameters that occur earlier in
					//the parameters list. The value for this query
					//parameter is then taken from the user selection
					//for that earlier report parameter.
	
		public QueryParameter(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
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
						OwnerReport.rl.LogError(4, "Unknown QueryParameter element '" + xNodeLoop.Name + "' ignored.");
						break;
				}
			}
			if (_Name == null)
				OwnerReport.rl.LogError(8, "QueryParameter name is required but not specified.");

			if (_Value == null)
				OwnerReport.rl.LogError(8, "QueryParameter Value is required but not specified or invalid for " + _Name==null? "<unknown name>": _Name.Nm);
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
        public bool IsArray
        {
            get
            {
                if (_Value == null)         // when null; usually means a parsing error
                    return false;           //   but we want to continue as far as we can
                return (_Value.GetTypeCode() == TypeCode.Object);
            }
        }
		#region IComparable Members

		public int CompareTo(object obj)
		{
			QueryParameter qp = obj as QueryParameter;
			if (qp == null)
				return 0;
			
			string tname = this.Name.Nm;
			string qpname =	qp.Name.Nm;

			int length_diff = qpname.Length - tname.Length;
			if (length_diff == 0)
				return qpname.CompareTo(tname);

			return length_diff;
		}

		#endregion
	}
}
