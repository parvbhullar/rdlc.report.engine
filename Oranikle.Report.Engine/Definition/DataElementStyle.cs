/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;


namespace Oranikle.Report.Engine
{
	///<summary>
	///Indicates whether textboxes should render as elements or attributes.
	///</summary>
	public class DataElementStyle
	{
        static public DataElementStyleEnum GetStyle(string s)
        {
            return GetStyle(s, null);
        }
		static public DataElementStyleEnum GetStyle(string s, ReportLog rl)
		{
			DataElementStyleEnum rs;

			switch (s)
			{		
				case "Auto":
					rs = DataElementStyleEnum.Auto;
					break;
				case "AttributeNormal":
					rs = DataElementStyleEnum.AttributeNormal;
					break;
				case "ElementNormal":
					rs = DataElementStyleEnum.ElementNormal;
					break;
				default:		
                    if (rl != null)
					    rl.LogError(4, "Unknown DataElementStyle '" + s + "'.  AttributeNormal assumed.");
					rs = DataElementStyleEnum.AttributeNormal;
				    break;
			}
			return rs;
		}
	}
}
