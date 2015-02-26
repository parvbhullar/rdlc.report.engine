using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;

namespace Oranikle.Report.Engine
{
    public class DMPPage
    {
        public int PageNumber { get; set; }
        public List<DMPLine> Content { get; set; }
        public List<DMPControl> ContentControls { get; set; }
        //default row height and blank space width
        float spaceWidth = 6.7f, rowHeight = 12f;


        private int TotalLines = 0;
        private int TotalSpaces = 0;
        public bool IsInPageHeader = false;
        public bool IsInPageFooter = false;
        public bool IsInBody = false;

        private string Chr(int asc)
        {
            string ret = "";
            ret += (char)asc;
            return ret;
        }

        [DllImport("kernel32.dll", EntryPoint = "CreateFileA")]
        static extern int CreateFileA(string lpFileName, int dwDesiredAccess, int dwShareMode,
            int lpSecurityAttributes,
            int dwCreationDisposition, int dwFlagsAndAttributes,
            int hTemplateFile);

        [DllImport("kernel32.dll", EntryPoint = "CloseHandle")]
        static extern int CloseHandle(int hObject);

        /// <summary>
        /// Set the printer to normal.
        /// </summary>
        public string Normal
        {
            get
            {
                return Chr(18);
            }
        }

        /// <summary>
        /// Set the printer to print in condensed mode.
        /// </summary>
        public string Pill
        {
            get
            {
                return Chr(15);
            }
        }

        /// <summary>
        /// Set the printer to print in expanded mode.
        /// </summary>
        public string Expanded
        {
            get
            {
                return Chr(14);
            }
        }

        /// <summary>
        /// Set the printer to print in expanded mode normal.
        /// </summary>
        public string ExpandedNormal
        {
            get
            {
                return Chr(20);
            }
        }

        /// <summary>
        /// Enables the bold printer.
        /// </summary>
        public string BoldOn
        {
            get
            {
                return Chr(27) + Chr(69);
            }
        }

        /// <summary>
        /// Turn off the printer so bold.
        /// </summary>
        public string BoldOff
        {
            get
            {
                return Chr(27) + Chr(70);
            }
        }

        public DMPPage(Page page, Report report)
        {
            Content = new List<DMPLine>();
            ContentControls = new List<DMPControl>();
            SetPageFrame(report.ReportDefinition.PageHeight.Points, report.ReportDefinition.PageWidth.Points);
            SetPageContentControls(page);
            //ControlsToContent();
        }

        private void SetPageContentControls(IEnumerable items)
        {
            foreach (PageItem pi in items)
            {
                if (pi is PageText)
                {
                    PageText pt = pi as PageText;
                  
                    this.InsertContentControl(pt.ItemNumber, pt.Text, pt.X, pt.Y, pt.W, pt.H, pt.SI, pt);
                    continue;
                }
                if (pi is PageLine)
                {
                    PageLine pl = pi as PageLine;
                    this.InsertLineContent(pl.ItemNumber, pl.X, pl.Y, pl.X2, pl.Y2, pl.SI);
                    continue;
                }
            }
            
        }

        private void ControlsToContent()
        {
            foreach (DMPControl control in ContentControls)
            {
                ControlToLine(control);
            }
        }

        private void ControlToLine(DMPControl control)
        {
            if (Content.Count > control.LineNumber)
            {
               bool isFullTextPrinted = Content[control.LineNumber].InsertText(control.String, control.FromIndex, control.ToIndex, control.SI);
               int lineNumber = control.LineNumber + 1;
               int sCountFrom = control.FromIndex;
               int sCountTo = control.ToIndex;
               if (control.CanGrow)
                   CheckGrowedText(isFullTextPrinted, control.String, lineNumber, sCountFrom, sCountTo, control.SI);
            }
        }

