/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using Oranikle.Report.Engine;


namespace Oranikle.Report.Engine
{
	/// <summary>
	/// If function caches data then this must be implemented and constructor must place in 
	///    master report report
	/// </summary>
	public interface ICacheData
	{
		void ClearCache(Report rpt);			// clear out cache of data: new data is coming
	}
}
