/* ====================================================================
   

   
	
   
   
   

       

   
   
   
   
   


   
   
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Oranikle.Report.Engine
{
	/// <summary>
	/// Represents a list of the tokens.
	/// </summary>
	public class TokenList : IEnumerable
	{
		private List<Token> tokens = null;

		public TokenList()
		{
			tokens = new List<Token>();
		}

		public void Add(Token token)
		{
			tokens.Add(token);
		}

		public void Push(Token token)
		{
			tokens.Insert(0, token);
		}

		public Token Peek()
		{
			return tokens[0];
		}

		public Token Extract()
		{
			Token token = tokens[0];
			tokens.RemoveAt(0);
			return token;
		}

		public int Count
		{
			get
			{
				return tokens.Count;
			}
		}

		public IEnumerator GetEnumerator()
		{
			return tokens.GetEnumerator();
		}
	}
}
