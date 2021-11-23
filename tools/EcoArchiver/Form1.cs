using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EcoArchiver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DatFile dat;
        DatFile dat2;
        private void Form1_Load(object sender, EventArgs e)
        {
            FD.SelectedPath = Application.StartupPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (OD.ShowDialog() == DialogResult.OK)
            {
                if (dat != null)
                    dat.Close();
                lst_File.Items.Clear();
                dat = new DatFile();
                dat.Open(OD.FileName);
                foreach (string i in dat.Files.Keys)
                {
                    lst_File.Items.Add(i);
                }
                button3.Enabled = true;
            }
        }

        private void lst_File_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_File.SelectedIndex == -1)
                button2.Enabled = false;
            else
                button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filename = (string)lst_File.SelectedItem;
            SD.FileName = filename;
            SD.Filter = filename + "|" + filename;
            if (SD.ShowDialog() == DialogResult.OK)
            {
                dat.Extract(filename, System.IO.Path.GetDirectoryName(SD.FileName));
                MessageBox.Show("Finished");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (FD.ShowDialog() == DialogResult.OK)
            {
                foreach (string i in dat.Files.Keys)
                {
                    try
                    {
                        dat.Extract(i, FD.SelectedPath);
                    }
                    catch { }
                }
                MessageBox.Show("Finished");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (FD.ShowDialog() == DialogResult.OK)
            {
                string path = FD.SelectedPath;
                SD.Filter = "*.hed|*.hed";
                if (SD.ShowDialog() == DialogResult.OK)
                {
                    string[] files = System.IO.Directory.GetFiles(path);
                    string tgPath = System.IO.Path.GetDirectoryName(SD.FileName);
                    string tgPre = System.IO.Path.GetFileNameWithoutExtension(SD.FileName);
                    System.IO.FileStream output = new System.IO.FileStream(tgPath + "\\" + tgPre + ".dat", System.IO.FileMode.Create);
                    System.IO.FileStream header = new System.IO.FileStream(tgPath + "\\" + tgPre + ".hed", System.IO.FileMode.Create);
                    System.IO.BinaryWriter bwout = new System.IO.BinaryWriter(output);
                    System.IO.BinaryWriter bwhed = new System.IO.BinaryWriter(header);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    System.IO.BinaryWriter bwms = new System.IO.BinaryWriter(ms);
                    bwms.Write(files.Length);
                    foreach (string i in files)
                    {
                        //if (System.IO.Path.GetExtension(i) != ".csv")
                        //    continue;
                        string file = System.IO.Path.GetFileName(i) + "\0";
                        byte[] buf = System.Text.Encoding.ASCII.GetBytes(file);
                        bwms.Write(buf);
                    }
                    ms.Flush();
                    byte[] filelist = new byte[(int)(ms.Position + 1)];
                    Array.Copy(ms.GetBuffer(), filelist, filelist.Length);
                    int listsize = filelist.Length;
                    int listPackSize = filelist.Length;
                    if (cb_comp.Checked)
                    {
                        DatFile.Pack(filelist, out filelist);
                        listPackSize = (int)(Convert.ToUInt32(filelist.Length) | 0x80000000);
                    }
                    bwhed.Write(0);
                    bwhed.Write(listPackSize);
                    bwhed.Write(listsize);
                    bwout.Write(filelist);
                    foreach (string i in files)
                    {
                        //if (System.IO.Path.GetExtension(i) != ".csv")
                        //    continue;
                        byte[] buf;
                        int offset = (int)output.Position;
                        System.IO.FileStream fs = new System.IO.FileStream(i, System.IO.FileMode.Open);
                        buf = new byte[fs.Length];
                        fs.Read(buf, 0, buf.Length);
                        fs.Close();
                        int packSize = buf.Length;
                        int size = buf.Length;
                        if (cb_comp.Checked)
                        {
                            DatFile.Pack(buf, out buf);
                            packSize = (int)(Convert.ToUInt32(buf.Length) | 0x80000000);
                        }
                        bwhed.Write(offset);
                        bwhed.Write(packSize);
                        bwhed.Write(size);
                        bwout.Write(buf);
                    }
                    output.Flush();
                    output.Close();
                    header.Flush();
                    header.Close();

                    MessageBox.Show("Finished");
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (OD.ShowDialog() == DialogResult.OK)
            {
                if (dat2 != null)
                    dat2.Close();
                listBox1.Items.Clear();
                dat2 = new DatFile();
                dat2.Open(OD.FileName);
                foreach (string i in dat.Files.Keys)
                {
                    listBox1.Items.Add(i);
                }
                button6.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (FD.ShowDialog() == DialogResult.OK)
            {
                foreach (string i in dat2.Files.Keys)
                {
                    try
                    {
                        if (!dat.Files.ContainsKey(i))
                            dat2.Extract(i, FD.SelectedPath);
                    }
                    catch { }
                }
                MessageBox.Show("Finished");
            }
        }
    }
}
