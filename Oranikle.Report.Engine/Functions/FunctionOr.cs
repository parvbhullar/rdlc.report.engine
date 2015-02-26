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
	/// OR operator  of form lhs || rhs
	/// </summary>
	[Serializable]
	public class FunctionOr : FunctionBinary, IExpr
	{

		/// <summary>
		/// Or two boolean expressions together of the form a || b
		/// </summary>
		public FunctionOr(IExpr lhs, IExpr rhs) 
		{
			_lhs = lhs;
			_rhs = rhs;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
		}

		public IExpr ConstantOptimization()
		{
			_lhs = _lhs.ConstantOptimization();
			_rhs = _rhs.ConstantOptimization();
			bool bLeftConst = _lhs.IsConstant();
			bool bRightConst = _rhs.IsConstant();
			if (bLeftConst && bRightConst)
			{
				bool b = EvaluateBoolean(null, null);
				return new ConstantBoolean(b);
			}
			else if (bRightConst)
			{
				bool b = _rhs.EvaluateBoolean(null, null);
				if (b)
					return new ConstantBoolean(true);
				else 
					return _lhs;
			}
			else if (bLeftConst)
			{
				bool b = _lhs.EvaluateBoolean(null, null);
				if (b)
					return new ConstantBoolean(true);
				else
					return _rhs;
			}

			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Report rpt, Row row)
		{
			return EvaluateBoolean(rpt, row);
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{
			return Double.NaN;
		}
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			return decimal.MinValue;
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            return int.MinValue;
        }

		public string EvaluateString(Report rpt, Row row)
		{
			bool result = EvaluateBoolean(rpt, row);
			return result.ToString();
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			return DateTime.MinValue;
		}

		public bool EvaluateBoolean(Report rpt, Row row)
		{
			bool r = _lhs.EvaluateBoolean(rpt, row);
			if (r)
				return true;
			return  _rhs.EvaluateBoolean(rpt, row);
		}
	}
}
