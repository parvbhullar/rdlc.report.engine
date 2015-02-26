/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;

namespace Oranikle.Report.Engine
{
	/// <summary>
	/// Token class that used by LangParser.
	/// </summary>
	public class Token
	{
		public string Value;
		public int StartLine;
		public int EndLine;
		public int StartCol;
		public int EndCol;
		public TokenTypes Type;

		/// <summary>
		/// Initializes a new instance of the Token class.
		/// </summary>
		public Token(string value, int startLine, int startCol, int endLine, int endCol, TokenTypes type)
		{
			Value = value;
			StartLine = startLine;
			EndLine = endLine;
			StartCol = startCol;
			EndCol = endCol;
			Type = type;
		}

		/// <summary>
		/// Initializes a new instance of the Token class.
		/// </summary>
		public Token(string value, TokenTypes type)
			: this(value, 0, 0, 0, 0, type)
		{
			// use this
		}

		/// <summary>
		/// Initializes a new instance of the Token class.
		/// </summary>
		public Token(TokenTypes type)
			: this(null, 0, 0, 0, 0, type)
		{
			// use this
		}

		/// <summary>
		/// Returns a string representation of the Token.
		/// </summary>
		public override string ToString()
		{
			return "<" + Type + "> " + Value;	
		}
	}
}
