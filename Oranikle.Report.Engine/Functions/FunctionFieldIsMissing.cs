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
	/// IsMissing attribute
	/// </summary>
	[Serializable]
	public class FunctionFieldIsMissing : FunctionField
	{
		/// <summary>
		/// Determine if value of Field is available
		/// </summary>
		public FunctionFieldIsMissing(Field fld) : base(fld)
		{
		}
		public FunctionFieldIsMissing(string method) : base(method)
		{
		}

		public override TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
		}

		public override bool IsConstant()
		{
			return false;
		}

		public override IExpr ConstantOptimization()
		{	
			return this;	// not a constant
		}

		// 
		public override object Evaluate(Report rpt, Row row)
		{
			return EvaluateBoolean(rpt, row);
		}
		
		public override double EvaluateDouble(Report rpt, Row row)
		{
			return EvaluateBoolean(rpt, row)? 1: 0;
		}
		
		public override decimal EvaluateDecimal(Report rpt, Row row)
		{
			return EvaluateBoolean(rpt, row)? 1m: 0m;
		}

		public override string EvaluateString(Report rpt, Row row)
		{
			return EvaluateBoolean(rpt, row)? "True": "False";
		}

		public override DateTime EvaluateDateTime(Report rpt, Row row)
		{
			return DateTime.MinValue;
		}

		public override bool EvaluateBoolean(Report rpt, Row row)
		{
			object o = base.Evaluate(rpt, row);
			return o == null? true: false;
		}
	}
}
