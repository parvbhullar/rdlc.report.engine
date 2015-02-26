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
	/// Identifier
	/// </summary>
	[Serializable]
	public class Identifier : IExpr
	{
		string _Value;		// value of the identifier

		/// <summary>
		/// passed class name, function name, and args for evaluation
		/// </summary>
		public Identifier(string v) 
		{
			string lv = v.ToLower();
			if (lv == "null" || lv == "nothing")
				_Value = null;
			else
				_Value = v;
		}

		public bool IsNothing
		{
			get { return _Value == null? true: false; }
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Object;			// TODO
		}

		public bool IsConstant()
		{
			return false;
		}

		public IExpr ConstantOptimization()
		{	
			return this;
		}

		public object Evaluate(Report rpt, Row row)
		{	
			return _Value;
		}

		public double EvaluateDouble(Report rpt, Row row)
		{
			return Convert.ToDouble(Evaluate(rpt, row));
		}
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			return Convert.ToDecimal(Evaluate(rpt, row));
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            return Convert.ToInt32(Evaluate(rpt, row));
        }

		public string EvaluateString(Report rpt, Row row)
		{
			return Convert.ToString(Evaluate(rpt, row));
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			return Convert.ToDateTime(Evaluate(rpt, row));
		}

		public bool EvaluateBoolean(Report rpt, Row row)
		{
			return Convert.ToBoolean(Evaluate(rpt, row));
		}
	}
}
