/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;

namespace Oranikle.Report.Engine
{
	/// <summary>
	///Store information about the document,Title, Author, Company, 
	/// </summary>
	public class PdfInfo:PdfBase
	{
		private string info;
		public PdfInfo(PdfAnchor pa):base(pa)
		{
			info=null;
		}
		/// <summary>
		/// Fill the Info Dict
		/// </summary>
		public void SetInfo(string title,string author,string subject, string company)
		{
			info=string.Format("\r\n{0} 0 obj<</ModDate({1})/CreationDate({1})/Title({2})/Creator(Oranikle Venture Inc.)"+
				"/Author({3})/Subject ({4})/Producer(Oranikle Venture Inc.)/Company({5})>>\tendobj\t",
				this.objectNum,
				GetDateTime(),
				title==null?"":title,
				author==null?"":author,
				subject==null?"":subject,
				company==null?"":company);

		}
		/// <summary>
		/// Get the Document Information Dictionary
		/// </summary>
		/// <returns></returns>
		public byte[] GetInfoDict(long filePos,out int size)
		{
			return GetUTF8Bytes(info,filePos,out size);
		}
		/// <summary>
		/// Get Date as Adobe needs ie similar to ISO/IEC 8824 format
		/// </summary>
		/// <returns></returns>
		private string GetDateTime()
		{
			DateTime universalDate=DateTime.UtcNow;
			DateTime localDate=DateTime.Now;
			string pdfDate=string.Format("D:{0:yyyyMMddhhmmss}", localDate);
			TimeSpan diff=localDate.Subtract(universalDate);
			int uHour=diff.Hours;
			int uMinute=diff.Minutes;
			char sign='+';
			if(uHour<0)
				sign='-';
			uHour=Math.Abs(uHour);
			pdfDate+=string.Format("{0}{1}'{2}'",sign,uHour.ToString().PadLeft(2,'0'),uMinute.ToString().PadLeft(2,'0'));
			return pdfDate;
		}

	}
}
