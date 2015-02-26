/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// AxisTickMarks definition and processing.
	///</summary>
	public enum AxisTickMarksEnum
	{
		None,
		Inside,
		Outside,
		Cross
	}

	public class AxisTickMarks
	{
        static public AxisTickMarksEnum GetStyle(string s)
        {
            return AxisTickMarks.GetStyle(s, null);
        }

		static public AxisTickMarksEnum GetStyle(string s, ReportLog rl)
		{
			AxisTickMarksEnum rs;

			switch (s)
			{		
				case "None":
					rs = AxisTickMarksEnum.None;
					break;
				case "Inside":
					rs = AxisTickMarksEnum.Inside;
					break;
				case "Outside":
					rs = AxisTickMarksEnum.Outside;
					break;
				case "Cross":
					rs = AxisTickMarksEnum.Cross;
					break;
				default:		
                    if (rl != null)
					    rl.LogError(4, "Unknown Axis Tick Mark '" + s + "'.  None assumed.");
					rs = AxisTickMarksEnum.None;
					break;
			}
			return rs;
		}
	}

}