        private void InsertContentControl(int number, string content, float X, float Y, float W, float H, StyleInfo styleInfo, PageText pt)
        {
            int sCountTo, sCountFrom;
            sCountFrom = (int)(X / spaceWidth);
            sCountTo = (int)((X + W) / spaceWidth);
            int rCount = (int)((Y + H) / rowHeight);
            //if (styleInfo.FontWeight == FontWeightEnum.Bold)
            //{
            //    content = content.Insert(0, BoldOn);
            //    content = content.Insert(content.Length, BoldOff);
            //}
            //if (styleInfo.FontSize > 11f)
            //{
            //    content = content.Insert(0, Expanded);
            //    content = content.Insert(content.Length, ExpandedNormal);
            //}
            DMPControl control = new DMPControl();
            control.Number = number;
            control.LineNumber = rCount;
            control.FromIndex = sCountFrom;
            control.ToIndex = sCountTo;
            control.ContentLength = content.Length;
            control.VerticalAlign = styleInfo.VerticalAlign;
            control.TextAlign = styleInfo.TextAlign;
            control.String = content;
            control.CanGrow = pt.CanGrow;
            control.SI = styleInfo;
            control.TopBorder = styleInfo.BStyleTop == BorderStyleEnum.Solid || styleInfo.BStyleTop == BorderStyleEnum.Dotted;
            control.BottomBorder = styleInfo.BStyleBottom == BorderStyleEnum.Solid || styleInfo.BStyleBottom == BorderStyleEnum.Dotted;
            ContentControls.Add(control);
        }
        public void InsertLineContent(int number, float X, float Y, float W, float H, StyleInfo styleInfo)
        {
            int sCountTo, sCountFrom;
            sCountFrom = (int)(X / spaceWidth);
            sCountTo = (int)((X + W) / spaceWidth);
            int rCount = (int)((Y + H) / rowHeight);
            int i = rCount;
            DMPControl control = new DMPControl();
            control.Number = number;
            control.LineNumber = rCount;
            control.FromIndex = sCountFrom;
            control.ToIndex = sCountTo;
            control.VerticalAlign = styleInfo.VerticalAlign;
            control.TextAlign = styleInfo.TextAlign;
            control.InsertLine(sCountFrom, sCountTo);
            control.CanGrow = false;
            control.SI = styleInfo;
            control.TopBorder = styleInfo.BStyleTop == BorderStyleEnum.Solid || styleInfo.BStyleTop == BorderStyleEnum.Dotted;
            control.BottomBorder = styleInfo.BStyleBottom == BorderStyleEnum.Solid || styleInfo.BStyleBottom == BorderStyleEnum.Dotted;
            ContentControls.Add(control);
        }

        public void SetPageFrame(float height, float width)
        {
            int tempSCount, sCount;
            tempSCount = sCount = (int)(width / spaceWidth);

            int rCount = (int)(height / rowHeight);
            TotalLines = rCount;
            TotalSpaces = tempSCount;
            
            //Insert Blank Space Frame
            while (rCount > 0)
            {
                AddLine(sCount);
                sCount = tempSCount;
                rCount--;
            }
        }

        public void SetPageHeader(float X, float Y, float W, float H)
        {
            int sCountTo, sCountFrom, rCountFrom, rCountTo;
            
            rCountFrom = (int)((Y) / rowHeight);
            rCountTo = (int)((Y + H) / rowHeight);
            foreach (DMPLine line in Content)
            {
                if (line.LineNumber >= rCountFrom && line.LineNumber <= rCountTo)
                    line.IsHeader = true;
            }
        }

        public void SetPageFooter(float X, float Y, float W, float H)
        {
            int sCountTo, sCountFrom, rCountFrom, rCountTo;

            rCountFrom = (int)((Y) / rowHeight);
            rCountTo = (int)((Y + H) / rowHeight);
            foreach (DMPLine line in Content)
            {
                if (line.LineNumber >= rCountFrom && line.LineNumber <= rCountTo)
                    line.IsFooter = true;
            }
        }

        public void AddLine(int charSizeOfLine)
        { 
            DMPLine line = new DMPLine();
            line.LineNumber = Content.Count + 1;
            while (charSizeOfLine > 0)
            {
                line.AddChar(' ');
                charSizeOfLine--;
            }
            Content.Add(line);
        }

        public void InsertForcedLine(int index, int charSizeOfLine)
        {
            DMPLine line = new DMPLine();
            line.LineNumber = index;
            line.IsForced = true;
            while (charSizeOfLine > 0)
            {
                line.AddChar(' ');
                charSizeOfLine--;
            }
            Content.Insert(index, line);
            RefreshLineNumbers();
        }

