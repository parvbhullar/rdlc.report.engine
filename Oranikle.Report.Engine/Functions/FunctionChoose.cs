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
	/// Choose function of the form Choose(int, expr1, expr2, ...)
	/// 
	///	
	/// </summary>
	[Serializable]
	public class FunctionChoose : IExpr
	{
		IExpr[] _expr;
		TypeCode _tc;

		/// <summary>
		/// Choose function of the form Choose(int, expr1, expr2, ...)
		/// </summary>
		public FunctionChoose(IExpr[] ie) 
		{
			_expr = ie;
			_tc = _expr[1].GetTypeCode();

		}

		public TypeCode GetTypeCode()
		{
			return _tc;
		}

		public bool IsConstant()
		{
			return false;		// we could be more sophisticated here; but not much benefit
		}

		public IExpr ConstantOptimization()
		{
			// simplify all expression if possible
			for (int i=0; i < _expr.Length; i++)
			{
				_expr[i] = _expr[i].ConstantOptimization();
			}

			return this;
		}

		// 
		public object Evaluate(Report rpt, Row row)
		{
			double di = _expr[0].EvaluateDouble(rpt, row);
			int i = (int) di;		// force it to integer; we'll accept truncation
			if (i >= _expr.Length || i <= 0)
				return null;
			
			return _expr[i].Evaluate(rpt, row);
		}

		public bool EvaluateBoolean(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return Convert.ToBoolean(result);
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return Convert.ToDouble(result);
		}
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return Convert.ToDecimal(result);
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            object result = Evaluate(rpt, row);
            return Convert.ToInt32(result);
        }

		public string EvaluateString(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return Convert.ToString(result);
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return Convert.ToDateTime(result);
		}
	}
}
