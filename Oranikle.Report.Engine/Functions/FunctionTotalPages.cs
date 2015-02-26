/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.IO;

using Oranikle.Report.Engine;


namespace Oranikle.Report.Engine
{
	/// <summary>
	/// Total Pages
	/// </summary>
	[Serializable]
	public class FunctionTotalPages : IExpr
	{
		/// <summary>
		/// Total page count; relys on PageHeader, PageFooter to set Report.TotalPages
		/// </summary>
		public FunctionTotalPages() 
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
            return rpt == null ? (int) 1 : (int) rpt.TotalPages;
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{	
			return rpt == null? 1: rpt.TotalPages;
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            return rpt == null ? 1 : rpt.TotalPages;
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
