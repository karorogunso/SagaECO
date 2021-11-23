using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ObfuscatorHelper
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> mapping = new Dictionary<string, string>();
        Dictionary<string, Dictionary<string, string>> mappingM = new Dictionary<string, Dictionary<string, string>>();
                
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (OD.ShowDialog()== DialogResult.OK )               
            {
                mapping.Clear();
                XmlDocument xml = new XmlDocument();
                //try
                //{
                    XmlElement root;
                    XmlNodeList list;
                    xml.Load(OD.FileName);
                    root = xml["dotfuscatorMap"];
                    list = root.ChildNodes;
                    
                    foreach (object j in list)
                    {
                        XmlElement i;
                        if (j.GetType() != typeof(XmlElement)) continue;
                        i = (XmlElement)j;

                        switch (i.Name.ToLower())
                        {
                            case "mapping":
                                XmlNodeList list2 = i.ChildNodes;
                                foreach (object l in list2)
                                {
                                    XmlElement k;
                                    if (l.GetType() != typeof(XmlElement)) continue;
                                    k = (XmlElement)l;
                                    switch (k.Name.ToLower())
                                    {
                                        case "module":
                                            XmlNodeList list3=k.ChildNodes;
                                            foreach(object obj in list3)
                                            {
                                                XmlElement obj2;
                                                if (obj.GetType() != typeof(XmlElement)) continue;
                                                obj2 = (XmlElement)obj;
                                                switch (obj2.Name.ToLower())
                                                {                                                    
                                                    case "type":
                                                        XmlNodeList list4 = obj2.ChildNodes;
                                                        string name = "";
                                                        string newname = "";
                                                        foreach (object obj3 in list4)
                                                        {
                                                            XmlElement obj4;
                                                            if (obj3.GetType() != typeof(XmlElement)) continue;
                                                            obj4 = (XmlElement)obj3;
                                                            switch (obj4.Name.ToLower())
                                                            {
                                                                case "name":
                                                                    name = obj4.InnerText;
                                                                    break;
                                                                case "newname":
                                                                    newname = obj4.InnerText;
                                                                    break;
                                                                case "methodlist":
                                                                case "fieldlist":
                                                                    {
                                                                        XmlNodeList list5 = obj4.ChildNodes;
                                                                        foreach (object obj5 in list5)
                                                                        {
                                                                            XmlElement obj6;
                                                                            if (obj5.GetType() != typeof(XmlElement)) continue;
                                                                            obj6 = (XmlElement)obj5;
                                                                            switch (obj6.Name.ToLower())
                                                                            {
                                                                                case "method":
                                                                                case "field":
                                                                                    XmlNodeList list6 = obj6.ChildNodes;
                                                                                    string name2 = "";
                                                                                    string newname2 = "";
                                                                                    foreach (object obj7 in list6)
                                                                                    {
                                                                                        XmlElement obj8;
                                                                                        if (obj7.GetType() != typeof(XmlElement)) continue;
                                                                                        obj8 = (XmlElement)obj7;
                                                                                        switch (obj8.Name.ToLower())
                                                                                        {
                                                                                            case "name":
                                                                                                name2 = obj8.InnerText;
                                                                                                break;
                                                                                            case "newname":
                                                                                                newname2 = obj8.InnerText;
                                                                                                break;
                                                                                        }
                                                                                    }
                                                                                    if (newname != "")
                                                                                    {
                                                                                        Dictionary<string, string> listM;
                                                                                        if (mappingM.ContainsKey(name))
                                                                                            listM = mappingM[name];
                                                                                        else
                                                                                        {
                                                                                            listM = new Dictionary<string, string>();
                                                                                            mappingM.Add(name, listM);
                                                                                        }
                                                                                        if (newname2 != "" && !listM.ContainsKey(newname2))
                                                                                            listM.Add(newname2, name2);
                                                                                    }
                                                                                    break;
                                                                            }
                                                                        }                                                                        
                                                                        break;
                                                                    }
                                                                    break;
                                                            }
                                                        }
                                                        if (newname != "")
                                                            mapping.Add(newname, name);
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                                break;
                        }
                       
                        
                    }
               // }
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString());
                //}
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] lines = tb_Src.Text.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Replace("\r", "");
                string[] tokens = line.Split('.');
                bool isfiled = false;
                string className = "";
                    
                for (int j = 0; j < tokens.Length; j++)
                {
                    string[] tmp = tokens[j].Split(" ()".ToCharArray());
                    foreach (string token in tmp)
                    {
                        if (token.Length > 5 || token == "")
                            continue;
                        if (!isfiled)
                        {
                            if (mapping.ContainsKey(token))
                            {
                                tokens[j] = tokens[j].Replace(token, mapping[token]);
                                isfiled = true;
                                className = mapping[token];
                            }
                        }
                        else
                        {
                            if (mappingM.ContainsKey(className))
                            {
                                if (mappingM[className].ContainsKey(token))
                                    tokens[j] = tokens[j].Replace(token, mappingM[className][token]);
                            }
                        }
                    }
                }
                line = "";
                foreach (string j in tokens)
                {
                    line += (j + ".");
                }
                line = line.Remove(line.Length - 1);
                lines[i] = line;
            }
            string res = "";
            foreach (string i in lines)
            {
                res += (i + "\r\n");
            }
            tb_Dst.Text = res;
        }
    }
}
