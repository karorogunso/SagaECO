namespace EffectTranslateTool
{
    partial class frmMain
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
            this.btnOfficialssp2csv = new System.Windows.Forms.Button();
            this.btnOfficialcsv2ssp = new System.Windows.Forms.Button();
            this.OpenSsp = new System.Windows.Forms.OpenFileDialog();
            this.SaveCsv = new System.Windows.Forms.SaveFileDialog();
            this.OpenCsv = new System.Windows.Forms.OpenFileDialog();
            this.SaveSsp = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAcessp2csv = new System.Windows.Forms.Button();
            this.btnAcecsv2ssp = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOfficialssp2csv
            // 
            this.btnOfficialssp2csv.Location = new System.Drawing.Point(6, 20);
            this.btnOfficialssp2csv.Name = "btnOfficialssp2csv";
            this.btnOfficialssp2csv.Size = new System.Drawing.Size(75, 23);
            this.btnOfficialssp2csv.TabIndex = 0;
            this.btnOfficialssp2csv.Text = "ssp->csv";
            this.btnOfficialssp2csv.UseVisualStyleBackColor = true;
            this.btnOfficialssp2csv.Click += new System.EventHandler(this.btnOfficialssp2csv_Click);
            // 
            // btnOfficialcsv2ssp
            // 
            this.btnOfficialcsv2ssp.Location = new System.Drawing.Point(87, 20);
            this.btnOfficialcsv2ssp.Name = "btnOfficialcsv2ssp";
            this.btnOfficialcsv2ssp.Size = new System.Drawing.Size(75, 23);
            this.btnOfficialcsv2ssp.TabIndex = 1;
            this.btnOfficialcsv2ssp.Text = "csv->ssp";
            this.btnOfficialcsv2ssp.UseVisualStyleBackColor = true;
            this.btnOfficialcsv2ssp.Click += new System.EventHandler(this.btnOfficialcsv2ssp_Click);
            // 
            // OpenSsp
            // 
            this.OpenSsp.FileName = "effect.ssp";
            // 
            // SaveCsv
            // 
            this.SaveCsv.FileName = "SkillDB.csv";
            // 
            // OpenCsv
            // 
            this.OpenCsv.FileName = "effect.csv";
            // 
            // SaveSsp
            // 
            this.SaveSsp.FileName = "effect.ssp";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOfficialssp2csv);
            this.groupBox1.Controls.Add(this.btnOfficialcsv2ssp);
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 58);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "官方文档转换";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAcessp2csv);
            this.groupBox2.Controls.Add(this.btnAcecsv2ssp);
            this.groupBox2.Location = new System.Drawing.Point(3, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 58);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ace文档转换";
            // 
            // btnAcessp2csv
            // 
            this.btnAcessp2csv.Location = new System.Drawing.Point(6, 20);
            this.btnAcessp2csv.Name = "btnAcessp2csv";
            this.btnAcessp2csv.Size = new System.Drawing.Size(75, 23);
            this.btnAcessp2csv.TabIndex = 2;
            this.btnAcessp2csv.Text = "ssp->csv";
            this.btnAcessp2csv.UseVisualStyleBackColor = true;
            this.btnAcessp2csv.Click += new System.EventHandler(this.btnAcessp2csv_Click);
            // 
            // btnAcecsv2ssp
            // 
            this.btnAcecsv2ssp.Location = new System.Drawing.Point(87, 20);
            this.btnAcecsv2ssp.Name = "btnAcecsv2ssp";
            this.btnAcecsv2ssp.Size = new System.Drawing.Size(75, 23);
            this.btnAcecsv2ssp.TabIndex = 3;
            this.btnAcecsv2ssp.Text = "csv->ssp";
            this.btnAcecsv2ssp.UseVisualStyleBackColor = true;
            this.btnAcecsv2ssp.Click += new System.EventHandler(this.btnAcecsv2ssp_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(176, 128);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Skill.ssp 转换器";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOfficialssp2csv;
        private System.Windows.Forms.Button btnOfficialcsv2ssp;
        private System.Windows.Forms.OpenFileDialog OpenSsp;
        private System.Windows.Forms.SaveFileDialog SaveCsv;
        private System.Windows.Forms.OpenFileDialog OpenCsv;
        private System.Windows.Forms.SaveFileDialog SaveSsp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAcessp2csv;
        private System.Windows.Forms.Button btnAcecsv2ssp;
    }
}

