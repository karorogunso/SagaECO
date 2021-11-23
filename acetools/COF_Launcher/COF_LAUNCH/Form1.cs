using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace COF_LAUNCH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void First_Load(object sender, EventArgs e)
        {
            RFPatch f = new RFPatch();
            f.acquire();
        }
    }
}