        private void RefreshLineNumbers()
        {
            int i = 1;
            foreach (DMPLine line in Content)
            {
                line.LineNumber = i;
                i++;
            }
        }

        private int CountOfForcedLines()
        {
            int i = 1;
            foreach (DMPLine line in Content)
            {
                if (line.IsForced)
                {
                    line.ForcedLineNumber = i;
                    i++;
                }
            }
            return i;
        }

        private int CountOfForcedLinesBefore(int index)
        {
            int i = 1;
            foreach (DMPLine line in Content)
            {
                if (line.LineNumber <= index && line.IsForced)
                {
                    i++;
                }
            }
            return i;
        }

        private int CountOfForcedLinesAfter(int index)
        {
            int i = 1;
            foreach (DMPLine line in Content)
            {
                if (line.LineNumber >= index && line.IsForced)
                {
                    i++;
                }
            }
            return i;
        }

        public string ContentToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DMPLine line in Content)
            {
                sb.AppendLine(line.LineToString());
            }
            return sb.ToString();
        }

        public void InsertContent(string content, float X, float Y, float W, float H, bool canGrow)
        {
            int sCountTo, sCountFrom;
            sCountFrom = (int)(X / spaceWidth);
            sCountTo = (int)((X + W) / spaceWidth);
            int rCount = (int)((Y + H) / rowHeight);
            if (Content.Count >= rCount)
            {
                StyleInfo styleInfo = new StyleInfo() { TextAlign = TextAlignEnum.Left };
                bool isFullTextPrinted = Content[rCount].InsertText(content, sCountFrom, sCountTo, styleInfo);
                if (canGrow)
                    CheckGrowedText(isFullTextPrinted, content, rCount, sCountFrom, sCountTo, styleInfo);
            }
        }

        public void InsertContent(string content, float X, float Y, float W, float H, StyleInfo styleInfo, bool canGrow)
        {
            int sCountTo, sCountFrom;
            sCountFrom = (int)(X / spaceWidth);
            sCountTo = (int)((X + W) / spaceWidth);
            int rCount = (int)((Y + H) / rowHeight);
            if (Content.Count >= rCount)
            {
                if (styleInfo.FontWeight == FontWeightEnum.Bold)
                {
                    content = content.Insert(0, BoldOn);
                    content = content.Insert(content.Length, BoldOff);
                }
                if (styleInfo.FontSize > 11f)
                {
                    content = content.Insert(0, Expanded);
                    content = content.Insert(content.Length, ExpandedNormal);
                }
                InsertBordar(X, Y, W, H, styleInfo);

                 bool isFullTextPrinted = Content[rCount].InsertText(content, sCountFrom, sCountTo, styleInfo);
                 if (canGrow)
                     CheckGrowedText(isFullTextPrinted, content, rCount, sCountFrom, sCountTo, styleInfo);
            }
            
        }

        private void CheckGrowedText(bool isFullTextPrinted, string content, int rCount, int sCountFrom, int sCountTo, StyleInfo styleInfo)
        {
            int lineNumber = rCount + 1;
            int index = sCountTo - (sCountFrom + 1);
            string text = content;
            while (!isFullTextPrinted)
            {
                text = text.Remove(0, index);
                if (Content.Count > lineNumber)
                    isFullTextPrinted = Content[lineNumber].InsertGrowedText(text, sCountFrom, sCountTo, styleInfo);
                else
                    break;
                lineNumber = lineNumber + 1;
                
            }
        }

        public void InsertBordar(float X, float Y, float W, float H, StyleInfo styleInfo)
        {
            int sCountTo, sCountFrom;
            sCountFrom = (int)(X / spaceWidth);
            sCountTo = (int)((X + W)/ spaceWidth);
            int rCount = (int)((Y + H) / rowHeight);
            string content = string.Empty;
            if (styleInfo.BStyleTop == BorderStyleEnum.Solid || styleInfo.BStyleTop == BorderStyleEnum.Dotted)
            {
                int i = rCount - 1;
                if (Content.Count >= i)
                {
                    if (!Content[i].InsertHorizontalBorder(sCountFrom, sCountTo))
                    {
                       // this.InsertForcedLine(i, TotalSpaces);
                       // Content[i].InsertHorizontalBorder(sCountFrom, sCountTo);
                    }
                }
            }
            if (styleInfo.BStyleBottom == BorderStyleEnum.Solid || styleInfo.BStyleBottom == BorderStyleEnum.Dotted)
            {
                int i = rCount + 1;
                if (Content.Count >= i)
                {
                    if (!Content[i].InsertHorizontalBorder(sCountFrom, sCountTo))
                    {
                       // this.InsertForcedLine(i, TotalSpaces);
                        //Content[i].InsertHorizontalBorder(sCountFrom, sCountTo);
                    }
                }
            }
            if (styleInfo.BStyleRight == BorderStyleEnum.Solid || styleInfo.BStyleRight == BorderStyleEnum.Dotted)
            {
                int i = sCountTo;
                if (Content.Count >= rCount)
                {
                    Content[rCount].InsertVerticalBorder(i);
                }
            }
            if (styleInfo.BStyleLeft == BorderStyleEnum.Solid || styleInfo.BStyleLeft == BorderStyleEnum.Dotted)
            {
                int i = sCountFrom;
                if (Content.Count >= rCount)
                {
                    Content[rCount].InsertVerticalBorder(i);
                }
            }
        }

        public void InsertLine(float X, float Y, float W, float H, StyleInfo styleInfo)
        {
            int sCountTo, sCountFrom;
            sCountFrom = (int)(X / spaceWidth);
            sCountTo = (int)((X + W) / spaceWidth);
            int rCount = (int)((Y + H) / rowHeight);
            int i = rCount;
            if (Content.Count >= i)
            {
                Content[i].InsertLine(sCountFrom, sCountTo);
            }
        }
    }

    public class DMPControl
    {
        public int Number { get; set; }
        public int LineNumber { get; set; }
        public int FromIndex { get; set; }
        public int ToIndex { get; set; }
        public int ContentLength { get; set; }
        public List<DMPChar> Content { get; set; }
        public bool TopBorder { get; set; }
        public bool BottomBorder { get; set; }
        public char _LineHChar { get; set; }
        public char _LineVChar { get; set; }
        public TextAlignEnum TextAlign { get; set; }
        public VerticalAlignEnum VerticalAlign { get; set; }
        public StyleInfo SI { get; set; }
        public bool CanGrow { get; set; }

        public DMPControl()
        {
            _LineHChar = '-';
            _LineVChar = '|';
            Content = new List<DMPChar>();
            SI = new StyleInfo();
        }
        public string String
        {
            get {
                return LineToString();
            }
            set {
                InsertText(value, 0);
            }
        }
       

        public void AddChar(char _char)
        {
            DMPChar c = new DMPChar();
            c.CharNumber = Content.Count + 1;
            c.Char = _char;
            Content.Add(c);
        }

        public string LineToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DMPChar _char in Content)
            {
                sb.Append(_char.Char.ToString());
            }
            return sb.ToString();  
        }

        public void InsertText(string text, int index)
        {
            foreach (char _char in text)
            {
                if (Content.Count > index)
                {
                    Content[index].Char = _char;
                    index++;
                }
                else
                {
                    AddChar(_char);
                    index++;
                }
            }
        }

        public void InsertLine(int fromIndex, int toIndex)
        {
            foreach (DMPChar _char in Content)
            {
                if (_char.IsBlank() && _char.CharNumber >= fromIndex && _char.CharNumber <= toIndex)
                    _char.Char = _LineHChar;
            }
        }

        public bool InsertHorizontalBorder(int fromIndex, int toIndex)
        {
            if (this.IsBlank())
            {
                foreach (DMPChar _char in Content)
                { //_char.IsBlank() &&
                    if ( _char.CharNumber >= fromIndex && _char.CharNumber <= toIndex)
                        _char.Char = _LineHChar;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void InsertVerticalBorder(int index)
        {
            foreach (DMPChar _char in Content)
            {
                if ((_char.IsBlank() || _char.Char == _LineHChar) 
                    && (_char.CharNumber == index))
                    _char.Char = _LineVChar;
            }
        }

        public bool IsBlank()
        {
            foreach (DMPChar _char in Content)
            {
                if (!_char.IsBlank() && !( _char.Char == _LineHChar))
                    return false;
            }
            return true;
        }
    }

    public class DMPLine
    {
        public int LineNumber { get; set; }
        public int ForcedLineNumber { get; set; }
        public List<DMPChar> Line { get; set; }
        public char _LineHChar { get; set; }
        public char _LineVChar { get; set; }
        public bool IsForced { get; set; }
        public bool IsFooter { get; set; }
        public bool IsHeader { get; set; }
        public bool IsBody { get; set; }
        public string String
        {
            get {
                return LineToString();
            }
            set {
                InsertText(value, 0, Line.Count, new StyleInfo() { TextAlign = TextAlignEnum.Left });
            }
        }
        public DMPLine()
        {
            _LineHChar = '-';
            _LineVChar = '|';
            IsForced = false;
            IsFooter = false;
            IsHeader = false;
            IsBody = true;
            Line = new List<DMPChar>();
        }

        public void AddChar(char _char)
        {
            DMPChar c = new DMPChar();
            c.CharNumber = Line.Count + 1;
            c.Char = _char;
            Line.Add(c);
        }

        public string LineToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DMPChar _char in Line)
            {
                sb.Append(_char.Char.ToString());
            }
            return sb.ToString();  
        }

        public bool InsertText(string text, int index, int lastIndex, StyleInfo styleInfo)
        {
            int space = lastIndex - (index + 1);
            if (space > text.Length && styleInfo.TextAlign == TextAlignEnum.Center)
                index += (space - text.Length) / 2;
            else if (space > text.Length && styleInfo.TextAlign == TextAlignEnum.Right)
                index += (space - text.Length);

            foreach (char _char in text)
            {
                if (index >= lastIndex - 1) return false;
                
                    if (Line.Count > index)
                    {
                        Line[index].Char = _char;
                        index++;
                    }
                    else
                    {
                        AddChar(_char);
                        index++;
                    }
                

            }
            return true;
        }

        public void InsertLine(int fromIndex, int toIndex)
        {
            foreach (DMPChar _char in Line)
            {
                if (_char.IsBlank() && _char.CharNumber >= fromIndex && _char.CharNumber <= toIndex)
                    _char.Char = _LineHChar;
            }
        }

        public bool InsertHorizontalBorder(int fromIndex, int toIndex)
        {
            if (this.IsBlank())
            {
                foreach (DMPChar _char in Line)
                { //_char.IsBlank() &&
                    if ( _char.CharNumber >= fromIndex && _char.CharNumber <= toIndex)
                        _char.Char = _LineHChar;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void InsertVerticalBorder(int index)
        {
            foreach (DMPChar _char in Line)
            {
                if ((_char.IsBlank() || _char.Char == _LineHChar) 
                    && (_char.CharNumber == index))
                    _char.Char = _LineVChar;
            }
        }

        public bool IsBlank()
        {
            foreach (DMPChar _char in Line)
            {
                if (!_char.IsBlank() && !( _char.Char == _LineHChar))
                    return false;
            }
            return true;
        }

        public bool InsertGrowedText(string text, int index, int lastIndex, StyleInfo styleInfo)
        {
            int space = lastIndex - (index + 1);
            if (space > text.Length && styleInfo.TextAlign == TextAlignEnum.Center)
                index += (space - text.Length) / 2;
            else if (space > text.Length && styleInfo.TextAlign == TextAlignEnum.Right)
                index += (space - text.Length);
            foreach (char _char in text)
            {
                
                if (index >= lastIndex - 1) return false;
                if (Line.Count > index)
                {
                    Line[index].Char = Line[index].IsBlank() || Line[index].Char == _LineHChar ? _char : Line[index].Char;
                    index++;
                }
                else
                {
                    AddChar(_char);
                    index++;
                }
            }
            return true;
        }
    }

    public class DMPChar
    {
        public int CharNumber { get; set; }
        public char Char { get; set; }
        public char _BlankChar { get; set; }
        public DMPChar()
        {
            _BlankChar = ' ';
        }

        public bool IsBlank()
        {
            return Char == _BlankChar;
        }
    }
}
