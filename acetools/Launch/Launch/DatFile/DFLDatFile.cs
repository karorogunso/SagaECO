using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Launch
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
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open,System.IO.FileAccess.ReadWrite);
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
            fs.Close();
        }
        public void ExtractDFL(ECODatFile ECOdat, string path)
        {
            if (System.IO.Path.GetExtension(path) != ".dfl")
                throw new ArgumentException("Can only open header(.dfl) file");
            //headers.Clear();
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
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
            ms.Close();
            brname.Close();
            try
            {
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
                    //header.filecontent = new byte[header.unpacksize];
                    if (header.packsize != header.unpacksize)
                    {
                        header.filecontent = new byte[header.unpacksize];
                        Unpack(brhed.ReadBytes(header.packsize), header.filecontent);
                    }
                    else
                        header.filecontent = brhed.ReadBytes(header.packsize);

                    if (ECOdat.Files.ContainsKey(filenames[i]))//修补部分
                    {
                        MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "替换：" + filenames[i]; }));
                        ECOdat.Replace(filenames[i], header.filecontent);
                    }
                    else
                    {
                        MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "添加：" + filenames[i]; }));
                        ECOdat.Add(filenames[i], header.filecontent);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "替换添加出错！");
                MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.Close(); }));
            }
            fs.Close();
        }
        public void Close()
        {
            brhed.BaseStream.Flush();
            brhed.BaseStream.Close();
        }
    }
}
