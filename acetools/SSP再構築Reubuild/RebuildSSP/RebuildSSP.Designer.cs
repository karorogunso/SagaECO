namespace SkillDB
{
    partial class SkillDB
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ConvertToSSP = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ConvertToCSV = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConvertToSSP
            // 
            this.ConvertToSSP.Location = new System.Drawing.Point(153, 12);
            this.ConvertToSSP.Name = "ConvertToSSP";
            this.ConvertToSSP.Size = new System.Drawing.Size(89, 23);
            this.ConvertToSSP.TabIndex = 0;
            this.ConvertToSSP.Text = "Convert to ssp";
            this.ConvertToSSP.UseVisualStyleBackColor = true;
            this.ConvertToSSP.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "2016 ECOAce Project";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "For knowledge exchange uses ONLY";
            // 
            // ConvertToCSV
            // 
            this.ConvertToCSV.Location = new System.Drawing.Point(37, 12);
            this.ConvertToCSV.Name = "ConvertToCSV";
            this.ConvertToCSV.Size = new System.Drawing.Size(89, 23);
            this.ConvertToCSV.TabIndex = 3;
            this.ConvertToCSV.Text = "Convert to csv";
            this.ConvertToCSV.UseVisualStyleBackColor = true;
            this.ConvertToCSV.Click += new System.EventHandler(this.ConvertToCSV_Click);
            // 
            // SkillDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 93);
            this.Controls.Add(this.ConvertToCSV);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConvertToSSP);
            this.Name = "SkillDB";
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.Text = "ssp 再構築";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConvertToSSP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ConvertToCSV;
    }
}

