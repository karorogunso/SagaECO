using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace EcoArchiver
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
            public string datafile;
            public byte[] filecontent;
        }

        Dictionary<string, Header> headers = new Dictionary<string, Header>();
        System.IO.BinaryReader brdata;
        System.IO.BinaryWriter bwdata;
        System.IO.BinaryReader brhed;
        System.IO.BinaryWriter bwhed;

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
            if (System.IO.Path.GetExtension(path) != ".dfl")
                throw new ArgumentException("Can only open header(.dfl) file");
            headers.Clear();
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            //System.IO.FileStream datafs = new System.IO.FileStream(System.IO.Path.GetDirectoryName(path) + "\\" + System.IO.Path.GetFileNameWithoutExtension(path) + ".dat", System.IO.FileMode.Open);
            brhed = new System.IO.BinaryReader(fs);
            bwhed = new System.IO.BinaryWriter(fs);
            int filelist = brhed.ReadInt32();
            int namepacklen = brhed.ReadInt32();
            if ((namepacklen & 0x80000000) == 0x80000000)
                namepacklen = (int)(namepacklen ^ 0x80000000);
            int namelen = brhed.ReadInt32();
            byte[] names = new byte[namelen];
            if (namepacklen != namelen)
                Unpack(brhed.ReadBytes(namepacklen), names);
            else
                names = brhed.ReadBytes(namepacklen);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(names);
            ms.Position = 0;
            System.IO.BinaryReader brname = new System.IO.BinaryReader(ms);
            string[] filenames = System.Text.Encoding.ASCII.GetString(brname.ReadBytes(namelen)).Split('\0');
            //string[] filenames = System.Text.Encoding.ASCII.GetString(brhed.ReadBytes(namelen - 4)).Split('\0');
            for (int i = 0; i < filelist; i++)
            {
                if (filenames[i] == "")
                    continue;
                Header header = new Header();
                header.filename = filenames[i];
                header.packsize = brhed.ReadInt32();
                if ((header.packsize & 0x80000000) == 0x80000000)
                    header.packsize = (int)(header.packsize ^ 0x80000000);
                header.unpacksize = brhed.ReadInt32();
                header.filecontent = new byte[header.unpacksize];
                if (header.packsize != header.unpacksize)
                {
                    header.filecontent = new byte[header.unpacksize];
                    Unpack(brhed.ReadBytes(header.packsize), header.filecontent);
                }
                else
                    header.filecontent = brhed.ReadBytes(header.packsize);
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
                bwhed.Write((int)(header.packsize | 0x80000000));
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
                bwhed.Write((uint)(packed.Length | 0x80000000));
                bwhed.Write(size);                
            }
            else
                Replace(filename, input);
        }

        public void Replace(string filename, byte[] buf)
        {
            if (headers.ContainsKey(filename))
            {
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
                bwhed.Write((uint)(header.packsize | 0x80000000));
                bwhed.Write(header.unpacksize);
            }
        }

        public void Extract(string filename, string path)
        {
            if (headers.ContainsKey(filename))
            {
                Header header = headers[filename];
                System.IO.FileStream fs = new System.IO.FileStream(path + "\\" + filename, System.IO.FileMode.Create);
                /*byte[] buf;
                //brdata.BaseStream.Position    = header.offset;
                if ((header.packsize & 0x80000000) == 0x80000000)
                    header.packsize = (int)(header.packsize ^ 0x80000000);
                if ((header.unpacksize & 0x80000000) == 0x80000000)
                    header.unpacksize = (int)(header.unpacksize ^ 0x80000000);
                if (header.packsize != header.unpacksize)
                {
                    buf = new byte[header.unpacksize];
                    Unpack(brhed.ReadBytes(header.packsize), buf);
                }
                else
                    buf = brdata.ReadBytes(header.packsize);
                */
                fs.Write(header.filecontent, 0, header.filecontent.Length);
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
            brhed.BaseStream.Flush();
            brhed.BaseStream.Close();
        }
    }
}
