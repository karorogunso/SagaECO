using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Collections.Generic;
using COF_LAUNCH_SERVER_Lib;
using System.Xml;
namespace COF_LAUNCH_SERVER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lst_Patch.Items.Clear();
                List<COF_LAUNCH_SERVER_Lib.Files> updatafiles = new System.Collections.Generic.List<COF_LAUNCH_SERVER_Lib.Files>();
                string[] files = System.IO.Directory.GetFiles(FD.SelectedPath, "*.*", System.IO.SearchOption.AllDirectories);
                string path = FD.SelectedPath;
                string outputPath = "";
                foreach (string i in files)
                {
                    MD5 md5 = MD5.Create();
                    System.IO.FileStream fs = new System.IO.FileStream(i, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    string hash = COF_LAUNCH_SERVER_Lib.Conversions.bytes2HexString(md5.ComputeHash(fs));
                    Files file = new Files();
                    file.name = i.Replace(path + "\\", "");
                    file.path = path + "\\";
                    file.md5 = hash;
                    updatafiles.Add(file);
                    fs.Close();
                    lst_Patch.Items.Add(string.Format("{0},MD5:{1}", i.Replace(path + "\\", ""), hash));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            
            doc.AppendChild(root);
            XmlElement Child1 = doc.CreateElement("attr1");
            XmlAttribute attr1 = doc.CreateAttribute("attr1");
            attr1.Value = "arrt1Content";

            Child1.Attributes.Append(attr1);
            root.AppendChild(Child1);
            doc.Save("1.xml");
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
