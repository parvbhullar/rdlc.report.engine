/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.IO;

using Oranikle.Report.Engine;


namespace Oranikle.Report.Engine
{
	/// <summary>
	/// User ID- Report.UserID must be set by the client to be accurate in multi-user case
	/// </summary>
	[Serializable]
	public class FunctionUserID : IExpr
	{
		/// <summary>
		/// Client user id
		/// </summary>
		public FunctionUserID() 
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
		{	
			return this;
		}

		// Evaluate is for interpretation  
		public object Evaluate(Report rpt, Row row)
		{
			return EvaluateString(rpt, row);
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{	
			string result = EvaluateString(rpt, row);
			return Convert.ToDouble(result);		
		}
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			string result = EvaluateString(rpt, row);

			return Convert.ToDecimal(result);
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            string result = EvaluateString(rpt, row);

            return Convert.ToInt32(result);
        }
		public string EvaluateString(Report rpt, Row row)
		{
			if (rpt == null || rpt.UserID == null)
				return Environment.UserName;
			else
				return rpt.UserID;
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
