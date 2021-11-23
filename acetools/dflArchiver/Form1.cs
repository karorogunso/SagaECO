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

  
    }
}
