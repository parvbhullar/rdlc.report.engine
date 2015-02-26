using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
namespace Oranikle.Report.Engine
{
    class PortaException : System.Exception { };

    /// <summary>
    /// Class for text printing directly to the printer port.
    /// By Parv Bhullar
    /// </ Summary>
    public class PrintText
    {
        private int GENERIC_WRITE = 0x40000000;
        private int OPEN_EXISTING = 3;
        private int FILE_SHARE_WRITE = 0x2;
        private string sPort;
        private int hPort;
        private FileStream outFile;
        private StreamWriter fileWriter;
        private IntPtr hPortP;
        private bool lOK = false;
        private string GeraFileLPT;

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

        public string Bold_On = "_G";
		public string Bold_Off = "_H";
        public string Width_On = "_W1"; //Chr(27) + Chr(87) + Chr(49) 'W1
        public string Width_Off = "_W0";

		//Public Const Compress_On = "¤"       'Chr(15)    '¤"
		//Public Const Compress_Off = "_" 'Chr(18)   '_
        public string ELITE_PITCH = "_M";
        public string Compress_On = "_ð";      //Chr(15)    '¤"
        public string Compress_Off = "_";
        public int ColWidth = 60;

        // / <summary>
        // / Starts printing in text mode.
        // / </ Summary>
        // / <param Name="sPortStart"> Specifies the printer port (LPT1, LPT2, LPT3, LPT4, ...) </ param>
        // / Returns true if <returns> inciar the printer and false otherwise </ returns>
        public bool Start(string sPortStart)
        {
            GeraFileLPT = "";
            sPortStart.ToUpper();
            outFile = null;
            if (sPortStart.Substring(0, 3) == "LPT")
            {
                if (sPortStart == "LPT")
                {
                    sPortStart = "LPT1";
                }
                sPort = sPortStart;
                sPortStart = "LPT-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + ".TXT";
                GeraFileLPT = sPortStart;
                fileWriter = new StreamWriter(sPortStart);
                lOK = true;
                //hPort = CreateFileA(sPort, GENERIC_WRITE, FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, 0);
                //if (hPort != -1)
                //{
                //    hPortP = new IntPtr(hPort);
                //    outFile = new FileStream(hPortP, FileAccess.Write);
                //    fileWriter = new StreamWriter(outFile);
                //    lOK = true;
                //}
                //else
                //{
                //    lOK = false;
                //}
            }
            else
            {
                fileWriter = new StreamWriter(sPortStart);
                lOK = true;
            }
            return lOK;
        }

        /// <summary>
        ///Terminates Printing.
        /// </summary>
        public void Close()
        {
            if (lOK)
            {
                fileWriter.Close();
                if (outFile != null)
                {
                    outFile.Close();
                    CloseHandle(hPort);
                }
                lOK = false;

                if (GeraFileLPT != String.Empty)
                {
                    System.IO.File.Copy(GeraFileLPT, sPort);
                    File.Delete(GeraFileLPT);
                    GeraFileLPT = "";
                }
            }
        }

        /// <summary>
        /// Print a string.
        /// </summary>
        /// <param Name="sLine"> String to be printed </ param>
        public void Print(string sLine)
        {
            if (lOK)
            {
                fileWriter.Write(sLine);
                fileWriter.Flush();
            }
        }

        /// <summary>
        /// Prints a string and skips a line.
        /// </summary>
        /// <param Name="sLine"> String to be printed </ param>
        public void PrintLF(string sLine)
        {
            if (lOK)
            {
                fileWriter.WriteLine(sLine);
                fileWriter.Flush();
            }
        }

        /// <summary>
        /// Prints a string in a particular column.
        /// </summary>
        /// <param Name="nCol"> Column to be placed </ param>
        /// <param Name="sLine"> String to be printed </ param>
        public void PrintCol(int nCol, string sLine)
        {
            string Cols = "";
            Cols = Cols.PadLeft(nCol, ' ');
            Print(Chr(13) + Cols + sLine);
        }

        /// <summary>
        /// Prints a string in a particular column and jumps a line.
        /// </summary>
        /// <param Name="nCol"> Column to be placed </ param>
        /// <param Name="sLine"> String to be printed </ param>
        public void PrintColLF(int nCol, string sLine)
        {
            PrintCol(nCol, sLine);
            Skip(1);
        }

        /// <summary>
        /// Skips a certain number of lines.
        /// </summary>
        /// <param Name="nLine"> number of lines to be skipped </ param>
        public void Skip(int nLine)
        {
            for (int i = 0; i < nLine; i++)
            {
                PrintLF("");
            }
        }

        /// <summary>
        ///  Ejects a page.
        /// </summary>
        public void Eject()
        {
            Print(Chr(12));
        }

        public PrintText()
        {
            sPort = "LPT1";
        }


    }
}
