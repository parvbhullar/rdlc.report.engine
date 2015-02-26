/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Handle title position enumeration: center, near, far.
	///</summary>
	public enum TitlePositionEnum
	{
		Center,
		Near,
		Far
	}
	public class TitlePosition
	{
		static public TitlePositionEnum GetStyle(string s, ReportLog rl)
		{
			TitlePositionEnum rs;

			switch (s)
			{		
				case "Center":
					rs = TitlePositionEnum.Center;
					break;
				case "Near":
					rs = TitlePositionEnum.Near;
					break;
				case "Far":
					rs = TitlePositionEnum.Far;
					break;
				default:	
					rl.LogError(4, "Unknown TitlePosition '" + s + "'.  Center assumed.");
					rs = TitlePositionEnum.Center;
					break;
			}
			return rs;
		}
	}

}
