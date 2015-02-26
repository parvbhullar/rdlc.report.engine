/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.IO;

using Oranikle.Report.Engine;


namespace Oranikle.Report.Engine
{
	/// <summary>
	/// Report Folder
	/// </summary>
	[Serializable]
	public class FunctionReportFolder : IExpr
	{
		/// <summary>
		/// Folder that holds Report
		/// </summary>
		public FunctionReportFolder() 
		{
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.String;
		}

		public bool IsConstant()
		{
			return false;
		}

		public IExpr ConstantOptimization()
		{	// not a constant expression
			return this;
		}

		public object Evaluate(Report rpt, Row row)
		{
			return rpt.Folder;
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{	
			return double.NaN;
		}
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			return Decimal.MinValue;
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            return int.MinValue;
        }

		public string EvaluateString(Report rpt, Row row)
		{
			return rpt == null? "": rpt.Folder;
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			string result = EvaluateString(rpt, row);
			return Convert.ToDateTime(result);
		}

		public bool EvaluateBoolean(Report rpt, Row row)
		{
			return false;
		}
	}
}
