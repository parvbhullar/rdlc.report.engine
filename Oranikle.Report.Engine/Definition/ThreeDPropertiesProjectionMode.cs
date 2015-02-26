/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;


namespace Oranikle.Report.Engine
{
	public enum ThreeDPropertiesProjectionModeEnum
	{
		Perspective,
		Orthographic
	}

	public class ThreeDPropertiesProjectionMode
	{
		static public ThreeDPropertiesProjectionModeEnum GetStyle(string s)
		{
			ThreeDPropertiesProjectionModeEnum pm;

			switch (s)
			{		
				case "Perspective":
					pm = ThreeDPropertiesProjectionModeEnum.Perspective;
					break;
				case "Orthographic":
					pm = ThreeDPropertiesProjectionModeEnum.Orthographic;
					break;
				default:
					pm = ThreeDPropertiesProjectionModeEnum.Perspective;
					break;
			}
			return pm;
		}
	}


}
