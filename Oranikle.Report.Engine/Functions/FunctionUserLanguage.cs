/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.IO;
using System.Globalization;

using Oranikle.Report.Engine;


namespace Oranikle.Report.Engine
{
	/// <summary>
	/// The Language field in the User collection.
	/// </summary>
	[Serializable]
	public class FunctionUserLanguage : IExpr
	{
		/// <summary>
		/// Client user language
		/// </summary>
		public FunctionUserLanguage() 
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
			throw new Exception("Invalid conversion from Language to double.");
		}
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			throw new Exception("Invalid conversion from Language to Decimal.");
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            throw new Exception("Invalid conversion from Language to Int32.");
        }
		public string EvaluateString(Report rpt, Row row)
		{
			if (rpt == null || rpt.ClientLanguage == null)
				return CultureInfo.CurrentCulture.ThreeLetterISOLanguageName;
			else
				return rpt.ClientLanguage;
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			throw new Exception("Invalid conversion from Language to DateTime.");
		}

		public bool EvaluateBoolean(Report rpt, Row row)
		{
			throw new Exception("Invalid conversion from Language to boolean.");
		}
	}
}
