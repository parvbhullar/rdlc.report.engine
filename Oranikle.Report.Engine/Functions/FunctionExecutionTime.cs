/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.IO;

using Oranikle.Report.Engine;


namespace Oranikle.Report.Engine
{
	/// <summary>
	/// DateTime Report started; actually the time that data is retrieved
	/// </summary>
	[Serializable]
	public class FunctionExecutionTime : IExpr
	{
		/// <summary>
		/// DateTime report started
		/// </summary>
		public FunctionExecutionTime() 
		{
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.DateTime;
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
			return EvaluateDateTime(rpt, row);
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{	
			DateTime result = EvaluateDateTime(rpt, row);
			return Convert.ToDouble(result);
		}
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			DateTime result = EvaluateDateTime(rpt, row);

			return Convert.ToDecimal(result);
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            DateTime result = EvaluateDateTime(rpt, row);

            return Convert.ToInt32(result);
        }

		public string EvaluateString(Report rpt, Row row)
		{
			DateTime result = EvaluateDateTime(rpt, row);
			return result.ToString();
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			return rpt.ExecutionTime;
		}
		
		public bool EvaluateBoolean(Report rpt, Row row)
		{
			return false;
		}
	}
}
