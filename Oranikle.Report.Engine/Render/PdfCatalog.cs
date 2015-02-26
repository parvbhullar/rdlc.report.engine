/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Diagnostics;

namespace Oranikle.Report.Engine
{
	/// <summary>
	/// Models the Catalog dictionary within a pdf file. This is the first created object. 
	/// It contains references to all other objects within the List of Pdf Objects.
	/// </summary>
	public class PdfCatalog:PdfBase
	{
		private string catalog;
		private string lang;
		public PdfCatalog(PdfAnchor pa, string l):base(pa)
		{
			if (l != null)
				lang = String.Format("/Lang({0})", l);
			else
				lang = "";
		}
		/// <summary>
		///Returns the Catalog Dictionary 
		/// </summary>
		/// <returns></returns>
		public byte[] GetCatalogDict(int outline, int refPageTree,long filePos,out int size)
		{
			Debug.Assert(refPageTree >= 1);
			
            if (outline >= 0)
                catalog=string.Format("\r\n{0} 0 obj<</Type /Catalog{2}/Pages {1} 0 R /Outlines {3} 0 R>>\tendobj\t",
				    this.objectNum,refPageTree, lang, outline);
            else
                catalog = string.Format("\r\n{0} 0 obj<</Type /Catalog{2}/Pages {1} 0 R>>\tendobj\t",
                    this.objectNum, refPageTree, lang);
            
            return this.GetUTF8Bytes(catalog, filePos, out size);
		}

	}
}
