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
	/// Aggregate function: Level
	/// </summary>
	[Serializable]
	public class FunctionAggrLevel : FunctionAggr, IExpr
	{
		/// <summary>
		/// Aggregate function: Level
		/// 
		///	Return type is double
		/// </summary>
		public FunctionAggrLevel(object scp):base(null, scp) 
		{
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;		// although it is always an integer
		}

		public object Evaluate(Report rpt, Row row)
		{
			return (object) EvaluateDouble(rpt, row);
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{
			if (row == null || this._Scope == null)
				return 0;

			Grouping g = this._Scope as Grouping;
			if (g == null || g.ParentGroup == null)
				return 0;

//			GroupEntry ge = row.R.CurrentGroups[g.Index];	// current group entry

			return row.Level;
		}
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			double d = EvaluateDouble(rpt, row);

			return Convert.ToDecimal(d);
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            double d = EvaluateDouble(rpt, row);

            return Convert.ToInt32(d);
        }

		public string EvaluateString(Report rpt, Row row)
		{
			double result = EvaluateDouble(rpt, row);
			return Convert.ToString(result);
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return Convert.ToDateTime(result);
		}
	}
}
