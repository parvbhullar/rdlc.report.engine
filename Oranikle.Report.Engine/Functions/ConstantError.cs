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
	/// Error in expression;  don't want iexpr to be null so this is returned.
	/// </summary>
	[Serializable]
	public class ConstantError : IExpr
	{
		string _Value;
		/// <summary>
		/// Constant - as opposed to an expression
		/// </summary>
		public ConstantError(string v) 
		{
			_Value = v;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.String;
		}

		public bool IsConstant()
		{
			return true;
		}

		public IExpr ConstantOptimization()
		{	// already constant expression
			return this;
		}

		public object Evaluate(Report rpt, Row row)
		{
			return _Value;
		}

		public string EvaluateString(Report rpt, Row row)
		{
			return _Value;
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{
			return Convert.ToDouble(_Value);
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            return Convert.ToInt32(_Value);
        }

        public decimal EvaluateDecimal(Report rpt, Row row)
		{
			return Convert.ToDecimal(_Value);
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			return Convert.ToDateTime(_Value);
		}

		public bool EvaluateBoolean(Report rpt, Row row)
		{
			return Convert.ToBoolean(_Value);
		}
	}
}
