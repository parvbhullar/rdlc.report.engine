/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.IO;
using Oranikle.Report.Engine;

namespace Oranikle.Report.Engine
{
	/// <summary>
	///Represents the font dictionary used in a pdf page
	/// </summary>
	public class PdfFonts
	{
		PdfAnchor pa;
		Hashtable fonts;
		public PdfFonts(PdfAnchor a)
		{
			pa = a;
			fonts = new Hashtable();
		}

		public Hashtable Fonts
		{
			get { return fonts; }
		}

		public string GetPdfFont(string facename)
		{
			PdfFontEntry fe = (PdfFontEntry) fonts[facename];
			if (fe != null)
				return fe.font;

			string name = "F" + (fonts.Count + 1).ToString();
			fe = new PdfFontEntry(pa, name, facename);
			fonts.Add(facename, fe);
			return fe.font;
		}

		public string GetPdfFont(StyleInfo si)
		{
			string face = FontNameNormalize(si.FontFamily);
            if (face == "Times-Roman")
            {
                if (si.IsFontBold() && si.FontStyle == FontStyleEnum.Italic)
                    face = "Times-BoldItalic";
                else if (si.IsFontBold())
                    face = "Times-Bold";
                else if (si.FontStyle == FontStyleEnum.Italic)
                    face = "Times-Italic";
            }
			else if (si.IsFontBold() && 
				si.FontStyle == FontStyleEnum.Italic)	// bold and italic?
				face = face + "-BoldOblique";
			else if (si.IsFontBold())			// just bold?
				face = face + "-Bold";
			else if (si.FontStyle == FontStyleEnum.Italic)
				face = face + "-Oblique";

			return GetPdfFont(face);
		}
		
		public string FontNameNormalize(string face)
		{
			string faceName;
			switch (face.ToLower())
			{
                case "times":
                case "times-roman":
                case "times roman":
                case "timesnewroman":
				case "times new roman":
				case "timesnewromanps":
				case "timesnewromanpsmt":
				case "serif":
					faceName = "Times-Roman";
					break;
				case "helvetica":
				case "arial":
				case "arialmt":
				case "sans-serif":
                case "sans serif":
                default:
					faceName = "Helvetica";
					break;
				case "courier":
				case "couriernew":
				case "courier new":
				case "couriernewpsmt":
				case "monospace":
					faceName = "Courier";
					break;
				case "symbol":
					faceName = "Symbol";
					break;
				case "zapfdingbats":
                case "wingdings":
                case "wingding":
                    faceName = "ZapfDingbats";
					break;
			}
			return faceName;
		}
		/// <summary>
		/// Gets the font entries to be written to the file
		/// </summary>
		/// <returns></returns>
		public byte[] GetFontDict(long filePos,out int size)
		{
			MemoryStream ms=new MemoryStream();
			int s;
			byte[] ba;
			foreach (PdfFontEntry fe in fonts.Values)
			{
				ba = fe.GetUTF8Bytes(fe.fontDict, filePos, out s);
				filePos += s;
				ms.Write(ba, 0, ba.Length);
			}
			
			ba = ms.ToArray();
			size = ba.Length;
			return ba;
		}
	}

	/// <summary>
	///Represents a font entry used in a pdf page
	/// </summary>
	public class PdfFontEntry:PdfBase
	{
		public string fontDict;
		public string font;

		/// <summary>
		/// Create the font Dictionary
		/// </summary>
		public PdfFontEntry(PdfAnchor pa,string fontName,string fontFace):base(pa)
		{
			font=fontName;
            fontDict = string.Format("\r\n{0} 0 obj<</Type/Font/Name /{1}/BaseFont/{2}/Subtype/Type1/Encoding /WinAnsiEncoding>>\tendobj\t",
                this.objectNum, fontName, fontFace);
            //fontDict = string.Format("\r\n{0} 0 obj<</Type/Font/Name /{1}/BaseFont/{2}/Subtype/Type1>>\tendobj\t",
            //    this.objectNum, fontName, fontFace);
        }
		/// <summary>
		/// Get the font entry to be written to the file
		/// </summary>
		/// <returns></returns>
		public byte[] GetFontDict(long filePos,out int size)
		{
			return this.GetUTF8Bytes(fontDict,filePos,out size);
		}

	}
}
