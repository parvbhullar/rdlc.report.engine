/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;


namespace Oranikle.Report.Engine
{

	public enum ThreeDPropertiesShadingEnum
	{
		None,
		Simple,
		Real
	}

	public class ThreeDPropertiesShading
	{
		static public ThreeDPropertiesShadingEnum GetStyle(string s, ReportLog rl)
		{
			ThreeDPropertiesShadingEnum sh;

			switch (s)
			{		
				case "None":
					sh = ThreeDPropertiesShadingEnum.None;
					break;
				case "Simple":
					sh = ThreeDPropertiesShadingEnum.Simple;
					break;
				case "Real":
					sh = ThreeDPropertiesShadingEnum.Real;
					break;
				default:	
					rl.LogError(4, "Unknown Shading '" + s + "'.  None assumed.");
					sh = ThreeDPropertiesShadingEnum.None;
					break;
			}
			return sh;
		}
	}


}
