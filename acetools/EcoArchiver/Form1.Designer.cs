namespace EcoArchiver
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.OD = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.lst_File = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.cb_comp = new System.Windows.Forms.CheckBox();
            this.SD = new System.Windows.Forms.SaveFileDialog();
            this.FD = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // OD
            // 
            resources.ApplyResources(this.OD, "OD");
            // 
            // button1
            // 
            this.button1.AccessibleDescription = null;
            this.button1.AccessibleName = null;
            resources.ApplyResources(this.button1, "button1");
            this.button1.BackgroundImage = null;
            this.button1.Font = null;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lst_File
            // 
            this.lst_File.AccessibleDescription = null;
            this.lst_File.AccessibleName = null;
            resources.ApplyResources(this.lst_File, "lst_File");
            this.lst_File.BackgroundImage = null;
            this.lst_File.Font = null;
            this.lst_File.FormattingEnabled = true;
            this.lst_File.Name = "lst_File";
            this.lst_File.SelectedIndexChanged += new System.EventHandler(this.lst_File_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.AccessibleDescription = null;
            this.button2.AccessibleName = null;
            resources.ApplyResources(this.button2, "button2");
            this.button2.BackgroundImage = null;
            this.button2.Font = null;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.AccessibleDescription = null;
            this.button3.AccessibleName = null;
            resources.ApplyResources(this.button3, "button3");
            this.button3.BackgroundImage = null;
            this.button3.Font = null;
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.AccessibleDescription = null;
            this.button4.AccessibleName = null;
            resources.ApplyResources(this.button4, "button4");
            this.button4.BackgroundImage = null;
            this.button4.Font = null;
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // cb_comp
            // 
            this.cb_comp.AccessibleDescription = null;
            this.cb_comp.AccessibleName = null;
            resources.ApplyResources(this.cb_comp, "cb_comp");
            this.cb_comp.BackgroundImage = null;
            this.cb_comp.Checked = true;
            this.cb_comp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_comp.Font = null;
            this.cb_comp.Name = "cb_comp";
            this.cb_comp.UseVisualStyleBackColor = true;
            // 
            // SD
            // 
            resources.ApplyResources(this.SD, "SD");
            // 
            // FD
            // 
            resources.ApplyResources(this.FD, "FD");
            // 
            // Form1
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.cb_comp);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lst_File);
            this.Font = null;
            this.Icon = null;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OD;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lst_File;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox cb_comp;
        private System.Windows.Forms.SaveFileDialog SD;
        private System.Windows.Forms.FolderBrowserDialog FD;
    }
}

