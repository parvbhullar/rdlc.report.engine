/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// ChartElementOutput parsing.
	///</summary>
	public enum ChartElementOutputEnum
	{
		Output,
		NoOutput
	}

	public class ChartElementOutput
	{
		static public ChartElementOutputEnum GetStyle(string s, ReportLog rl)
		{
			ChartElementOutputEnum ceo;

			switch (s)
			{		
				case "Output":
					ceo = ChartElementOutputEnum.Output;
					break;
				case "NoOutput":
					ceo = ChartElementOutputEnum.NoOutput;
					break;
				default:		
					rl.LogError(4, "Unknown ChartElementOutput '" + s + "'.  Output assumed.");
					ceo = ChartElementOutputEnum.Output;
					break;
			}
			return ceo;
		}
	}


}
