/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;

using Oranikle.Report.Engine;


namespace Oranikle.Report.Engine
{
	/// <summary>
	/// Aggregate function: next
	/// </summary>
	[Serializable]
	public class FunctionAggrPrevious : FunctionAggr, IExpr, ICacheData
	{
		private TypeCode _tc;		// type of result: depends on type of argument
		private string _key;
		/// <summary>
		/// Aggregate function: next returns the next value in a group
		///	Return type is same as input expression	
		/// </summary>
        public FunctionAggrPrevious(List<ICacheData> dataCache, IExpr e, object scp)
            : base(e, scp) 
		{
			_key = "aggrprev" + Interlocked.Increment(ref Parser.Counter).ToString();

			// Determine the result
			_tc = e.GetTypeCode();
			dataCache.Add(this);
		}

		public TypeCode GetTypeCode()
		{
			return _tc;
		}

		public object Evaluate(Report rpt, Row row)
		{
			object v = null;
			if (row == null)
				return null;
			bool bSave=true;
			RowEnumerable re = this.GetDataScope(rpt, row, out bSave);
			if (re == null)
				return null;

			Row crow=null;
			foreach (Row r in re)
			{
				if (r == row)
					break;
				crow = r;
			}
			if (crow != null)
				v = _Expr.Evaluate(rpt, crow);
			return v;
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return result == null? double.MinValue: Convert.ToDouble(result);
		}
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return result == null? decimal.MinValue: Convert.ToDecimal(result);
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            object result = Evaluate(rpt, row);
            return result == null ? int.MinValue : Convert.ToInt32(result);
        }

		public string EvaluateString(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return result == null? null: Convert.ToString(result);
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return result == null? DateTime.MinValue: Convert.ToDateTime(result);
		}

		private object GetValue(Report rpt)
		{
			return rpt.Cache.Get(_key);
		}

		private void SetValue(Report rpt, object o)
		{
			rpt.Cache.AddReplace(_key, o);
		}

		#region ICacheData Members

		public void ClearCache(Report rpt)
		{
			rpt.Cache.Remove(_key);
		}

		#endregion
	}
}
