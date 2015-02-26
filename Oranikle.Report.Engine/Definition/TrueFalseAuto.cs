/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;


namespace Oranikle.Report.Engine
{
	///<summary>
	/// Three value state; true, false, auto (dependent on context)
	///</summary>
	public enum TrueFalseAutoEnum
	{
		True,
		False,
		Auto
	}
	
	public class TrueFalseAuto
	{
		static public TrueFalseAutoEnum GetStyle(string s, ReportLog rl)
		{
			TrueFalseAutoEnum rs;

			switch (s)
			{		
				case "True":
					rs = TrueFalseAutoEnum.True;
					break;
				case "False":
					rs = TrueFalseAutoEnum.False;
					break;
				case "Auto":
					rs = TrueFalseAutoEnum.Auto;
					break;
				default:		
					rl.LogError(4, "Unknown True False Auto value of '" + s + "'.  Auto assumed.");
					rs = TrueFalseAutoEnum.Auto;
					break;
			}
			return rs;
		}
	}
}
