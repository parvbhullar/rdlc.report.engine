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
	/// Aggregate function: Variance
	/// </summary>
	[Serializable]
	public class FunctionAggrVar : FunctionAggr, IExpr, ICacheData
	{
		string _key;				// key for cache when scope is dataset we can cache the result
		/// <summary>
		/// Aggregate function: Var = n sum(square(x)) - square((sum(x))) / n(n-1)
		/// Stdev assumes values are a sample of the population of data.  If the data
		/// is the entire representation then use Stdevp.
		/// </summary>
        public FunctionAggrVar(List<ICacheData> dataCache, IExpr e, object scp)
            : base(e, scp) 
		{
			_key = "aggrvar" + Interlocked.Increment(ref Parser.Counter).ToString();
			dataCache.Add(this);
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Report rpt, Row row)
		{
			double d = EvaluateDouble(rpt, row);
			if (d.CompareTo(double.NaN) == 0)
				return null;
			return (object) d;
		}
		
		public double EvaluateDouble(Report rpt, Row row)
		{
			bool bSave=true;
			IEnumerable re = this.GetDataScope(rpt, row, out bSave);
			if (re == null)
				return double.NaN;

			ODouble od = GetValue(rpt);
			if (od != null)
				return od.d;

			double sum=0;
			double sum2=0;
			int count=0;
			double temp;
			foreach (Row r in re)
			{
				temp = _Expr.EvaluateDouble(rpt, r);
				if (temp.CompareTo(double.NaN) != 0)
				{
					sum += temp;
					sum2 += (temp*temp);
					count++;
				}
			}

			double result;
			if (count > 1)
			{
				result = (count * sum2 - sum*sum) / (count * (count-1));
			}
			else
				result = double.NaN;
			if (bSave)
				SetValue(rpt, result);

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
			double result = EvaluateDouble(rpt, row);
			if (result.CompareTo(double.NaN) == 0)
				return null;
			return Convert.ToString(result);
		}

		public DateTime EvaluateDateTime(Report rpt, Row row)
		{
			object result = Evaluate(rpt, row);
			return Convert.ToDateTime(result);
		}
		private ODouble GetValue(Report rpt)
		{
			return rpt.Cache.Get(_key) as ODouble;
		}

		private void SetValue(Report rpt, double d)
		{
			rpt.Cache.AddReplace(_key, new ODouble(d));
		}
		#region ICacheData Members

		public void ClearCache(Report rpt)
		{
			rpt.Cache.Remove(_key);
		}

		#endregion
	}
}
