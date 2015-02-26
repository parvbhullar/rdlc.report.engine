/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;


namespace Oranikle.Report.Engine
{
	///<summary>
	/// Style Background image source enumeration
	///</summary>

	public enum StyleBackgroundImageSourceEnum
	{
		External,		// The Value contains a constant or
		// expression that evaluates to for the location
		// of the image
		Embedded,		// The Value contains a constant
		// or expression that evaluates to the name of
		// an EmbeddedImage within the report
		Database,		// The Value contains an expression
		// (a field in the database) that evaluates to the
		// binary data for the image.
		Unknown			// Illegal (or no) value specified
	}

	public class StyleBackgroundImageSource
	{
		static public StyleBackgroundImageSourceEnum GetStyle(string s)
		{
			StyleBackgroundImageSourceEnum rs;

			switch (s)
			{		
				case "External":
					rs = StyleBackgroundImageSourceEnum.External;
					break;
				case "Embedded":
					rs = StyleBackgroundImageSourceEnum.Embedded;
					break;
				case "Database":
					rs = StyleBackgroundImageSourceEnum.Database;
					break;
				default:		// user error just force to normal TODO
					rs = StyleBackgroundImageSourceEnum.External;
					break;
			}
			return rs;
		}
	}

}
