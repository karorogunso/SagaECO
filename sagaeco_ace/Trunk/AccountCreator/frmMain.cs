using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

using SagaLib;

namespace AccountCreator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Configuration.Instance.Initialization("./SagaAccount.xml");
            textBox4.Text = Configuration.Instance.DBHost;
            textBox5.Text = Configuration.Instance.DBPort.ToString();
            textBox6.Text = Configuration.Instance.DBUser;
            textBox7.Text = Configuration.Instance.DBPass;
            textBox8.Text = Configuration.Instance.DBName;

        }


        private string GetMD5(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buf = System.Text.Encoding.ASCII.GetBytes(str);
            buf = md5.ComputeHash(buf);
            return Conversions.bytes2HexString(buf);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox9.Text == "")
            {
                MessageBox.Show("请填写完整资料");
                return;
            }
            
            try
            {
                MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};", textBox8.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text));
                string sqlstr = string.Format("INSERT INTO `login`(`username`,`password`,`deletepass`,`gmlevel`) VALUES ('{0}','{1}','{2}',{3})", this.textBox1.Text, GetMD5(this.textBox2.Text), GetMD5(this.textBox3.Text),int.Parse(this.textBox9.Text));
                con.Open();
                DataSet tmp;
                tmp = MySql.Data.MySqlClient.MySqlHelper.ExecuteDataset(con, string.Format("SELECT count(*) FROM `login` WHERE `username` = '{0}'", textBox1.Text));
                if (tmp.Tables[0].Rows[0][0].ToString() != "0")
                {
                    MessageBox.Show("帐号已经存在.请重新填写");
                    return;
                }
                MySql.Data.MySqlClient.MySqlHelper.ExecuteNonQuery(con, sqlstr, null);
                MessageBox.Show("帐号添加成功!!");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};", textBox8.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text));
            //string sqlstr = string.Format("INSERT INTO `login`(`username`,`password`,`deletepass`,`gmlevel`) VALUES ('{0}','{1}','{2}',{3})", this.textBox1.Text, GetMD5(this.textBox2.Text), GetMD5(this.textBox3.Text), int.Parse(this.textBox9.Text));
            con.Open();
            DataSet tmp;
            tmp = MySql.Data.MySqlClient.MySqlHelper.ExecuteDataset(con,"SELECT * FROM `login`");
            
            for (int i = 0;i<tmp.Tables[0].Rows.Count;i++)
                listBox1.Items.Add(tmp.Tables[0].Rows[i]["account_id"].ToString() + "|" + tmp.Tables[0].Rows[i]["username"].ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};", textBox8.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text));
            string sqlstr = string.Format("DELETE FROM `login` WHERE account_id={0}", listBox1.SelectedItem.ToString().Split('|')[0] );
            con.Open();
            MySql.Data.MySqlClient.MySqlHelper.ExecuteNonQuery(con, sqlstr, null);
            sqlstr = string.Format("DELETE FROM `char` WHERE account_id={0}", listBox1.SelectedItem.ToString().Split('|')[0]);
            MySql.Data.MySqlClient.MySqlHelper.ExecuteNonQuery(con, sqlstr, null);
        }


    }
    public class Configuration : Singleton<Configuration>
    {
        string dbhost, dbuser, dbpass, dbname;
        int dbport;
        public string DBHost { get { return this.dbhost; } set { this.dbhost = value; } }
        public string DBUser { get { return this.dbuser; } set { this.dbuser = value; } }
        public string DBPass { get { return this.dbpass; } set { this.dbpass = value; } }
        public string DBName { get { return this.dbname; } set { this.dbname = value; } }
        public int DBPort { get { return this.dbport; } set { this.dbport = value; } }
        public void Initialization(string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                xml.Load(path);
                root = xml["SagaAccount"];
                list = root.ChildNodes;
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    switch (i.Name.ToLower())
                    {
                        case "dbhost":
                            this.dbhost = i.InnerText;
                            break;
                        case "dbport":
                            this.dbport = int.Parse(i.InnerText);
                            break;
                        case "dbuser":
                            this.dbuser = i.InnerText;
                            break;
                        case "dbpass":
                            this.dbpass = i.InnerText;
                            break;
                        case "dbname":
                            this.dbname = i.InnerText;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
