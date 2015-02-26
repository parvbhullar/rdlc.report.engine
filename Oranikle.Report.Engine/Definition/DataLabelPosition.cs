/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;


namespace Oranikle.Report.Engine
{
	
	public enum DataLabelPositionEnum
	{
		Auto,
		Top,
		TopLeft,
		TopRight,
		Left,
		Center,
		Right,
		BottomRight,
		Bottom,
		BottomLeft
	}

	public class DataLabelPosition
	{
		static public DataLabelPositionEnum GetStyle(string s, ReportLog rl)
		{
			DataLabelPositionEnum dlp;

			switch (s)
			{		
				case "Auto":
					dlp = DataLabelPositionEnum.Auto;
					break;
				case "Top":
					dlp = DataLabelPositionEnum.Top;
					break;
				case "TopLeft":
					dlp = DataLabelPositionEnum.TopLeft;
					break;
				case "TopRight":
					dlp = DataLabelPositionEnum.TopRight;
					break;
				case "Left":
					dlp = DataLabelPositionEnum.Left;
					break;
				case "Center":
					dlp = DataLabelPositionEnum.Center;
					break;
				case "Right":
					dlp = DataLabelPositionEnum.Right;
					break;
				case "BottomRight":
					dlp = DataLabelPositionEnum.BottomRight;
					break;
				case "Bottom":
					dlp = DataLabelPositionEnum.Bottom;
					break;
				case "BottomLeft":
					dlp = DataLabelPositionEnum.BottomLeft;
					break;
				default:		
					rl.LogError(4, "Unknown DataLablePosition '" + s + "'.  Auto assumed.");
					dlp = DataLabelPositionEnum.Auto;
					break;
			}
			return dlp;
		}
	}


}
