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
	/// Aggregate function: RunningValue stdevp
	/// </summary>
	[Serializable]
	public class FunctionAggrRvStdevp : FunctionAggr, IExpr, ICacheData
	{
		string _key;				// key for caching holding between invocations
		/// <summary>
		/// Aggregate function: RunningValue Stdevp returns the Stdevp of all values of the
		///		expression within the scope up to that row
		///	Return type is double for all expressions.	
		/// </summary>
        public FunctionAggrRvStdevp(List<ICacheData> dataCache, IExpr e, object scp)
            : base(e, scp) 
		{
			_key = "aggrrvstdevp" + Interlocked.Increment(ref Parser.Counter).ToString();
			dataCache.Add(this);
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		public object Evaluate(Report rpt, Row row)
		{
			return (object) EvaluateDouble(rpt, row);
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{
			bool bSave=true;
			IEnumerable re = this.GetDataScope(rpt, row, out bSave);
			if (re == null)
				return double.NaN;

			Row startrow=null;
			foreach (Row r in re)
			{
				startrow = r;			// We just want the first row
				break;
			}

			WorkClass wc = GetValue(rpt);
			double currentValue = _Expr.EvaluateDouble(rpt, row);
			if (row == startrow)
			{
				// restart the group
				wc.Sum = wc.Sum2 = 0;
				wc.Count = 0;
			}
			
			if (currentValue.CompareTo(double.NaN) != 0)
			{
				wc.Sum += currentValue;
				wc.Sum2 += (currentValue*currentValue);
				wc.Count++;
			}

			double result;
			if (wc.Count > 0)
				result = Math.Sqrt((wc.Count * wc.Sum2 - wc.Sum*wc.Sum) / (wc.Count * wc.Count));
			else
				result = double.NaN;

			return result;
		}
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			double d = EvaluateDouble(rpt, row);
			if (d.CompareTo(double.NaN) == 0)
				return decimal.MinValue;

			return Convert.ToDecimal(d);
		}

        public int EvaluateInt32(Report rpt, Row row)
        {
            double d = EvaluateDouble(rpt, row);
            if (d.CompareTo(double.NaN) == 0)
                return int.MinValue;

            return Convert.ToInt32(d);
        }

		public string EvaluateString(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return Convert.ToString(result);
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return Convert.ToDateTime(result);
		}
		private WorkClass GetValue(Report rpt)
		{
			WorkClass wc = rpt.Cache.Get(_key) as WorkClass;
			if (wc == null)
			{
				wc = new WorkClass();
				rpt.Cache.Add(_key, wc);
			}
			return wc;
		}

		private void SetValue(Report rpt, WorkClass w)
		{
			rpt.Cache.AddReplace(_key, w);
		}

		#region ICacheData Members

		public void ClearCache(Report rpt)
		{
			rpt.Cache.Remove(_key);
		}

		#endregion

		class WorkClass
		{
			public double Sum;		
			public double Sum2;		
			public int Count;			
			public WorkClass()
			{
				Sum = Sum2 = 0;
				Count = -1;
			}
		}
	}
}
