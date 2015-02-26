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
	/// Aggregate function: Count
	/// </summary>
	[Serializable]
	public class FunctionAggrArray : FunctionAggr, IExpr, ICacheData
	{
		string _key;
		/// <summary>
		/// Aggregate function: Count
		/// 
		///	Return type is double
		/// </summary>
        public FunctionAggrArray(List<ICacheData> dataCache, IExpr e, object scp)
            : base(e, scp) 
		{
			_key = "aggrarray" + Interlocked.Increment(ref Parser.Counter).ToString();
			dataCache.Add(this);
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Object;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Report rpt, Row row)
		{
            bool bSave = true;
            RowEnumerable re = this.GetDataScope(rpt, row, out bSave);
            if (re == null)
                return null;

            object v = GetValue(rpt);
            if (v == null)
            {
                object temp;
                ArrayList ar = new ArrayList(Math.Max(1,re.LastRow - re.FirstRow + 1));
                foreach (Row r in re)
                {
                    temp = _Expr.Evaluate(rpt, r);
                    ar.Add(temp);
                }

                v = ar;
                if (bSave)
                    SetValue(rpt, v);
            }
            return v;
        }
		
		public double EvaluateDouble(Report rpt, Row row)
		{
			return double.MinValue;
		}
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			return decimal.MinValue;
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

		private object GetValue(Report rpt)
		{
			object oi = rpt.Cache.Get(_key);
			return oi;
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
