/* ====================================================================

   
	
   
   
   

       

   
   
   
   
   


   
   
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Oranikle.Report.Engine
{
     public class EMFSetPageTransform
    {
        public System.Drawing.GraphicsUnit PageUnit;
        public bool postMultiplyTransform;
        public Single PageScale;
        public static EMFSetPageTransform getTransform(int flags, byte[] RecordData)
        {
            return new EMFSetPageTransform(flags, RecordData);
        }

        public EMFSetPageTransform(int flags, byte[] RecordData)
        {
            MemoryStream _fs = null;
            BinaryReader _fr = null;
            try
            {
                _fs = new MemoryStream(BitConverter.GetBytes(flags));
                _fr = new BinaryReader(_fs);

                //PageUnit...
                UInt16 PageU = _fr.ReadByte();
                PageUnit = (System.Drawing.GraphicsUnit)PageU;

                UInt16 RealFlags = _fr.ReadByte();
                //XXXXXAXX - if A = 1 the transform matrix is post-multiplied else pre-multiplied...
                //01234567
                postMultiplyTransform = ((RealFlags & (UInt16)Math.Pow(2, 5)) == Math.Pow(2, 5));
                PageScale = BitConverter.ToSingle(RecordData, 0);
                
            }
            finally
           {
               if (_fr != null)
                   _fr.Close();
               if (_fs != null)
                   _fs.Dispose();
               
           }
        }
    }
}
