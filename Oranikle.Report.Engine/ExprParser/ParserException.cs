/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;

namespace Oranikle.Report.Engine
{
	/// <summary>
	/// Represents an exception throwed by lexer and parser.
	/// </summary>
	public class ParserException : ApplicationException
	{
		/// <summary>
		/// Initializes a new instance of the ParserException class with the
		/// specified message.
		/// </summary>
		/// <param name="message">A message.</param>
		public ParserException(string message) : base(message)
		{
			// used base
		}
	}
}
