using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PacketViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<SagaLib.Packet> currentList;
        int currentIndex;

        private void button1_Click(object sender, EventArgs e)
        {
            OD.ShowDialog();
            System.IO.FileStream fs = new System.IO.FileStream(OD.FileName, System.IO.FileMode.Open);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter BF = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            PacketProxy.PacketContainer pc = new PacketProxy.PacketContainer();
            PacketProxy.PacketContainer.Instance = (PacketProxy.PacketContainer)BF.Deserialize(fs);
            fs.Close();
            if (radioButton1.Checked)
                currentList = PacketProxy.PacketContainer.Instance.packets;
            else
                currentList = PacketProxy.PacketContainer.Instance.packets2;
            currentIndex = 0;
            showPacket();
            list.Items.Clear();
            foreach (SagaLib.Packet i in currentList)
            {
                string tmp = string.Format("0x{0:X4}", i.ID);
                if (list.Items.Contains(tmp))
                    continue;
                list.Items.Add(tmp, CheckState.Checked);
            }
        }

        private void showPacket()
        {
            SagaLib.Packet p = currentList[currentIndex];
            string tmp = "Sender:{0}\r\nOpcode:0x{1:X4}\r\nName:{2}\r\n\r\n{5}\r\n\r\nLength:{3}\r\nData:\r\n{4}\r\n";
            string tmp2 = p.DumpData();
            string sender;
            if (p.GetType() == typeof(PacketProxy.Packets.Client.SendUniversal))
                sender = "Client";
            else
                sender = "Server";
            tmp = string.Format(tmp, sender, p.ID, p.ToString(), p.data.Length, tmp2, "{0}");
            textBox1.Text = tmp;
            label1.Text = string.Format("{0} / {1}", currentIndex + 1, currentList.Count);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentIndex == currentList.Count - 1)
                return;
            currentIndex++;
            showPacket();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentIndex == 0) return;
            currentIndex--;
            showPacket();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                currentList = PacketProxy.PacketContainer.Instance.packets;
            else
                currentList = PacketProxy.PacketContainer.Instance.packets2;
            currentIndex = 0;
            showPacket();
            list.Items.Clear();
            
            foreach (SagaLib.Packet i in currentList)
            {
                string tmp = string.Format("0x{0:X4}", i.ID);
                if (list.Items.Contains(tmp))
                    continue;
                list.Items.Add(tmp, CheckState.Checked);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                currentList = PacketProxy.PacketContainer.Instance.packets;
            else
                currentList = PacketProxy.PacketContainer.Instance.packets2;
            currentIndex = 0;
            showPacket();
            list.Items.Clear();
            
            foreach (SagaLib.Packet i in currentList)
            {
                string tmp = string.Format("0x{0:X4}", i.ID);
                if (list.Items.Contains(tmp))
                    continue;
                list.Items.Add(tmp, CheckState.Checked);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<SagaLib.Packet> filter = new List<SagaLib.Packet>();
            if (radioButton1.Checked)
                currentList = PacketProxy.PacketContainer.Instance.packets;
            else
                currentList = PacketProxy.PacketContainer.Instance.packets2;
            
            foreach (SagaLib.Packet i in currentList)
            {
                string tmp = string.Format("0x{0:X4}", i.ID);
                if (list.CheckedItems.Contains(tmp))
                    filter.Add(i);
            }
            currentList = filter;
            currentIndex = 0;            
            showPacket();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < list.Items.Count; i++)
            {
                list.SetItemChecked(i, true);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < list.Items.Count; i++)
            {
                list.SetItemChecked(i, false);
            }
        }
    }
}
