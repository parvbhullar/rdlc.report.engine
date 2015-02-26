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
	/// Reference to a Textbox data value
	/// </summary>
	[Serializable]
	public class FunctionTextbox : IExpr
	{
		Textbox t;	

		/// <summary>
		/// obtain value of Textbox
		/// </summary>
		public FunctionTextbox(Textbox tb, string uniquename) 
		{
			t=tb;
			if (uniquename == null)	
				return;
			// We need to register this expression with the Textbox
			tb.AddExpressionReference(uniquename);
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Object;
		}

		public bool IsConstant()
		{
			return false;
		}

		public IExpr ConstantOptimization()
		{	// not a constant expression
			return this;
		}

		// Evaluate the value for the expression
		public object Evaluate(Report rpt, Row row)
		{
			return t.Evaluate(rpt, row);
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
			return result.ToString();
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return Convert.ToDateTime(result);
		}

		public bool EvaluateBoolean(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return Convert.ToBoolean(result);
		}
	}
}
