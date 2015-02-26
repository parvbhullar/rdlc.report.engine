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
	/// Aggregate function: CountDistinct
	/// </summary>
	[Serializable]
	public class FunctionAggrCountDistinct : FunctionAggr, IExpr, ICacheData
	{
		string _key;			// key used for caching value
		/// <summary>
		/// Aggregate function: CountDistinct
		/// 
		///	Return type is double
		/// </summary>
        public FunctionAggrCountDistinct(List<ICacheData> dataCache, IExpr e, object scp)
            : base(e, scp) 
		{
			_key = "countdistinct" + Interlocked.Increment(ref Parser.Counter).ToString();

			dataCache.Add(this);
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Int32;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Report rpt, Row row)
		{
			return (object) EvaluateDouble(rpt, row);
		}
		
		public int EvaluateInt32(Report rpt, Row row)
		{
			bool bSave=true;
			RowEnumerable re = this.GetDataScope(rpt, row, out bSave);
			if (re == null)
				return 0;

			int v = GetValue(rpt);
			if (v < 0)
			{
				object temp;
				int count = Math.Max(2, re.LastRow - re.FirstRow);
				Hashtable ht = new Hashtable(count);
				foreach (Row r in re)
				{
					temp = _Expr.Evaluate(rpt, r);
					if (temp != null)
					{
						object o = ht[temp];	// search for it
						if (o == null)			// if not found; add it to the hash table
						{
							ht.Add(temp, temp);
						}
					}
				}
				v = ht.Count;
				if (bSave)
					SetValue(rpt, v);
			}
			return  v;
		}

        public double EvaluateDouble(Report rpt, Row row)
        {
            int d = EvaluateInt32(rpt, row);

            return Convert.ToDouble(d);
        }
		
		public decimal EvaluateDecimal(Report rpt, Row row)
		{
			int d = EvaluateInt32(rpt, row);

			return Convert.ToDecimal(d);
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

		private int GetValue(Report rpt)
		{
			OInt oi = rpt.Cache.Get(_key) as OInt;
			return oi == null? -1: oi.i;
		}

		private void SetValue(Report rpt, int i)
		{
			rpt.Cache.AddReplace(_key, new OInt(i));
		}

		#region ICacheData Members

		public void ClearCache(Report rpt)
		{
			rpt.Cache.Remove(_key);
		}

		#endregion
	}
}
