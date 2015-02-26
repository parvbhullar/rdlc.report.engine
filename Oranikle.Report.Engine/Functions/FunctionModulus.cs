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
	/// Modulus operator  of form lhs % rhs
	/// </summary>
	[Serializable]
	public class FunctionModulus : FunctionBinary, IExpr
	{

		/// <summary>
		/// Do modulus on double data types
		/// </summary>
		public FunctionModulus(IExpr lhs, IExpr rhs) 
		{
			_lhs = lhs;
			_rhs = rhs;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		public IExpr ConstantOptimization()
		{
			_lhs = _lhs.ConstantOptimization();
			_rhs = _rhs.ConstantOptimization();
			bool bLeftConst = _lhs.IsConstant();
			bool bRightConst = _rhs.IsConstant();
			if (bLeftConst && bRightConst)
			{
				double d = EvaluateDouble(null, null);
				return new ConstantDouble(d);
			}
			else if (bRightConst)
			{
				double d = _rhs.EvaluateDouble(null, null);
				if (d == 1)
					return _lhs;
			}
			else if (bLeftConst)
			{
				double d = _lhs.EvaluateDouble(null, null);
				if (d == 0)
					return new ConstantDouble(0);
			}

			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Report rpt, Row row)
		{
			return EvaluateDouble(rpt, row);
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{
			double lhs = _lhs.EvaluateDouble(rpt, row);
			double rhs = _rhs.EvaluateDouble(rpt, row);
			// n % d = n - d*INT(n/d)  where INT rounds a number down to the nearest integer
			double temp = (int) (lhs/rhs);

			return lhs - rhs*temp;
		}
        
        public int EvaluateInt32(Report rpt, Row row)
        {
            double result = EvaluateDouble(rpt, row);

            return Convert.ToInt32(result);
        }
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			double result = EvaluateDouble(rpt, row);

			return Convert.ToDecimal(result);
		}

		public string EvaluateString(Report rpt, Row row)
		{
			double result = EvaluateDouble(rpt, row);
			return result.ToString();
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			double result = EvaluateDouble(rpt, row);
			return Convert.ToDateTime(result);
		}

		public bool EvaluateBoolean(Report rpt, Row row)
		{
			double result = EvaluateDouble(rpt, row);
			return Convert.ToBoolean(result);
		}
	}
}
