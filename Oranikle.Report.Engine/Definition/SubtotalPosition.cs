/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Handle the matrix subtotal position: before, after
	///</summary>
	public enum SubtotalPositionEnum
	{
		Before,			// left/above
		After			// right/below

	}

	public class SubtotalPosition
	{
		static public SubtotalPositionEnum GetStyle(string s, ReportLog rl)
		{
			SubtotalPositionEnum rs;

			switch (s)
			{		
				case "Before":
					rs = SubtotalPositionEnum.Before;
					break;
				case "After":
					rs = SubtotalPositionEnum.After;
					break;
				default:		
					rl.LogError(4, "Unknown SubtotalPosition '" + s + "'.  Before assumed.");
					rs = SubtotalPositionEnum.Before;
					break;
			}
			return rs;
		}
	}

}
