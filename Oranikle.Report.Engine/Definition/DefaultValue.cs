/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// The default value for a parameter.
	///</summary>
	[Serializable]
	public class DefaultValue : ReportLink
	{
		// Only one of Values and DataSetReference can be specified.
		DataSetReference _DataSetReference;	// The query to execute to obtain the default value(s) for the parameter.
									// The default is the first value of the ValueField.
		Values _Values;		// The default values for the parameter

		public DefaultValue(ReportDefn r, ReportLink p, XmlNode xNode) : base(r, p)
		{
			_DataSetReference=null;
			_Values=null;

			// Loop thru all the child nodes
			foreach(XmlNode xNodeLoop in xNode.ChildNodes)
			{
				if (xNodeLoop.NodeType != XmlNodeType.Element)
					continue;
				switch (xNodeLoop.Name)
				{
					case "DataSetReference":
						_DataSetReference = new DataSetReference(r, this, xNodeLoop);
						break;
					case "Values":
						_Values = new Values(r, this, xNodeLoop);
						break;
					default:
						break;
				}
			}
		}
		
		override public void FinalPass()
		{
			if (_DataSetReference != null)
				_DataSetReference.FinalPass();
			if (_Values != null)
				_Values.FinalPass();
			return;
		}

		public DataSetReference DataSetReference
		{
			get { return  _DataSetReference; }
			set {  _DataSetReference = value; }
		}

		public object[] GetValue(Report rpt)
		{
			if (_Values != null)
				return ValuesCalc(rpt);
			object[] dValues = this.GetDataValues(rpt);
			if (dValues != null)
				return dValues;

			string[] dsValues;
			if (_DataSetReference != null)
				_DataSetReference.SupplyValues(rpt, out dsValues, out dValues);

			this.SetDataValues(rpt, dValues);
			return dValues;
		}

		public Values Values
		{
			get { return  _Values; }
			set {  _Values = value; }
		}

		public object[] ValuesCalc(Report rpt)
		{
			if (_Values == null)
				return null;
			object[] result = new object[_Values.Count];
			int index=0;
			foreach (Expression v in _Values)
			{
				result[index++] = v.Evaluate(rpt, null);
			}
			return result;
		}

		private object[] GetDataValues(Report rpt)
		{
			return rpt.Cache.Get(this, "datavalues") as object[];
		}

		private void SetDataValues(Report rpt, object[] vs)
		{
			if (vs == null)
				rpt.Cache.Remove(this, "datavalues");
			else
				rpt.Cache.AddReplace(this, "datavalues", vs);
		}
	}
}
