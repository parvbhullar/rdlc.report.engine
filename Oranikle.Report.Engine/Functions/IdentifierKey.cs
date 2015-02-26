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
	/// IdentifierKey
	/// </summary>
	public enum IdentifierKeyEnum
	{
		/// <summary>
		/// Recursive
		/// </summary>
		Recursive,
		/// <summary>
		/// Simple
		/// </summary>
		Simple	
	}

	[Serializable]
	public class IdentifierKey : IExpr
	{
		IdentifierKeyEnum _Value;		// value of the identifier

		/// <summary>
		/// 
		/// </summary>
		public IdentifierKey(IdentifierKeyEnum v) 
		{
			_Value = v;
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Object;			
		}

		public bool IsConstant()
		{
			return false;
		}

		public IdentifierKeyEnum Value
		{
			get {return _Value;}
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
			return Double.NaN;
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
			return null;
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			return DateTime.MinValue;
		}

		public bool EvaluateBoolean(Report rpt, Row row)
		{
			return false;
		}
	}
}
