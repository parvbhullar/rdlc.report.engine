/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;


namespace Oranikle.Report.Engine
{
	///<summary>
	/// Type expression
	///</summary>
	public enum ExpressionType
	{
		Variant,			// dynamic at runtime	
		String,				// string
		Integer,			// int
		Boolean,			// true, false
		Color,				// Color
		ReportUnit,			// CSS style absolute Length unit
		URL,				// URL
		Enum,				// result corresponds to an enum, like string
		Language			// language
	}
}
