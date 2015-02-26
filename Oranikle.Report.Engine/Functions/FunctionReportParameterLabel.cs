/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.IO;
using System.Reflection;


using Oranikle.Report.Engine;


namespace Oranikle.Report.Engine
{
	/// <summary>
	/// Obtain the runtime value of a report parameter label.
	/// </summary>
	[Serializable]
	public class FunctionReportParameterLabel : FunctionReportParameter
	{
		/// <summary>
		/// obtain value of ReportParameter
		/// </summary>
		public FunctionReportParameterLabel(ReportParameter parm): base(parm) 
		{
		}

		public override TypeCode GetTypeCode()
		{
            if (this.ParameterMethod == ReportParameterMethodEnum.Value)
                return TypeCode.String;
            else
                return base.GetTypeCode();
		}

		public override bool IsConstant()
		{
			return false;
		}

		public override IExpr ConstantOptimization()
		{	// not a constant expression
			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public override object Evaluate(Report rpt, Row row)
		{
			string v = base.EvaluateString(rpt, row);

			if (p.ValidValues == null)
				return v;

			string[] displayValues = p.ValidValues.DisplayValues(rpt);
			object[] dataValues = p.ValidValues.DataValues(rpt);

			for (int i=0; i < dataValues.Length; i++)
			{
				if (dataValues[i].ToString() == v)
					return displayValues[i];
			}

			return v;
		}
		
		public override double EvaluateDouble(Report rpt, Row row)
		{	
			string r = EvaluateString(rpt, row);

			return r == null? double.MinValue: Convert.ToDouble(r);
		}
		
		public override decimal EvaluateDecimal(Report rpt, Row row)
		{
			string r = EvaluateString(rpt, row);

			return r == null? decimal.MinValue: Convert.ToDecimal(r);
		}

		public override string EvaluateString(Report rpt, Row row)
		{
			return (string) Evaluate(rpt, row);
		}

		public override DateTime EvaluateDateTime(Report rpt, Row row)
		{
			string r = EvaluateString(rpt, row);

			return r == null? DateTime.MinValue: Convert.ToDateTime(r);
		}

		public override bool EvaluateBoolean(Report rpt, Row row)
		{
			string r = EvaluateString(rpt, row);

			return r.ToLower() == "true"? true: false;
		}
	}
}
