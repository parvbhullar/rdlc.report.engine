/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Handle sort direction enumeration: ascending, descending.
	///</summary>
	public enum SortDirectionEnum
	{
		Ascending,
		Descending
	}
	public class SortDirection
	{
		static public SortDirectionEnum GetStyle(string s, ReportLog rl)
		{
			SortDirectionEnum rs;

			switch (s)
			{		
				case "Ascending":
					rs = SortDirectionEnum.Ascending;
					break;
				case "Descending":
					rs = SortDirectionEnum.Descending;
					break;
				default:		
					rl.LogError(4, "Unknown SortDirection '" + s + "'.  Ascending assumed.");
					rs = SortDirectionEnum.Ascending;
					break;
			}
			return rs;
		}
	}

}
