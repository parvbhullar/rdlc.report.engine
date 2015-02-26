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
	/// Handles calling function in the Code element
	/// </summary>
	[Serializable]
	public class FunctionCode : IExpr
	{
		string _Func;		// function/operator
		IExpr[] _Args;		// arguments 
		TypeCode _ReturnTypeCode;	// the return type
		Type[] _ArgTypes;	// argument types

		/// <summary>
		/// passed ReportClass, function name, and args for evaluation
		/// </summary>
		public FunctionCode(string f, IExpr[] a, TypeCode type) 
		{
			_Func = f;
			_Args = a;
			_ReturnTypeCode = type;

			_ArgTypes = new Type[a.Length];
			int i=0;
			foreach (IExpr ex in a)
			{
				_ArgTypes[i++] = XmlUtil.GetTypeFromTypeCode(ex.GetTypeCode());
			}

		}

		public TypeCode GetTypeCode()
		{
			return _ReturnTypeCode;
		}

		public bool IsConstant()
		{
			return false;		// Can't know what the function does
		}

		public IExpr ConstantOptimization()
		{
			// Do constant optimization on all the arguments
			for (int i=0; i < _Args.GetLength(0); i++)
			{
				IExpr e = (IExpr)_Args[i];
				_Args[i] = e.ConstantOptimization();
			}

			// Can't assume that the function doesn't vary
			//   based on something other than the args e.g. Now()
			return this;
		}

		// Evaluate is for interpretation  (and is relatively slow)
		public object Evaluate(Report rpt, Row row)
		{
			if (rpt == null || rpt.CodeInstance == null)
				return null;

			// get the results
			object[] argResults = new object[_Args.Length];
			int i=0;
			bool bUseArg=true;
            bool bNull = false;
			foreach(IExpr a  in _Args)
			{
				argResults[i] = a.Evaluate(rpt, row);
                if (argResults[i] == null)
                    bNull = true;
				else if (argResults[i].GetType() != _ArgTypes[i])
					bUseArg = false;
				i++;
			}
			Type[] argTypes = bUseArg || bNull? _ArgTypes: Type.GetTypeArray(argResults);

			// We can definitely optimize this by caching some info TODO

			// Get ready to call the function
			Object returnVal;

			object inst = rpt.CodeInstance;
			Type theClassType=inst.GetType();
            MethodInfo mInfo = XmlUtil.GetMethod(theClassType, _Func, argTypes);
            if (mInfo == null)
            {
                throw new Exception(string.Format("{0} method not found in code", _Func));
            }
            returnVal = mInfo.Invoke(inst, argResults);

			return returnVal;
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

		public string Func
		{
			get { return  _Func; }
			set {  _Func = value; }
		}

		public IExpr[] Args
		{
			get { return  _Args; }
			set {  _Args = value; }
		}
	}
}
