/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.IO;

namespace Oranikle.Report.Engine
{
	/// <summary>
	///Represents the general content stream in a Pdf Page. 
	///This is used only by the PageObjec 
	/// </summary>
	public class PdfContent:PdfBase
	{
		private string content;
		private string contentStream;
		private bool CanCompress;
		public PdfContent(PdfAnchor pa):base(pa)
		{
			CanCompress = pa.CanCompress;
			content=null;
//			contentStream="%stream\r";
			contentStream="";
		}
		/// <summary>
		/// Set the Stream of this Content Dict.
		/// Stream is taken from PdfElements Objects
		/// </summary>
		/// <param name="stream"></param>
		public void SetStream(string stream)
		{
			if (stream == null)
				return;
			contentStream+=stream;
		}
		/// <summary>
		/// Content object
		/// </summary>
		/// <summary>
		/// Get the Content Dictionary
		/// </summary>
		public byte[] GetContentDict(long filePos,out int size)
		{	
			// When no compression
			if (!CanCompress)
			{
				content=string.Format("\r\n{0} 0 obj<</Length {1}>>stream\r{2}\rendstream\rendobj\r",
					this.objectNum,contentStream.Length,contentStream);

				return GetUTF8Bytes(content,filePos,out size);
			}

			// Try to use compression; could still fail in which case fall back to uncompressed
			Stream strm=null;
			MemoryStream cs=null;
			try
			{
				CompressionConfig cc = RdlEngineConfig.GetCompression();
				cs = new MemoryStream();	// this will contain the content stream
				if (cc != null)
					strm = cc.GetStream(cs);

				if (strm == null)
				{	// can't compress string
					cs.Close();		

					content=string.Format("\r\n{0} 0 obj<</Length {1}>>stream\r{2}\rendstream\rendobj\r",
						this.objectNum,contentStream.Length,contentStream);

					return GetUTF8Bytes(content,filePos,out size);
				}

				// Compress the contents
				int cssize;
				byte[] ca = PdfUtility.GetUTF8Bytes(contentStream,out cssize);
				strm.Write(ca, 0, cssize);
				strm.Flush();
				cc.CallStreamFinish(strm);

				// Now output the PDF command
				MemoryStream ms=new MemoryStream();
				int s;
				byte[] ba;

				// get the compressed data;  we need the lenght now
				byte[] cmpData = cc.GetArray(cs);

				// write the beginning portion of the PDF object
				string ws = string.Format("\r\n{0} 0 obj<< /Filter /FlateDecode /Length {1}>>stream\r",
					this.objectNum, cmpData.Length);

				ba = GetUTF8Bytes(ws,filePos,out s);	// this will also register the object
				ms.Write(ba, 0, ba.Length);
				filePos += s;

				// write the Compressed data
				ms.Write(cmpData, 0, cmpData.Length);
				filePos += ba.Length;

				// write the end portion of the PDF object
				ba = PdfUtility.GetUTF8Bytes("\rendstream\rendobj\r", out s);
				ms.Write(ba, 0, ba.Length);
				filePos += s;

				// now the final output array
				ba = ms.ToArray();
				size = ba.Length;
				return ba;

			}
			finally
			{
				if (strm != null)
					strm.Close();
			}
		}
	}

}
