/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;

namespace Oranikle.Report.Engine
{
	///<summary>
	/// When Query is database SQL; QueryColumn represents actual database column.
	///</summary>
	[Serializable]
	public class QueryColumn
	{
		public int colNum;			// Column # in query select
		public string colName;		// Column name in query
		public TypeCode _colType;	// TypeCode in query

		public QueryColumn(int colnum, string name, TypeCode c)
		{
			colNum = colnum;
            colName = name.TrimEnd('\0');
			_colType = c;
		}

		public TypeCode colType
		{
			// Treat Char as String for queries: <sigh> drivers sometimes confuse char and string types
			//    telling me a type is char but actually returning a string (Mono work around)
			get {return _colType == TypeCode.Char? TypeCode.String: _colType; }
		}
	}
}
