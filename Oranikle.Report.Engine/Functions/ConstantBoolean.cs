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
	/// Boolean constant
	/// </summary>
	[Serializable]
	public class ConstantBoolean : IExpr
	{
		bool _Value;		// value of the constant

		/// <summary>
		/// A boolean constant: i.e. true or false.
		/// </summary>
		public ConstantBoolean(string v) 
		{
			_Value = Convert.ToBoolean(v);
		}

		public ConstantBoolean(bool v) 
		{
			_Value = v;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
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
			return Convert.ToDouble(_Value);
		}
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			return Convert.ToDecimal(_Value);
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            return Convert.ToInt32(_Value);
        }

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			return Convert.ToDateTime(_Value);
		}

		public bool EvaluateBoolean(Report rpt, Row row)
		{
			return _Value;
		}
	}
}
