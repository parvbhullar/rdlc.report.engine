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
	/// Unary minus operator with a decimal operand
	/// </summary>
	[Serializable]
	public class FunctionUnaryMinusDecimal : IExpr
	{
		IExpr _rhs;			// rhs

		/// <summary>
		/// Do minus on decimal data type
		/// </summary>
		public FunctionUnaryMinusDecimal() 
		{
			_rhs = null;
		}

		public FunctionUnaryMinusDecimal(IExpr r) 
		{
			_rhs = r;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Decimal;
		}

		public bool IsConstant()
		{
			return _rhs.IsConstant();
		}

		public IExpr ConstantOptimization()
		{
			_rhs = _rhs.ConstantOptimization();
			if (_rhs.IsConstant())
			{
				decimal d = EvaluateDecimal(null, null);
				return new ConstantDecimal(d);
			}

			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Report rpt, Row row)
		{
			return EvaluateDecimal(rpt, row);
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{
			decimal result = EvaluateDecimal(rpt, row);

			return Convert.ToDouble(result);
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            decimal result = EvaluateDecimal(rpt, row);

            return Convert.ToInt32(result);
        }
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			decimal rhs = _rhs.EvaluateDecimal(rpt, row);

			return (decimal) (-rhs);
		}

		public string EvaluateString(Report rpt, Row row)
		{
			decimal result = EvaluateDecimal(rpt, row);
			return result.ToString();
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			decimal result = EvaluateDecimal(rpt, row);
			return Convert.ToDateTime(result);
		}

		public bool EvaluateBoolean(Report rpt, Row row)
		{
			return false;
		}

		public IExpr Rhs
		{
			get { return  _rhs; }
			set {  _rhs = value; }
		}

	}
}
