/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.IO;
using Oranikle.Report.Engine;


namespace Oranikle.Report.Engine
{
	/// <summary>
	/// The IExpr interface should be implemented when you want to create a built-in function.
	/// </summary>
	public interface IExpr
	{
		TypeCode GetTypeCode();			// return the type of the expression
		bool IsConstant();				// expression returns a constant
		IExpr ConstantOptimization();	// constant optimization

		// Evaluate is for interpretation
		object Evaluate(Report r, Row row);				// return an object
		string EvaluateString(Report r, Row row);		// return a string
		double EvaluateDouble(Report r, Row row);		// return a double
		decimal EvaluateDecimal(Report r, Row row);		// return a decimal
        int EvaluateInt32(Report r, Row row);           // return an Int32
		DateTime EvaluateDateTime(Report r, Row row);	// return a DateTime
		bool EvaluateBoolean(Report r, Row row);		// return boolean
	}
}
