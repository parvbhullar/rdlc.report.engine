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
	/// Constant Integer
	/// </summary>
	[Serializable]
	public class ConstantInteger : IExpr
	{
		int _Value;		// value of the constant

		/// <summary>
		/// passed class name, function name, and args for evaluation
		/// </summary>
		public ConstantInteger(string v) 
		{
			_Value = Convert.ToInt32(v);
		}

		public ConstantInteger(int v) 
		{
			_Value = v;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Int32;
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
			return Convert.ToString(_Value);
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{
			return _Value;
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            return _Value;
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
