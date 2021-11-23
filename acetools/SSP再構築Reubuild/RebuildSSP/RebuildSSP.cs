using RebuildSSP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkillDB
{
    public partial class SkillDB : Form
    {
        public SkillDB()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Utility process = new Utility();
            Utility.Datapack d = new Utility.Datapack();
            Utility.SetUtility s = new Utility.SetUtility();
            int returncode;

            returncode=process.ReadSkillDB(out d);
            if (returncode == 0)
            {
                System.Windows.Forms.MessageBox.Show("讀取完成");
                s.OpenSetting(true);
                returncode=process.Writessp(d);
                if (returncode == 0)
                {
                    System.Windows.Forms.MessageBox.Show("寫入完成");
                }
                else if (returncode == -1)
                {
                    System.Windows.Forms.MessageBox.Show("沒有選擇檔案");
                }
            }
            else if (returncode == -1)
            {
                System.Windows.Forms.MessageBox.Show("檔案不存在");
            }
        }

        private void ConvertToCSV_Click(object sender, EventArgs e)
        {
            Utility process = new Utility();
            Utility.Datapack d = new Utility.Datapack();
            Utility.SetUtility s = new Utility.SetUtility();
            int returncode;

            s.OpenSetting(false);
            returncode = process.Readssp(out d);
            if (returncode == 0)
            {
                System.Windows.Forms.MessageBox.Show("讀取完成");
                returncode = process.WriteSkillDB(d);
                if (returncode == 0)
                {
                    System.Windows.Forms.MessageBox.Show("寫入完成");
                }
                else if (returncode == -1)
                {
                    System.Windows.Forms.MessageBox.Show("沒有選擇檔案");
                }
            }
            else if (returncode == -1)
            {
                System.Windows.Forms.MessageBox.Show("檔案不存在");
            }
        }
    }
}
