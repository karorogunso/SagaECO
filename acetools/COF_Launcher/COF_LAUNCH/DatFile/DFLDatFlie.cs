using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace COF_LAUNCH
{
    public unsafe class DFLDatFile : Header
    {
        Dictionary<string, Header> headers = new Dictionary<string, Header>();
        System.IO.BinaryReader brdata;
        System.IO.BinaryWriter bwdata;
        System.IO.BinaryReader brhed;
        System.IO.BinaryWriter bwhed;

        public Dictionary<string, Header> Files { get { return this.headers; } }

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

        public void Close()
        {
            brhed.BaseStream.Flush();
            brhed.BaseStream.Close();
        }
    }
}
