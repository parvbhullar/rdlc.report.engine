/* ====================================================================

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;


namespace Oranikle.Report.Engine
{
   
    public class EMFStringFormat : EMFRecordObject
    {
        public StringFormat myStringFormat;
       
        private EMFStringFormat(byte[] RecordData)        
        {
            ObjectType = EmfObjectType.stringformat;
            myStringFormat = new System.Drawing.StringFormat();
            //put the Data into a stream and use a binary reader to read the data
            MemoryStream _ms = new MemoryStream(RecordData);
            BinaryReader _br = new BinaryReader(_ms);           
            _br.ReadUInt32(); //Who cares about version..not me!            
            myStringFormat.FormatFlags = (StringFormatFlags)_br.ReadUInt32();
            _br.ReadBytes(4);//Language...Ignore for now!
            myStringFormat.LineAlignment = (StringAlignment)_br.ReadUInt32();
            myStringFormat.Alignment = (StringAlignment)_br.ReadUInt32();
            UInt32 DigitSubstitutionMethod = _br.ReadUInt32();
            UInt32 DigitSubstitutionLanguage = _br.ReadUInt32();
            myStringFormat.SetDigitSubstitution((int)DigitSubstitutionLanguage, (StringDigitSubstitute)DigitSubstitutionMethod);
            
            Single FirstTabOffSet = _br.ReadSingle();

            myStringFormat.HotkeyPrefix = (System.Drawing.Text.HotkeyPrefix) _br.ReadInt32();

             _br.ReadSingle();//leading Margin
             _br.ReadSingle();//trailingMargin           
             _br.ReadSingle();//tracking
            myStringFormat.Trimming = (StringTrimming)_br.ReadUInt32();           
            Int32 TabStopCount = _br.ReadInt32();
            Int32 RangeCount = _br.ReadInt32();
            //Next is stringformatdata...
            Single[] TabStopArray;           
            System.Drawing.CharacterRange[] RangeArray;

            if (TabStopCount > 0)
            {
                TabStopArray = new Single[TabStopCount];
                for (int i = 0; i < TabStopCount; i++)
                {
                    TabStopArray[i] = _br.ReadSingle();
                }
                myStringFormat.SetTabStops(FirstTabOffSet, TabStopArray);
            }

            if (RangeCount > 0)
            {
                RangeArray = new System.Drawing.CharacterRange[RangeCount];
                for (int i = 0; i < RangeCount; i++)
                {
                    RangeArray[i].First = _br.ReadInt32();
                    RangeArray[i].Length = _br.ReadInt32();
                }
                myStringFormat.SetMeasurableCharacterRanges(RangeArray);
            }
        }


        public static EMFStringFormat getEMFStringFormat(byte[] RecordData)
        {
            return new EMFStringFormat(RecordData);         
        }      
               
    }
}
