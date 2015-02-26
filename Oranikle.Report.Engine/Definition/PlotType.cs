/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;


namespace Oranikle.Report.Engine
{
	
	public enum PlotTypeEnum
	{
		Auto,
		Line
	}

	public class PlotType
	{
		static public PlotTypeEnum GetStyle(string s, ReportLog rl)
		{
			PlotTypeEnum pt;

			switch (s)
			{		
				case "Auto":
					pt = PlotTypeEnum.Auto;
					break;
				case "Line":
					pt = PlotTypeEnum.Line;
					break;
				default:		
					rl.LogError(4, "Unknown PlotType '" + s + "'.  Auto assumed.");
					pt = PlotTypeEnum.Auto;
					break;
			}
			return pt;
		}
	}


}
