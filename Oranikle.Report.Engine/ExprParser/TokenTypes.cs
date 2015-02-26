/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;

namespace Oranikle.Report.Engine
{
	/// <summary>
	/// Summary description for TokenTypes.
	/// </summary>
	public enum TokenTypes
	{
		AND,
		OR,
		NOT,
		PLUS,
		PLUSSTRING,
		MINUS,
		LPAREN,
		RPAREN,
		QUOTE,
		IDENTIFIER,
		COMMA,
		NUMBER,
		DATETIME,
		DOUBLE,
		INTEGER,
		EQUAL,
		NOTEQUAL,
		GREATERTHAN,
		GREATERTHANOREQUAL,
		LESSTHAN,
		LESSTHANOREQUAL,
		FORWARDSLASH,
		BACKSLASH,
		STAR,
		EXP,
		MODULUS,
        DOT,                // dot operator
		OTHER,
		EOF
	}
}
