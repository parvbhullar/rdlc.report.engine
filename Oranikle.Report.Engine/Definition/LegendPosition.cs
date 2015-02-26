/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Handle Legend position enumeration: TopLeft, LeftTop, ...
	///</summary>
	public enum LegendPositionEnum
	{
		TopLeft,
		TopCenter,
		TopRight,
		LeftTop,
		LeftCenter,
		LeftBottom,
		RightTop,
		RightCenter,
		RightBottom,
		BottomRight,
		BottomCenter,
		BottomLeft
	}
	public class LegendPosition
	{
        static public LegendPositionEnum GetStyle(string s)
        {
            return LegendPosition.GetStyle(s, null);
        }
		static public LegendPositionEnum GetStyle(string s, ReportLog rl)
		{
			LegendPositionEnum rs;

			switch (s)
			{		
				case "TopLeft":
					rs = LegendPositionEnum.TopLeft;
					break;
				case "TopCenter":
					rs = LegendPositionEnum.TopCenter;
					break;
				case "TopRight":
					rs = LegendPositionEnum.TopRight;
					break;
				case "LeftTop":
					rs = LegendPositionEnum.LeftTop;
					break;
				case "LeftCenter":
					rs = LegendPositionEnum.LeftCenter;
					break;
				case "LeftBottom":
					rs = LegendPositionEnum.LeftBottom;
					break;
				case "RightTop":
					rs = LegendPositionEnum.RightTop;
					break;
				case "RightCenter":
					rs = LegendPositionEnum.RightCenter;
					break;
				case "RightBottom":
					rs = LegendPositionEnum.RightBottom;
					break;
				case "BottomRight":
					rs = LegendPositionEnum.BottomRight;
					break;
				case "BottomCenter":
					rs = LegendPositionEnum.BottomCenter;
					break;
				case "BottomLeft":
					rs = LegendPositionEnum.BottomLeft;
					break;
				default:		
                    if (rl != null)
					    rl.LogError(4, "Unknown LegendPosition '" + s + "'.  RightTop assumed.");
					rs = LegendPositionEnum.RightTop;
					break;
			}
			return rs;
		}
	}

}
