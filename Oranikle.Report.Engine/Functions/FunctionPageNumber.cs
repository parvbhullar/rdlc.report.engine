/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.IO;

using Oranikle.Report.Engine;


namespace Oranikle.Report.Engine
{
	/// <summary>
	/// Page number operator.   Relies on render to set the proper page #.
	/// </summary>
	[Serializable]
	public class FunctionPageNumber : IExpr
	{
		/// <summary>
		/// Current page number
		/// </summary>
		public FunctionPageNumber() 
		{
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Int32;
		}

		public bool IsConstant()
		{
			return false;
		}

		public IExpr ConstantOptimization()
		{	// not a constant expression
			return this;
		}

		// Evaluate is for interpretation  
		public object Evaluate(Report rpt, Row row)
		{
            return rpt == null ? (int) 0 : (int) rpt.PageNumber;
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{	
			return rpt == null? 0: rpt.PageNumber;
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            return rpt == null ? 0 : rpt.PageNumber;
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
			return false;
		}
	}
}
