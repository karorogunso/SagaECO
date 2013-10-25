using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

using SagaLib;

namespace AccountCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};", "sagaeco", "127.0.0.001", "3306", "root", "lh630206"));
            string sqlstr = string.Format("INSERT INTO `login`(`username`,`password`,`deletepass`) VALUES ('{0}','{1}','{2}')", this.textBox1.Text, GetMD5(this.textBox2.Text), GetMD5(this.textBox3.Text));
            con.Open();
            MySql.Data.MySqlClient.MySqlHelper.ExecuteNonQuery(con, sqlstr, null);
                    
        }

        private string GetMD5(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buf = System.Text.Encoding.ASCII.GetBytes(str);
            buf = md5.ComputeHash(buf);
            return Conversions.bytes2HexString(buf);
        }
    }
}
