using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DebugLogViewer
{
    public partial class Form1 : Form
    {
        System.IO.FileStream fs;
        System.IO.BinaryReader br;
        int page = 0;

        List<PacketHeader> headers = new List<PacketHeader>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OD.ShowDialog() == DialogResult.OK)
            {
                if (fs != null)
                    fs.Close();
                fs = new System.IO.FileStream(OD.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                br = new System.IO.BinaryReader(fs);
                headers.Clear();
                page = 0;
                while (fs.Position < fs.Length)
                {
                    PacketHeader header = new PacketHeader();
                    header.Type = (PacketType)br.ReadByte();
                    header.time = DateTime.FromBinary(br.ReadInt64());
                    switch (header.Type)
                    {
                        case PacketType.Client:
                            ushort id = br.ReadUInt16();                            
                            header.name = Encoding.UTF8.GetString(br.ReadBytes(br.ReadInt16())).Replace("SagaMap.Packets.Client.", "");
                            short propertyCount = br.ReadInt16();
                            for (int i = 0; i < propertyCount; i++)
                            {
                                header.properties.Add(Encoding.UTF8.GetString(br.ReadBytes(br.ReadInt16())),
                                        Encoding.UTF8.GetString(br.ReadBytes(br.ReadInt16())));
                            }
                            header.hasInventory = br.ReadBoolean();
                            if (header.hasInventory)
                            {
                                header.inventoryOffset = fs.Position;
                                short len = br.ReadInt16();
                                fs.Position += len;
                            }
                            if (id != 0x0FA5 && id != 0x0FA6)
                                headers.Add(header);
                            break;
                        case PacketType.Server:
                            header.name = Encoding.UTF8.GetString(br.ReadBytes(br.ReadInt16())).Replace("SagaMap.Packets.Server.", "");
                            byte[] buf = br.ReadBytes(br.ReadInt16());
                            header.content = new SagaLib.Packet();
                            header.content.data = buf;
                            header.hasInventory = br.ReadBoolean();
                            if (header.hasInventory)
                            {
                                header.inventoryOffset = fs.Position;
                                short len = br.ReadInt16();
                                fs.Position += len;
                            }
                            if (header.content.ID != 0x0FA5 && header.content.ID != 0x0FA6)                            
                            headers.Add(header);
                            break;
                    }
                }
                ShowPage(page);
            }
        }

        public void ShowPage(int page)
        {
            if (headers.Count > 0)
            {
                if (headers.Count > page * 100)
                {
                    listBox1.Items.Clear();
                    for (int i = 0; i < 100; i++)
                    {
                        int index = page * 100 + i;
                        if (index >= headers.Count)
                            break;
                        PacketHeader current = headers[index];
                        switch (current.Type)
                        {
                            case PacketType.Client:
                                {
                                    string property = "";
                                    foreach (string j in current.properties.Keys)
                                    {
                                        property += j + "=" + current.properties[j] + ",";
                                    }
                                    listBox1.Items.Add(string.Format("{0} Client {1},{2}", current.time, current.name, property));
                                }
                                break;
                            case PacketType.Server:
                                {
                                    listBox1.Items.Add(string.Format("{0} Server {1}", current.time, current.name));
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (page > 0)
            {
                page--;
                ShowPage(page);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (headers.Count > (page + 1) * 100)
            {
                page++;
                ShowPage(page);
            }
        }
    }
}
