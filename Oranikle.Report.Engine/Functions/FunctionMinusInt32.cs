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
	/// Minus operator  of form lhs - rhs where both operands are decimal.
	/// </summary>
	[Serializable]
	public class FunctionMinusInt32 : FunctionBinary, IExpr
	{

		/// <summary>
		/// Do minus on decimal data types
		/// </summary>
		public FunctionMinusInt32() 
		{
			_lhs = null;
			_rhs = null;
		}

		public FunctionMinusInt32(IExpr lhs, IExpr rhs) 
		{
			_lhs = lhs;
			_rhs = rhs;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Int32;
		}

		public IExpr ConstantOptimization()
		{
			_lhs = _lhs.ConstantOptimization();
			_rhs = _rhs.ConstantOptimization();
			bool bLeftConst = _lhs.IsConstant();
			bool bRightConst = _rhs.IsConstant();
			if (bLeftConst && bRightConst)
			{
				int d = EvaluateInt32(null, null);
				return new ConstantInteger(d);
			}
			else if (bRightConst)
			{
				int d = _rhs.EvaluateInt32(null, null);
				if (d == 0)
					return _lhs;
			}

			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Report rpt, Row row)
		{
			return EvaluateInt32(rpt, row);
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{
			int result = EvaluateInt32(rpt, row);

			return Convert.ToDouble(result);
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            int lhs = _lhs.EvaluateInt32(rpt, row);
            int rhs = _rhs.EvaluateInt32(rpt, row);

            return (lhs - rhs);
        }
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
            int result = EvaluateInt32(rpt, row);

            return Convert.ToDecimal(result);
        }

		public string EvaluateString(Report rpt, Row row)
		{
			int result = EvaluateInt32(rpt, row);
			return result.ToString();
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			int result = EvaluateInt32(rpt, row);
			return Convert.ToDateTime(result);
		}

		public bool EvaluateBoolean(Report rpt, Row row)
		{
			int result = EvaluateInt32(rpt, row);
			return Convert.ToBoolean(result);
		}
	}
}
