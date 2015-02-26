/* ====================================================================
  
*/

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Oranikle.Report.Engine
{
    public class DMPTextWriter
    {
        private TextWriter textWriter;

        private string columnDelimiter;
        private bool firstColumn = true;

        //private char quote = ' ';
        public List<DMPPage> Pages { get; set; }
        public DMPPage GetPage(int pageNumber)
        {
            return Pages[pageNumber];
        }
        public DMPPage LastPage
        {
            get { return Pages[Pages.Count - 1]; }
        }
        
        public TextWriter TextWriter
        {
            get
            {
                return textWriter;
            }
        }

             
        public string ColumnDelimiter
        {
            get { return columnDelimiter; }
        }

        private string rowDelimiter = "\n";

        public string RowDelimiter
        {
            get { return rowDelimiter; }
        }

       
        protected void WriteDelimeter()
        {
            if (!firstColumn)
            {
                textWriter.Write(columnDelimiter);
            }

            firstColumn = false;
        }

        public DMPTextWriter(TextWriter writer, string delimeter)
        {
            textWriter = writer;
            columnDelimiter = delimeter;
            Pages = new List<DMPPage>();
        }

        private void WriteQuoted(object value)
        {
            WriteDelimeter();
            //WriteQuote();

            if (value != null)
                textWriter.Write(value.ToString().Replace("\"", "\"\""));

            //WriteQuote();
        }

        private void WriteUnquoted(object value)
        {
            WriteDelimeter();

            if (value != null)
                textWriter.Write(value);
        }

        public void Write(object value)
        {
            bool isQuoted = true;

            if (value != null)
            {
                Type type = value.GetType();

                if (type.IsPrimitive &&
                    type != typeof(bool) && type != typeof(char))
                {
                    isQuoted = false;
                }
            }

            if (isQuoted)
                WriteQuoted(value);
            else
                WriteUnquoted(value);
        }

        public void Write(string format, params object[] arg)
        {
            WriteQuoted(string.Format(format, arg));
        }

        public void WriteLine()
        {
            textWriter.Write(rowDelimiter);
            firstColumn = true;
        }

        public void WriteLine(object value)
        {
            Write(value);
            WriteLine();
        }

        public void WriteLine(string format, params object[] arg)
        {
            Write(format, arg);
            WriteLine();
        }
    }
}