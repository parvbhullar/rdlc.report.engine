/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Xml;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// A report object name.   CLS comliant identifier.
	///</summary>
	[Serializable]
	public class Name
	{
		string _Name;			// name CLS compliant identifier; www.unicode.org/unicode/reports/tr15/tr15-18.html
	
		public Name(string name)
		{
			_Name=name;
		}

		public string Nm
		{
			get { return  _Name; }
			set {  _Name = value; }
		}

		public override string ToString()
		{
			return _Name;
		}
	}
}
