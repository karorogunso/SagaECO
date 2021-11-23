namespace SagaECOConfigurator
{
    partial class frm_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_English = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_Login_Password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Login_Port = new System.Windows.Forms.TextBox();
            this.cb_Login_Debug = new System.Windows.Forms.CheckBox();
            this.cb_Login_SQL = new System.Windows.Forms.CheckBox();
            this.cb_Login_Error = new System.Windows.Forms.CheckBox();
            this.cb_Login_Warning = new System.Windows.Forms.CheckBox();
            this.cb_Login_Info = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb_Login_Emil = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(649, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_English});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.fileToolStripMenuItem.Text = "Language";
            // 
            // menu_English
            // 
            this.menu_English.Checked = true;
            this.menu_English.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menu_English.Name = "menu_English";
            this.menu_English.Size = new System.Drawing.Size(112, 22);
            this.menu_English.Text = "English";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(5, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(643, 325);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(635, 299);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SagaLogin";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_Login_Password);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_Login_Port);
            this.groupBox1.Controls.Add(this.cb_Login_Debug);
            this.groupBox1.Controls.Add(this.cb_Login_SQL);
            this.groupBox1.Controls.Add(this.cb_Login_Error);
            this.groupBox1.Controls.Add(this.cb_Login_Warning);
            this.groupBox1.Controls.Add(this.cb_Login_Info);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 218);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Settings";
            // 
            // tb_Login_Password
            // 
            this.tb_Login_Password.Location = new System.Drawing.Point(29, 188);
            this.tb_Login_Password.Name = "tb_Login_Password";
            this.tb_Login_Password.Size = new System.Drawing.Size(118, 20);
            this.tb_Login_Password.TabIndex = 5;
            this.tb_Login_Password.Text = "saga";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Communication Password:";
            // 
            // tb_Login_Port
            // 
            this.tb_Login_Port.Location = new System.Drawing.Point(76, 12);
            this.tb_Login_Port.Name = "tb_Login_Port";
            this.tb_Login_Port.Size = new System.Drawing.Size(71, 20);
            this.tb_Login_Port.TabIndex = 3;
            this.tb_Login_Port.Text = "12000";
            // 
            // cb_Login_Debug
            // 
            this.cb_Login_Debug.AutoSize = true;
            this.cb_Login_Debug.Checked = true;
            this.cb_Login_Debug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Login_Debug.Location = new System.Drawing.Point(29, 152);
            this.cb_Login_Debug.Name = "cb_Login_Debug";
            this.cb_Login_Debug.Size = new System.Drawing.Size(110, 17);
            this.cb_Login_Debug.TabIndex = 2;
            this.cb_Login_Debug.Text = "[Debug] Message";
            this.cb_Login_Debug.UseVisualStyleBackColor = true;
            // 
            // cb_Login_SQL
            // 
            this.cb_Login_SQL.AutoSize = true;
            this.cb_Login_SQL.Checked = true;
            this.cb_Login_SQL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Login_SQL.Location = new System.Drawing.Point(29, 129);
            this.cb_Login_SQL.Name = "cb_Login_SQL";
            this.cb_Login_SQL.Size = new System.Drawing.Size(99, 17);
            this.cb_Login_SQL.TabIndex = 2;
            this.cb_Login_SQL.Text = "[SQL] Message";
            this.cb_Login_SQL.UseVisualStyleBackColor = true;
            // 
            // cb_Login_Error
            // 
            this.cb_Login_Error.AutoSize = true;
            this.cb_Login_Error.Checked = true;
            this.cb_Login_Error.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Login_Error.Location = new System.Drawing.Point(29, 106);
            this.cb_Login_Error.Name = "cb_Login_Error";
            this.cb_Login_Error.Size = new System.Drawing.Size(100, 17);
            this.cb_Login_Error.TabIndex = 2;
            this.cb_Login_Error.Text = "[Error] Message";
            this.cb_Login_Error.UseVisualStyleBackColor = true;
            // 
            // cb_Login_Warning
            // 
            this.cb_Login_Warning.AutoSize = true;
            this.cb_Login_Warning.Checked = true;
            this.cb_Login_Warning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Login_Warning.Location = new System.Drawing.Point(29, 83);
            this.cb_Login_Warning.Name = "cb_Login_Warning";
            this.cb_Login_Warning.Size = new System.Drawing.Size(118, 17);
            this.cb_Login_Warning.TabIndex = 2;
            this.cb_Login_Warning.Text = "[Warning] Message";
            this.cb_Login_Warning.UseVisualStyleBackColor = true;
            // 
            // cb_Login_Info
            // 
            this.cb_Login_Info.AutoSize = true;
            this.cb_Login_Info.Checked = true;
            this.cb_Login_Info.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Login_Info.Location = new System.Drawing.Point(29, 60);
            this.cb_Login_Info.Name = "cb_Login_Info";
            this.cb_Login_Info.Size = new System.Drawing.Size(96, 17);
            this.cb_Login_Info.TabIndex = 2;
            this.cb_Login_Info.Text = "[Info] Message";
            this.cb_Login_Info.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Log Level:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Listen Port:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(635, 299);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SagaMap";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.rb_Login_Emil);
            this.groupBox2.Location = new System.Drawing.Point(180, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(207, 229);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Start Status";
            // 
            // rb_Login_Emil
            // 
            this.rb_Login_Emil.AutoSize = true;
            this.rb_Login_Emil.Checked = true;
            this.rb_Login_Emil.Location = new System.Drawing.Point(16, 20);
            this.rb_Login_Emil.Name = "rb_Login_Emil";
            this.rb_Login_Emil.Size = new System.Drawing.Size(44, 17);
            this.rb_Login_Emil.TabIndex = 0;
            this.rb_Login_Emil.TabStop = true;
            this.rb_Login_Emil.Text = "Emil";
            this.rb_Login_Emil.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(66, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(57, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "Titania";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(129, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(69, 17);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.Text = "Dominion";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 362);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SagaECO Configurator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_English;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_Login_Warning;
        private System.Windows.Forms.CheckBox cb_Login_Info;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cb_Login_Error;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_Login_Port;
        private System.Windows.Forms.CheckBox cb_Login_Debug;
        private System.Windows.Forms.CheckBox cb_Login_SQL;
        private System.Windows.Forms.TextBox tb_Login_Password;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rb_Login_Emil;
    }
}

