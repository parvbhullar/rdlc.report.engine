/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// Handle Matrix layout direction enumeration: LTR (left to right), RTL (right to left)
	///</summary>
	public enum MatrixLayoutDirectionEnum
	{
		LTR,				// Left to Right
		RTL					// Right to Left
	}

	public class MatrixLayoutDirection
	{
		static public MatrixLayoutDirectionEnum GetStyle(string s, ReportLog rl)
		{
			MatrixLayoutDirectionEnum rs;

			switch (s)
			{		
				case "LTR":
				case "LeftToRight":
					rs = MatrixLayoutDirectionEnum.LTR;
					break;
				case "RTL":
				case "RightToLeft":
					rs = MatrixLayoutDirectionEnum.RTL;
					break;
				default:		
					rl.LogError(4, "Unknown MatrixLayoutDirection '" + s + "'.  LTR assumed.");
					rs = MatrixLayoutDirectionEnum.LTR;
					break;
			}
			return rs;
		}
	}

}
