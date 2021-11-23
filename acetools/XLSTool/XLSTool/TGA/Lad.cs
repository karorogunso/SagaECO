using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
namespace XLSTool
{
    public unsafe class LoadTGA
    {
        public class Header
        {
            public string filename;
            public int offset;
            public int packsize;
            public int unpacksize;
            public int index;
        }

        public Dictionary<string, Header> headers = new Dictionary<string, Header>();
        BinaryReader brdata;
        BinaryWriter bwdata;
        BinaryReader brhed;
        BinaryWriter bwhed;

        public static Dictionary<string, Header> Files = new Dictionary<string, Header>();
        [DllImport("Unpack.dll")]
        static extern int Pack(byte[] src, int srcSize, byte** dest, out int destSize, int dw, int dw2);
        [DllImport("Unpack.dll")]
        static extern int Unpack(byte[] src, int srcSize, byte** dest, out int destSize, int dw);
        [DllImport("Unpack.dll")]
        static extern int PackFree(byte** dest);

        public static bool Unpack(byte[] src, byte[] dst)
        {
            fixed (byte* ptr = dst)
            {
                byte* tmp = ptr;
                int dstSize = dst.Length;
                int res = Unpack(src, src.Length, &tmp, out dstSize, 1);
                return (res == 1);
            }
        }
        public Image Extract(string filename)
        {
            if (headers.ContainsKey(filename))
            {
                Header header = headers[filename];
                byte[] buf;
                brdata.BaseStream.Position = header.offset;
                if ((header.packsize & 0x80000000) == 0x80000000)
                    header.packsize = (int)(header.packsize ^ 0x80000000);
                if (header.packsize != header.unpacksize)
                {
                    buf = new byte[header.unpacksize];
                    Unpack(brdata.ReadBytes(header.packsize), buf);
                }
                else
                    buf = brdata.ReadBytes(header.packsize);
                MemoryStream ms = new MemoryStream(buf);

                Texture tex = new Texture(ms);
                return tex.Image;
            }
            else
                return null;
        }
        public void Open(string path)
        {
            if (Path.GetExtension(path) != ".hed")
                throw new ArgumentException("Can only open header(.hed) file");
            headers.Clear();
            FileStream fs = new FileStream(path, FileMode.Open);
            FileStream datafs = new FileStream(Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".dat", FileMode.Open);
            brhed = new BinaryReader(fs);
            bwhed = new BinaryWriter(fs);
            brdata = new BinaryReader(datafs);
            bwdata = new BinaryWriter(datafs);
            int filelist = brhed.ReadInt32();
            int namepacklen = brhed.ReadInt32();
            int namelen = brhed.ReadInt32();
            byte[] names = new byte[namelen];
            if ((namepacklen & 0x80000000) == 0x80000000)
                namepacklen = (int)(namepacklen ^ 0x80000000);
            brdata.BaseStream.Position = filelist;
            if (namepacklen != namelen)
                Unpack(brdata.ReadBytes(namepacklen), names);
            else
                names = brdata.ReadBytes(namepacklen);
            MemoryStream ms = new MemoryStream(names);
            ms.Position = 0;
            BinaryReader brname = new BinaryReader(ms);
            int count = brname.ReadInt32();
            string[] filenames = System.Text.Encoding.ASCII.GetString(brname.ReadBytes(namelen - 4)).Split('\0');
            for (int i = 0; i < count; i++)
            {
                if (filenames[i] == "")
                    continue;
                Header header = new Header();
                header.filename = filenames[i];
                header.offset = brhed.ReadInt32();
                header.packsize = brhed.ReadInt32();
                header.unpacksize = brhed.ReadInt32();
                header.index = i;
                headers.Add(filenames[i], header);
            }
        }
    }
}
