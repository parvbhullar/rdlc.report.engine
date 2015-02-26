using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Skybound.Drawing.Design
{

    internal class IconFile
    {

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi, Pack = 4)]
        private struct BITMAP
        {

            public System.IntPtr bmBits;
            public short bmBitsPixel;
            public int bmHeight;
            public short bmPlanes;
            public int bmType;
            public int bmWidth;
            public int bmWidthBytes;

        }

        private struct BITMAPINFOHEADER
        {

            public ushort biBitCount;
            public uint biClrImportant;
            public uint biClrUsed;
            public uint biCompression;
            public int biHeight;
            public ushort biPlanes;
            public uint biSize;
            public uint biSizeImage;
            public int biWidth;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;

        }

        private struct ICONDIR
        {

            public ushort idCount;
            public short idReserved;
            public short idType;

            public void Read(System.IO.BinaryReader r)
            {
                idReserved = r.ReadInt16();
                idType = r.ReadInt16();
                idCount = r.ReadUInt16();
            }

            public void WriteToStream(System.IO.BinaryWriter w)
            {
                w.Write(idReserved);
                w.Write(idType);
                w.Write(idCount);
            }

        }

        private struct ICONDIRENTRY
        {

            public byte bColorCount;
            public byte bHeight;
            public byte bReserved;
            public byte bWidth;
            public int dwBytesInRes;
            public int dwImageOffset;
            public short wBitCount;
            public short wPlanes;

            public void Read(System.IO.BinaryReader r)
            {
                bWidth = r.ReadByte();
                bHeight = r.ReadByte();
                bColorCount = r.ReadByte();
                bReserved = r.ReadByte();
                wPlanes = r.ReadInt16();
                wBitCount = r.ReadInt16();
                dwBytesInRes = r.ReadInt32();
                dwImageOffset = r.ReadInt32();
            }

            public void WriteToStream(System.IO.BinaryWriter w)
            {
                w.Write(bWidth);
                w.Write(bHeight);
                w.Write(bColorCount);
                w.Write(bReserved);
                w.Write(wPlanes);
                w.Write(wBitCount);
                w.Write(dwBytesInRes);
                w.Write(dwImageOffset);
            }

        }

        private struct ICONINFO
        {

            public bool fIcon;
            public System.IntPtr hbmColor;
            public System.IntPtr hbmMask;
            public int xHotspot;
            public int yHotspot;

        }

        private Skybound.Drawing.Design.IconFormat[] Formats;
        private System.Collections.Hashtable IconData;
        private System.Collections.Hashtable IconDirEntries;

        public IconFile(System.IO.Stream s)
        {
            Skybound.Drawing.Design.IconFile.ICONDIR icondir;
            Skybound.Drawing.Design.IconFile.ICONDIRENTRY icondirentry;

            using (System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(s))
            {
                icondir = new Skybound.Drawing.Design.IconFile.ICONDIR();
                icondir.Read(binaryReader);
                if ((icondir.idReserved != 0) || (icondir.idType != 1))
                    throw new System.Exception("The specified stream did not contain a valid icon.");
                icondirentry = new Skybound.Drawing.Design.IconFile.ICONDIRENTRY();
                Formats = new Skybound.Drawing.Design.IconFormat[icondir.idCount];
                IconData = new System.Collections.Hashtable();
                IconDirEntries = new System.Collections.Hashtable();
                for (int i = 0; (ushort)i < icondir.idCount; i++)
                {
                    icondirentry.Read(binaryReader);
                    Formats[i] = new Skybound.Drawing.Design.IconFormat(new System.Drawing.Size(icondirentry.bWidth, icondirentry.bHeight), GetColorDepth(icondirentry.wBitCount * icondirentry.wPlanes));
                    long l = s.Position;
                    s.Position = (long)icondirentry.dwImageOffset;
                    byte[] bArr = new byte[icondirentry.dwBytesInRes];
                    s.Read(bArr, 0, icondirentry.dwBytesInRes);
                    IconData[Formats[i]] = bArr;
                    IconDirEntries[Formats[i]] = icondirentry;
                    s.Position = l;
                }
            }
        }

        private Skybound.Drawing.Design.IconFormat FindBestFormat(System.Drawing.Size size)
        {
            Skybound.Drawing.Design.IconFormat iconFormat1 = new Skybound.Drawing.Design.IconFormat(System.Drawing.Size.Empty, System.Windows.Forms.ColorDepth.Depth4Bit);
            Skybound.Drawing.Design.IconFormat[] iconFormatArr = Formats;
            for (int i = 0; i < iconFormatArr.Length; i++)
            {
                Skybound.Drawing.Design.IconFormat iconFormat2 = iconFormatArr[i];
                if (iconFormat2.Size == size && (iconFormat2.ColorDepth >= iconFormat1.ColorDepth))
                    iconFormat1 = iconFormat2;
            }
            return iconFormat1;
        }

        private System.Windows.Forms.ColorDepth GetColorDepth(int bits)
        {
            switch (bits)
            {
                case 4:
                    return System.Windows.Forms.ColorDepth.Depth4Bit;

                case 8:
                    return System.Windows.Forms.ColorDepth.Depth8Bit;

                case 16:
                    return System.Windows.Forms.ColorDepth.Depth16Bit;

                case 24:
                    return System.Windows.Forms.ColorDepth.Depth24Bit;

                case 32:
                    return System.Windows.Forms.ColorDepth.Depth32Bit;
            }
            return System.Windows.Forms.ColorDepth.Depth4Bit;
        }

        public Skybound.Drawing.Design.IconFormat[] GetFormats()
        {
            return (Skybound.Drawing.Design.IconFormat[])Formats.Clone();
        }

        public System.Drawing.Bitmap ToBitmap(System.Drawing.Size size)
        {
            Skybound.Drawing.Design.IconFormat iconFormat = FindBestFormat(size);
            if (iconFormat.Size == System.Drawing.Size.Empty)
                return null;
            return ToBitmap(iconFormat);
        }

        public System.Drawing.Bitmap ToBitmap(Skybound.Drawing.Design.IconFormat iconFormat)
        {
            return Skybound.Drawing.Design.IconFile.ToBitmap(ToIcon(iconFormat), iconFormat);
        }

        public System.Drawing.Icon ToIcon(Skybound.Drawing.Design.IconFormat iconFormat)
        {
            Skybound.Drawing.Design.IconFile.ICONDIR icondir;

            byte[] bArr = (byte[])IconData[iconFormat];
            System.Drawing.Icon icon = null;
            if (bArr != null)
            {
                System.IO.Stream stream = new System.IO.MemoryStream();
                System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(stream);
                icondir = new Skybound.Drawing.Design.IconFile.ICONDIR();
                icondir.idCount = 1;
                icondir.idType = 1;
                icondir.WriteToStream(binaryWriter);
                Skybound.Drawing.Design.IconFile.ICONDIRENTRY icondirentry = (Skybound.Drawing.Design.IconFile.ICONDIRENTRY)IconDirEntries[iconFormat];
                icondirentry.dwImageOffset = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Skybound.Drawing.Design.IconFile.ICONDIR)) + System.Runtime.InteropServices.Marshal.SizeOf(typeof(Skybound.Drawing.Design.IconFile.ICONDIRENTRY));
                icondirentry.WriteToStream(binaryWriter);
                stream.Write(bArr, 0, bArr.Length);
                stream.Position = (long)0;
                icon = new System.Drawing.Icon(stream, icondirentry.bWidth, icondirentry.bHeight);
            }
            return icon;
        }

        public System.Drawing.Icon ToIcon(System.Drawing.Size size)
        {
            Skybound.Drawing.Design.IconFormat iconFormat = FindBestFormat(size);
            if (iconFormat.Size == System.Drawing.Size.Empty)
                return null;
            return ToIcon(iconFormat);
        }

        public static System.Drawing.Bitmap ToBitmap(System.Drawing.Icon icon, Skybound.Drawing.Design.IconFormat iconFormat)
        {
            Skybound.Drawing.Design.IconFile.BITMAP bitmap;
            Skybound.Drawing.Design.IconFile.ICONINFO iconinfo;

            System.Drawing.Bitmap bitmap1 = null;
            if (icon != null)
            {
                if (iconFormat.ColorDepth == System.Windows.Forms.ColorDepth.Depth32Bit)
                {
                    Skybound.Drawing.Design.IconFile.GetIconInfo(icon.Handle, out iconinfo);
                    bitmap = new Skybound.Drawing.Design.IconFile.BITMAP();
                    Skybound.Drawing.Design.IconFile.GetObjectBitmap(iconinfo.hbmColor, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Skybound.Drawing.Design.IconFile.BITMAP)), ref bitmap);
                    int i = bitmap.bmWidthBytes * bitmap.bmHeight;
                    int[] iArr = new int[(i / 4)];
                    Skybound.Drawing.Design.IconFile.GetBitmapBits(iconinfo.hbmColor, i, iArr);
                    System.Runtime.InteropServices.GCHandle gchandle = System.Runtime.InteropServices.GCHandle.Alloc(iArr, System.Runtime.InteropServices.GCHandleType.Pinned);
                    System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(bitmap.bmWidth, bitmap.bmHeight, bitmap.bmWidthBytes, System.Drawing.Imaging.PixelFormat.Format32bppArgb, System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(iArr, 0));
                    bitmap1 = new System.Drawing.Bitmap(bitmap2.Width, bitmap2.Height);
                    using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap1))
                    {
                        graphics.DrawImage(bitmap2, 0, 0, bitmap2.Width, bitmap2.Height);
                    }
                    gchandle.Free();
                    Skybound.Drawing.Design.IconFile.DeleteObject(iconinfo.hbmMask);
                    Skybound.Drawing.Design.IconFile.DeleteObject(iconinfo.hbmColor);
                }
                else
                {
                    bitmap1 = icon.ToBitmap();
                }
            }
            return bitmap1;
        }

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int DeleteObject(System.IntPtr hObject);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int GetBitmapBits(System.IntPtr hBitmap, int dwCount, int[] lpBits);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int GetIconInfo(System.IntPtr hIcon, out Skybound.Drawing.Design.IconFile.ICONINFO piconinfo);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("gdi32", EntryPoint = "GetObject", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int GetObjectBitmap(System.IntPtr hObject, int nCount, ref Skybound.Drawing.Design.IconFile.BITMAP lpObject);

    } // class IconFile

}

