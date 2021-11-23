using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace TomatoProxyTool
{
    public unsafe class DatFile
    {
        public class Header
        {
            public string filename;
            public int offset;
            public int packsize;
            public int unpacksize;
            public int index;
        }

        Dictionary<string, Header> headers = new Dictionary<string, Header>();
        System.IO.BinaryReader brdata;
        System.IO.BinaryWriter bwdata;
        System.IO.BinaryReader brhed;
        System.IO.BinaryWriter bwhed;
        System.IO.FileStream fs;
        System.IO.FileStream datafs;

        public Dictionary<string, Header> Files { get { return this.headers; } }

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

        public static bool Pack(byte[] src, out byte[] dst)
        {
            byte* pDst;
            int dstSize;
            int res = Pack(src, src.Length, &pDst, out dstSize, 1, 0);
            if (res == 1)
            {
                dst = new byte[dstSize];
                fixed (byte* ptr = dst)
                {
                    int* data = (int*)pDst;
                    int* dataDst = (int*)ptr;
                    int time = dstSize / 4;
                    for (int i = 0; i < time; i++)
                    {
                        dataDst[i] = data[i];
                    }
                    for (int i = 0; i < dstSize % 4; i++)
                    {
                        ptr[time * 4 + i] = pDst[time * 4 + i];
                    }
                }
            }
            else
                dst = new byte[0];
            //PackFree(&pDst);
            return (res == 1);
        }

        public void Open(string path)
        {
            if (System.IO.Path.GetExtension(path) != ".hed")
                throw new ArgumentException("Can only open header(.hed) file");
            headers.Clear();
            fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            datafs = new System.IO.FileStream(System.IO.Path.GetDirectoryName(path) + "\\" + System.IO.Path.GetFileNameWithoutExtension(path) + ".dat", System.IO.FileMode.Open);
            brhed = new System.IO.BinaryReader(fs);
            bwhed = new System.IO.BinaryWriter(fs);
            brdata = new System.IO.BinaryReader(datafs);
            bwdata = new System.IO.BinaryWriter(datafs);
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
            System.IO.MemoryStream ms = new System.IO.MemoryStream(names);
            ms.Position = 0;
            System.IO.BinaryReader brname = new System.IO.BinaryReader(ms);
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
        public bool Exists(string filename)
        {
            return headers.ContainsKey(filename);
        }

        public void Add(string filename, byte[] input)
        {
            if (!headers.ContainsKey(filename))
            {
                Header header = new Header();
                header.unpacksize = input.Length;
                byte[] packed;
                Pack(input, out packed);

                header.filename = filename;
                header.packsize = packed.Length;
                header.offset = (int)bwdata.BaseStream.Length;
                header.index = headers.Count;

                headers.Add(header.filename, header);
                bwdata.BaseStream.Position = header.offset;
                bwdata.Write(packed);
                bwhed.BaseStream.Position = (header.index + 1) * 12;
                bwhed.Write(header.offset);
                bwhed.Write((int)(Convert.ToUInt32(header.packsize) | 0x80000000));
                bwhed.Write(header.unpacksize);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms);

                bw.Write(headers.Count);
                foreach (string i in headers.Keys)
                {
                    bw.Write(System.Text.Encoding.ASCII.GetBytes(i + "\0"));
                }

                ms.Flush();
                int size = (int)ms.Length;

                Pack(ms.ToArray(), out packed);
                ms.Close();
                int offset = (int)bwdata.BaseStream.Position;
                bwdata.Write(packed);
                bwhed.BaseStream.Position = 0;
                bwhed.Write(offset);
                bwhed.Write((uint)(Convert.ToUInt32(packed.Length) | 0x80000000));
                bwhed.Write(size);
            }
            else
                Replace(filename, input);
        }

        public void Replace(string filename, byte[] buf)
        {
            if (headers.ContainsKey(filename))
            {
                bwhed = new System.IO.BinaryWriter(fs);
                bwdata = new System.IO.BinaryWriter(datafs);
                Header header = headers[filename];
                header.unpacksize = buf.Length;
                byte[] packed;
                Pack(buf, out packed);
                header.packsize = packed.Length;
                header.offset = (int)bwdata.BaseStream.Length;
                bwdata.BaseStream.Position = header.offset;
                bwdata.Write(packed);
                bwhed.BaseStream.Position = (header.index + 1) * 12;
                bwhed.Write(header.offset);
                bwhed.Write((uint)(Convert.ToUInt32(header.packsize) | 0x80000000));
                bwhed.Write(header.unpacksize);
            }
        }

        public void Extract(string filename, string path)
        {
            if (headers.ContainsKey(filename))
            {
                Header header = headers[filename];
                System.IO.FileStream fs = new System.IO.FileStream(path + "\\" + filename, System.IO.FileMode.Create);
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

                fs.Write(buf, 0, buf.Length);
                fs.Flush();
                fs.Close();
            }
        }

        public byte[] Extract(string filename)
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

                return buf;
            }
            return null;
        }

        public void Close()
        {
            brdata.BaseStream.Flush();
            brhed.BaseStream.Flush();
            brdata.BaseStream.Close();
            brhed.BaseStream.Close();
        }
    }
}
