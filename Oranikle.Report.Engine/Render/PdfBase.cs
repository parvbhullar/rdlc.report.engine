/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Oranikle.Report.Engine
{
	/// <summary>
	/// This is the base object for all objects used within the pdf.
	/// </summary>
	public class PdfBase
	{
		/// <summary>
		/// Stores the Object Number
		/// </summary>
		public int objectNum;
		public PdfAnchor xref;
		/// <summary>
		/// Constructor increments the object number to 
		/// reflect the currently used object number
		/// </summary>
		protected PdfBase(PdfAnchor pa)
		{
			xref=pa;
			xref.current++;
			objectNum=xref.current;
		}

		public int Current
		{
			get { return xref.current; }
		}
		/// <summary>
		/// Convert the unicode string 16 bits to unicode bytes. 
		/// This is written to the file to create Pdf 
		/// </summary>
		/// <returns></returns>
		public byte[] GetUTF8Bytes(string str,long filePos,out int size)
		{
			ObjectList objList=new ObjectList(objectNum,filePos);
            //byte []abuf;

            //byte[] ubuf = Encoding.Unicode.GetBytes(str);
            //Encoding enc = Encoding.GetEncoding(1252);
            //abuf = Encoding.Convert(Encoding.Unicode, enc, ubuf);
            byte[] ubuf = Encoding.Unicode.GetBytes(str);
            Encoding enc = Encoding.GetEncoding(65001); // utf-8
            byte[] abuf = Encoding.Convert(Encoding.Unicode, enc, ubuf);

			size=abuf.Length;
			xref.offsets.Add(objList);

            return abuf;
		}

	}

	/// <summary>
	/// Holds the Byte offsets of the objects used in the Pdf Document
	/// </summary>
	public class PdfAnchor
	{
        public List<ObjectList> offsets;
		public int current;
		public bool CanCompress;
		
		public PdfAnchor(bool bCompress)
		{
            offsets = new List<ObjectList>();
			current=0;
			CanCompress = bCompress;
		}

		public void Reset()
		{
			offsets.Clear();
			current=0;
		}
	}

	/// <summary>
	/// For Adding the Object number and file offset
	/// </summary>
	public class ObjectList:IComparable
	{
		public long offset;
		public int objNum;

		public ObjectList(int objectNum,long fileOffset)
		{
			offset=fileOffset;
			objNum=objectNum;
		}
		#region IComparable Members

		public int CompareTo(object obj)
		{

			int result=0;
			result=(this.objNum.CompareTo(((ObjectList)obj).objNum));
			return result;
		}

		#endregion
	}
}
