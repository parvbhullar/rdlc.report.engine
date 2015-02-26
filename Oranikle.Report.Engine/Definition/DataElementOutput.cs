/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;


namespace Oranikle.Report.Engine
{
	///<summary>
	/// Filter operators
	///</summary>
	public enum DataElementOutputEnum
	{
		Output,		// Indicates the item should appear in the output
		NoOutput,	// Indicates the item should not appear in the output
		ContentsOnly,	// Indicates the item should not appear in the XML, but its contents should be
						// rendered as if they were in this item’s
						// container. Only applies to Lists.
		Auto		// (Default): Will behave as NoOutput for
					// Textboxes with constant values,
					// ContentsOnly for Rectangles and Output for
					// all other items		

	}

	public class DataElementOutput
	{
        static public DataElementOutputEnum GetStyle(string s)
        {
            return GetStyle(s, null);
        }

		static public DataElementOutputEnum GetStyle(string s, ReportLog rl)
		{
			DataElementOutputEnum rs;

			switch (s)
			{		
				case "Output":
					rs = DataElementOutputEnum.Output;
					break;
				case "NoOutput":
					rs = DataElementOutputEnum.NoOutput;
					break;
				case "ContentsOnly":
					rs = DataElementOutputEnum.ContentsOnly;
					break;
				case "Auto":
					rs = DataElementOutputEnum.Auto;
					break;
				default:		
                    if (rl != null)
					    rl.LogError(4, "Unknown DataElementOutput '" + s + "'.  Auto assumed.");
					rs = DataElementOutputEnum.Auto;
					break;
			}
			return rs;
		}
	}

}
