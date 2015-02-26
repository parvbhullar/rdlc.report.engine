/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using Oranikle.Report.Engine;

namespace Oranikle.Report.Engine
{
	/// <summary>
	///Represents the font dictionary used in a pdf page
	/// </summary>
	public class PdfImages
	{
		PdfAnchor pa;
		Hashtable images;
		public PdfImages(PdfAnchor a)
		{
			pa = a;
			images = new Hashtable();
		}

		public Hashtable Images
		{
			get { return images; }
		}

		public string GetPdfImage(PdfPage p, string imgname, int contentRef, ImageFormat imf, byte[] ba, int width, int height)
		{
			PdfImageEntry ie;
			if (imgname != null)
			{
				ie = (PdfImageEntry) images[imgname];
				if (ie != null)
				{
					p.AddResource(ie, contentRef);
					return ie.name;
				}
			}
			else
				imgname = "I" + (images.Count + 1).ToString();
			ie = new PdfImageEntry(pa, p, contentRef, imgname, imf, ba, width, height);
			images.Add(imgname, ie);
			return ie.name;
		}

		/// <summary>
		/// Gets the image entries to be written to the file
		/// </summary>
		/// <returns></returns>
		public byte[] GetImageDict(long filePos,out int size)
		{
			MemoryStream ms=new MemoryStream();
			int s;
			byte[] ba;
			foreach (PdfImageEntry ie in images.Values)
			{
				ObjectList objList=new ObjectList(ie.objectNum,filePos);
				ba = PdfUtility.GetUTF8Bytes(ie.imgDict, out s);
				ms.Write(ba, 0, ba.Length);
				filePos += s;

				ms.Write(ie.ba, 0, ie.ba.Length);		// write out the image
				filePos += ie.ba.Length;

				ba = PdfUtility.GetUTF8Bytes("endstream\r\nendobj\r\n", out s);
				ms.Write(ba, 0, ba.Length);
				filePos += s;
				ie.xref.offsets.Add(objList);
			}
			
			ba = ms.ToArray();
			size = ba.Length;
			return ba;
		}
	}

	/// <summary>
	///Represents a image entry used in a pdf page
	/// </summary>
	public class PdfImageEntry:PdfBase
	{
		public string name;
		public ImageFormat imf;
		public byte[] ba;
		public string imgDict;

		/// <summary>
		/// Create the image Dictionary
		/// </summary>
		public PdfImageEntry(PdfAnchor pa, PdfPage p, int contentRef, string nm, ImageFormat imgf, byte[] im, int width, int height):base(pa)
		{
			name=nm;
			imf = imgf;
			ba=im;

			string filter;
            string colorSpace;
            if (imf == ImageFormat.Jpeg)
            {
                filter = "/DCTDecode";

                switch (JpgParser.GetColorSpace(ref im))
                {   
                    case 0:
                        colorSpace = "/DeviceRGB";
                        break;
                    case 1:
                        colorSpace = "/DeviceGray";
                        break;
                    case 3:
                        colorSpace = "/DeviceRGB";
                        break;
                    case 4:
                        colorSpace = "/DeviceCMYK";
                        break;
                    default:
                        colorSpace = "/DeviceRGB";
                        break;
                }
            }
            else if (imf == ImageFormat.Png)    // TODO: this still doesn't work
            {
                filter = "/FlateDecode /DecodeParms <</Predictor 15 /Colors 3 /BitsPerComponent 8 /Columns 80>>";
                colorSpace = "/DeviceRGB";
            }
            else if (imf == ImageFormat.Gif)    // TODO: this still doesn't work
            {
                filter = "/LZWDecode";
                colorSpace = "/DeviceRGB";
            }
            else
            {
                filter = "";
                colorSpace = "";
            }
			imgDict=string.Format("\r\n{0} 0 obj<</Type/XObject/Subtype /Image /Width {1} /Height {2} /ColorSpace {5} /BitsPerComponent 8 /Length {3} /Filter {4} >>\nstream\n",
				this.objectNum,width, height, ba.Length, filter, colorSpace);
					
			p.AddResource(this, contentRef);
		}

	}
    // JpgParser donated by jonh (see http://www.oranikle.in/forum/viewtopic.php?t=403 )
    public class JpgParser
    {
        public static byte GetColorSpace(ref byte[] jpg)
        {
            try
            {
                if (jpg[0] != 255 && jpg[1] != 216)
                    //Not Jpeg 
                    return 0;

                int jpgLen = jpg.GetLength(0);
                for (int i = 2; i < jpgLen; i++)
                {
                    if (i + 1 < jpgLen && jpg[i] == 0xff && jpg[i + 1] == 0xc0) //S0F0 
                    {
                        return jpg[i + 9];
                    }
                }
            }
            catch { return 0; }
            return 0;
        }
    } 

}
