/* ====================================================================

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace Oranikle.Report.Engine
{
    public class FillPie : DrawBase
    {
        public FillPie(Single Xin, Single Yin, Single WidthIn, Single HeightIn, System.Collections.Hashtable ObjectTableIn)
        {
            X = Xin;
            Y = Yin;
            Width = WidthIn;
            Height = HeightIn;
            ObjectTable = ObjectTableIn;
            items = new List<PageItem>();
        }
        public List<PageItem> Process(int Flags, byte[] RecordData)
        {
            MemoryStream _ms = null;
            BinaryReader _br = null;
            MemoryStream _fs = null;
            BinaryReader _fr = null;
            try
            {
                _fs = new MemoryStream(BitConverter.GetBytes(Flags));
                _fr = new BinaryReader(_fs);
                _fr.ReadByte();
                //Byte 2 is the real flags
                byte RealFlags = _fr.ReadByte();
                // 0 1 2 3 4 5 6 7
                // X X X X X X C S                
                // if C = 1 Data int16 else float!
                bool Compressed = ((RealFlags & (int)Math.Pow(2, 6)) == (int)Math.Pow(2, 6));
                bool BrushIsARGB = ((RealFlags & (int)Math.Pow(2, 7)) == (int)Math.Pow(2, 7));
                _ms = new MemoryStream(RecordData);
                _br = new BinaryReader(_ms);
                Brush b;
                if (BrushIsARGB)
                {
                    byte A, R, G, B;
                    B = _br.ReadByte();
                    G = _br.ReadByte();
                    R = _br.ReadByte();
                    A = _br.ReadByte();
                    b = new SolidBrush(Color.FromArgb(A, R, G, B));
                }
                else
                {
                    UInt32 BrushID = _br.ReadUInt32();
                    EMFBrush EMFb = (EMFBrush)ObjectTable[(byte)BrushID];
                    b = EMFb.myBrush;
                }
                Single StartAngle = _br.ReadSingle();
                Single SweepAngle = _br.ReadSingle();
                if (Compressed)
                {
                    DoCompressed(StartAngle,SweepAngle, _br, b);
                }
                else
                {
                    DoFloat(StartAngle,SweepAngle, _br, b);
                }
                return items;
            }
            finally
            {
                if (_br != null)
                    _br.Close();
                if (_ms != null)
                    _ms.Dispose();
                if (_fr != null)
                    _fr.Close();
                if (_fs != null)
                    _fs.Dispose();
            }
        }

        private void DoFloat(Single StartAngle,Single SweepAngle, BinaryReader _br, Brush b)
        {
                Single recX = _br.ReadSingle();
                Single recY = _br.ReadSingle();
                Single recWidth = _br.ReadSingle();
                Single recHeight = _br.ReadSingle();
                DoInstructions(recX, recY, recWidth, recHeight, b, StartAngle, SweepAngle);          
        }

        private void DoCompressed(Single StartAngle, Single SweepAngle, BinaryReader _br, Brush b)
        {
            Int16 recX = _br.ReadInt16();
            Int16 recY = _br.ReadInt16();
            Int16 recWidth = _br.ReadInt16();
            Int16 recHeight = _br.ReadInt16();
            DoInstructions(recX, recY, recWidth, recHeight, b, StartAngle, SweepAngle);            
        }

        private void DoInstructions(Single recX, Single recY, Single recWidth, Single recHeight, Brush b, Single StartAngle, Single SweepAngle)
        {

            PagePie pl = new PagePie();
            pl.StartAngle = StartAngle;
            pl.SweepAngle = SweepAngle;

            StyleInfo SI = new StyleInfo();
            pl.X = X + recX * SCALEFACTOR;
            pl.Y = Y + recY * SCALEFACTOR;
            pl.W = recWidth * SCALEFACTOR;
            pl.H = recHeight * SCALEFACTOR;

            switch (b.GetType().Name)
            {
                case "SolidBrush":
                    System.Drawing.SolidBrush theBrush = (System.Drawing.SolidBrush)b;
                    SI.Color = theBrush.Color;
                    SI.BackgroundColor = theBrush.Color;
                    break;
                case "LinearGradientBrush":
                    System.Drawing.Drawing2D.LinearGradientBrush linBrush = (System.Drawing.Drawing2D.LinearGradientBrush)b;
                    SI.BackgroundGradientType = BackgroundGradientTypeEnum.LeftRight;
                    SI.BackgroundColor = linBrush.LinearColors[0];
                    SI.BackgroundGradientEndColor = linBrush.LinearColors[1];
                    break;
                case "HatchBrush":
                    System.Drawing.Drawing2D.HatchBrush hatBrush = (System.Drawing.Drawing2D.HatchBrush)b;
                    SI.BackgroundColor = hatBrush.BackgroundColor;
                    SI.Color = hatBrush.ForegroundColor;

                    SI.PatternType = StyleInfo.GetPatternType(hatBrush.HatchStyle);
                    break;
                default:
                    break;
            }

            pl.SI = SI;
            items.Add(pl);
        }
    }
}


