/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using Oranikle.Report.Engine;

namespace Oranikle.Report.Engine
{
    /// <summary>
	///Represents the outline dictionary
	/// </summary>
    public class PdfOutline
	{
        PdfAnchor _pa;
        List<PdfOutlineEntry> bookmarks;        // lists of outline entries (ie bookmarks)
        PdfOutlineMain _pom = null;
        
        public PdfOutline(PdfAnchor pa)
		{
            _pa = pa;
            bookmarks = new List<PdfOutlineEntry>();
		}

		public List<PdfOutlineEntry> Bookmarks
		{
			get { return bookmarks; }
		}

        public int GetObjectNumber()
        {
            if (this.bookmarks.Count == 0)
                return -1;
            _pom = new PdfOutlineMain(_pa);
            return _pom.objectNum;
        }

		/// <summary>
		/// Gets the outline entries to be written to the file
		/// </summary>
		/// <returns></returns>
		public byte[] GetOutlineDict(long filePos,out int size)
		{
			MemoryStream ms=new MemoryStream();
			int s;
			byte[] ba;

//23 0 obj
//<</Type /Outlines /First 13 0 R
///Last 13 0 R>>
//endobj

            if (_pom == null)
                throw new Exception("GetObjectNumber must be called before GetOutlineDict");

			string content=string.Format("\r\n{0} 0 obj<</Type /Outlines /First {1} 0 R\r/Last {2} 0 R\r/Count {3}>>\rendobj\r",
					_pom.objectNum,
                    bookmarks[0].objectNum, 
                    bookmarks[bookmarks.Count-1].objectNum, 
                    bookmarks.Count);

            ba = _pom.GetUTF8Bytes(content, filePos, out s);
            ms.Write(ba, 0, ba.Length);
            filePos += s;

            string parent = string.Format("\r/Parent {0} 0 R", _pom.objectNum);
            
            PdfOutlineEntry coe;        // current outline entry
            PdfOutlineEntry poe;        // previous outline entry
            PdfOutlineEntry noe;        // previous outline entry
            for (int i = 0; i < bookmarks.Count; i++)
            {
                coe = bookmarks[i];
                poe = i > 0? bookmarks[i-1]: null;
                noe = i + 1 == bookmarks.Count ? null : bookmarks[i + 1];
                
                StringBuilder sb = new StringBuilder();
// <</Title (Yahoo! News: Business)
                string newtext = coe.Text.Replace("\\", "\\\\");
                newtext = newtext.Replace("(", "\\(");
                newtext = newtext.Replace(")", "\\)");
                newtext = newtext.Replace('\r', ' ');
                newtext = newtext.Replace('\n', ' ');
                sb.AppendFormat("\r\n{0} 0 obj<</Title ({1})", coe.objectNum, newtext);
// /Parent 23 0 R
                sb.Append(parent); 
// /Prev 14 0 R
                if (poe != null)
                    sb.AppendFormat("\r/Prev {0} 0 R", poe.objectNum);
// /Next 16 0 R
                if (noe != null)
                    sb.AppendFormat("\r/Next {0} 0 R", noe.objectNum);
// /Dest [3 0 R /XYZ 0 400.86 null]
                sb.AppendFormat("\r/Dest [{0} 0 R /XYZ {1} {2} null]", coe.P, coe.X, coe.Y);             

                sb.Append("\r>>\rendobj\r");
                ba = coe.GetUTF8Bytes(sb.ToString(), filePos, out s);
                ms.Write(ba, 0, ba.Length);
                filePos += s;
            }
			
			ba = ms.ToArray();
			size = ba.Length;
			return ba;
		}
	}
    
    public class PdfOutlineMain : PdfBase
    {
        public PdfOutlineMain(PdfAnchor pa)
            : base(pa)
        {
        }
    }

	/// <summary>
	///Represents a outline entry
	/// </summary>
	public class PdfOutlineEntry:PdfBase
	{
        int _p;                 // page object number
        string _text;           // text for bookmark
        float _x;               // x location on page
        float _y;               // y location on page

		/// <summary>
		/// Create the image Dictionary
		/// </summary>
		public PdfOutlineEntry(PdfAnchor pa, int p, string text, float x, float y ):base(pa)
		{
            _p = p;
            _x = x;
            _y = y;
            _text = text;
		}

        public int P { get { return _p; } }
        public string Text { get { return _text; } }
        public float X { get { return _x; } }
        public float Y { get { return _y; } }
	}
}
