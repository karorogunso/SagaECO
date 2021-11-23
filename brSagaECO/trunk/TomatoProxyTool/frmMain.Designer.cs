namespace TomatoProxyTool
{
    public partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.ToolPortBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ServerIPBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Launch = new System.Windows.Forms.Button();
            this.Stop_Click = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.PacketDataBox = new System.Windows.Forms.TextBox();
            this.autofollow = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PacketInfoBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PacketsList = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.SendRB = new System.Windows.Forms.RadioButton();
            this.ReceiveRB = new System.Windows.Forms.RadioButton();
            this.SendPacket = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button4 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.MapRB = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.FBD = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "程序端口";
            // 
            // ToolPortBox
            // 
            this.ToolPortBox.Location = new System.Drawing.Point(67, 12);
            this.ToolPortBox.Name = "ToolPortBox";
            this.ToolPortBox.Size = new System.Drawing.Size(40, 21);
            this.ToolPortBox.TabIndex = 1;
            this.ToolPortBox.Text = "12000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "目标IP";
            // 
            // ServerIPBox
            // 
            this.ServerIPBox.Location = new System.Drawing.Point(158, 12);
            this.ServerIPBox.Name = "ServerIPBox";
            this.ServerIPBox.Size = new System.Drawing.Size(132, 21);
            this.ServerIPBox.TabIndex = 3;
            this.ServerIPBox.Text = "127.0.0.1:12006";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.Launch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ServerIPBox);
            this.groupBox1.Controls.Add(this.ToolPortBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(473, 37);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "日服",
            "本地"});
            this.comboBox1.Location = new System.Drawing.Point(370, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(91, 20);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Launch
            // 
            this.Launch.Location = new System.Drawing.Point(296, 12);
            this.Launch.Name = "Launch";
            this.Launch.Size = new System.Drawing.Size(68, 21);
            this.Launch.TabIndex = 4;
            this.Launch.Text = "启动";
            this.Launch.UseVisualStyleBackColor = true;
            this.Launch.Click += new System.EventHandler(this.Launch_Click);
            // 
            // Stop_Click
            // 
            this.Stop_Click.Enabled = false;
            this.Stop_Click.Location = new System.Drawing.Point(491, 23);
            this.Stop_Click.Name = "Stop_Click";
            this.Stop_Click.Size = new System.Drawing.Size(69, 21);
            this.Stop_Click.TabIndex = 5;
            this.Stop_Click.Text = "停止";
            this.Stop_Click.UseVisualStyleBackColor = true;
            this.Stop_Click.Click += new System.EventHandler(this.Stop_Click_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.PacketDataBox);
            this.groupBox2.Controls.Add(this.autofollow);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.PacketInfoBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.PacketsList);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(12, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(499, 340);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "预览窗口";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(155, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "data";
            // 
            // PacketDataBox
            // 
            this.PacketDataBox.AcceptsReturn = true;
            this.PacketDataBox.AcceptsTab = true;
            this.PacketDataBox.Location = new System.Drawing.Point(158, 115);
            this.PacketDataBox.Multiline = true;
            this.PacketDataBox.Name = "PacketDataBox";
            this.PacketDataBox.Size = new System.Drawing.Size(335, 165);
            this.PacketDataBox.TabIndex = 10;
            this.PacketDataBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PacketDataBox_MouseUp);
            // 
            // autofollow
            // 
            this.autofollow.AutoSize = true;
            this.autofollow.Checked = true;
            this.autofollow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autofollow.Location = new System.Drawing.Point(67, 14);
            this.autofollow.Name = "autofollow";
            this.autofollow.Size = new System.Drawing.Size(72, 16);
            this.autofollow.TabIndex = 9;
            this.autofollow.Text = "更新跟随";
            this.autofollow.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(158, 298);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(88, 33);
            this.textBox1.TabIndex = 8;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(90, 315);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Text = "接收";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(13, 315);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "发送";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(155, 282);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "选择翻译";
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(252, 298);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(238, 33);
            this.textBox4.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "封包内容";
            // 
            // PacketInfoBox
            // 
            this.PacketInfoBox.AcceptsReturn = true;
            this.PacketInfoBox.AcceptsTab = true;
            this.PacketInfoBox.Location = new System.Drawing.Point(158, 30);
            this.PacketInfoBox.Multiline = true;
            this.PacketInfoBox.Name = "PacketInfoBox";
            this.PacketInfoBox.Size = new System.Drawing.Size(335, 69);
            this.PacketInfoBox.TabIndex = 2;
            this.PacketInfoBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PacketInfoBox_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "捕获列表";
            // 
            // PacketsList
            // 
            this.PacketsList.FormattingEnabled = true;
            this.PacketsList.ItemHeight = 12;
            this.PacketsList.Location = new System.Drawing.Point(11, 30);
            this.PacketsList.Name = "PacketsList";
            this.PacketsList.Size = new System.Drawing.Size(129, 280);
            this.PacketsList.TabIndex = 0;
            this.PacketsList.SelectedIndexChanged += new System.EventHandler(this.PacketsList_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.checkBox3);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.SendPacket);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.MapRB);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(11, 402);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(864, 81);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "操作";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(273, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 21);
            this.button2.TabIndex = 12;
            this.button2.Text = "移至参照";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.checkBox4);
            this.groupBox5.Location = new System.Drawing.Point(148, 35);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox5.Size = new System.Drawing.Size(194, 32);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(85, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 21);
            this.button1.TabIndex = 11;
            this.button1.Text = "放行下一个封包";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(7, 11);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(72, 16);
            this.checkBox4.TabIndex = 10;
            this.checkBox4.Text = "暂停接收";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(149, 17);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(120, 16);
            this.checkBox3.TabIndex = 8;
            this.checkBox3.Text = "每份封包等待0.3s";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.SendRB);
            this.groupBox4.Controls.Add(this.ReceiveRB);
            this.groupBox4.Location = new System.Drawing.Point(354, 34);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox4.Size = new System.Drawing.Size(120, 32);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            // 
            // SendRB
            // 
            this.SendRB.AutoSize = true;
            this.SendRB.Enabled = false;
            this.SendRB.Location = new System.Drawing.Point(5, 11);
            this.SendRB.Name = "SendRB";
            this.SendRB.Size = new System.Drawing.Size(47, 16);
            this.SendRB.TabIndex = 6;
            this.SendRB.Text = "发送";
            this.SendRB.UseVisualStyleBackColor = true;
            // 
            // ReceiveRB
            // 
            this.ReceiveRB.AutoSize = true;
            this.ReceiveRB.Checked = true;
            this.ReceiveRB.Location = new System.Drawing.Point(60, 11);
            this.ReceiveRB.Name = "ReceiveRB";
            this.ReceiveRB.Size = new System.Drawing.Size(47, 16);
            this.ReceiveRB.TabIndex = 7;
            this.ReceiveRB.TabStop = true;
            this.ReceiveRB.Text = "接收";
            this.ReceiveRB.UseVisualStyleBackColor = true;
            // 
            // SendPacket
            // 
            this.SendPacket.Location = new System.Drawing.Point(480, 42);
            this.SendPacket.Name = "SendPacket";
            this.SendPacket.Size = new System.Drawing.Size(68, 22);
            this.SendPacket.TabIndex = 8;
            this.SendPacket.Text = "模拟发送";
            this.SendPacket.UseVisualStyleBackColor = true;
            this.SendPacket.Click += new System.EventHandler(this.SendPacket_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(6, 41);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(135, 20);
            this.button5.TabIndex = 2;
            this.button5.Text = "过滤列表";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Enabled = false;
            this.radioButton2.Location = new System.Drawing.Point(493, 14);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(53, 16);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Login";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(77, 14);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(66, 21);
            this.button4.TabIndex = 1;
            this.button4.Text = " 清空列表";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(355, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "目标服务器:";
            // 
            // MapRB
            // 
            this.MapRB.AutoSize = true;
            this.MapRB.Checked = true;
            this.MapRB.Location = new System.Drawing.Point(431, 15);
            this.MapRB.Name = "MapRB";
            this.MapRB.Size = new System.Drawing.Size(59, 16);
            this.MapRB.TabIndex = 4;
            this.MapRB.TabStop = true;
            this.MapRB.Text = "Server";
            this.MapRB.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(65, 21);
            this.button3.TabIndex = 0;
            this.button3.Text = "保存封包";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBox5);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.textBox6);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.textBox3);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.textBox2);
            this.groupBox6.Location = new System.Drawing.Point(525, 57);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(350, 339);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "参照窗口";
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(9, 298);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(88, 33);
            this.textBox5.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 282);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 17;
            this.label10.Text = "选择翻译";
            // 
            // textBox6
            // 
            this.textBox6.Enabled = false;
            this.textBox6.Location = new System.Drawing.Point(103, 298);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(238, 33);
            this.textBox6.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "封包内容";
            // 
            // textBox3
            // 
            this.textBox3.AcceptsReturn = true;
            this.textBox3.AcceptsTab = true;
            this.textBox3.Location = new System.Drawing.Point(9, 30);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(335, 69);
            this.textBox3.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "data";
            // 
            // textBox2
            // 
            this.textBox2.AcceptsReturn = true;
            this.textBox2.AcceptsTab = true;
            this.textBox2.Location = new System.Drawing.Point(6, 114);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(335, 165);
            this.textBox2.TabIndex = 12;
            this.textBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.textBox2_MouseUp);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(566, 22);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(116, 21);
            this.button6.TabIndex = 8;
            this.button6.Text = "加载模拟器封包";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 487);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.Stop_Click);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "封包分析器";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ToolPortBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ServerIPBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Stop_Click;
        private System.Windows.Forms.Button Launch;
        private System.Windows.Forms.GroupBox groupBox2;

        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox PacketInfoBox;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ListBox PacketsList;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button SendPacket;
        private System.Windows.Forms.RadioButton ReceiveRB;
        private System.Windows.Forms.RadioButton SendRB;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton MapRB;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.CheckBox autofollow;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox PacketDataBox;
        public System.Windows.Forms.CheckBox checkBox3;
        public System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button6;
        public System.Windows.Forms.FolderBrowserDialog FBD;
    }
}

