namespace Random_Warp
{
    partial class Form1
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLoad = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Load_Message = new System.Windows.Forms.MaskedTextBox();
            this.Start = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.MapName_Message = new System.Windows.Forms.MaskedTextBox();
            this.btnLoad_MapName = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(259, 40);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "瀏覽...";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Warp檔案位置";
            // 
            // Load_Message
            // 
            this.Load_Message.Location = new System.Drawing.Point(17, 40);
            this.Load_Message.Name = "Load_Message";
            this.Load_Message.Size = new System.Drawing.Size(236, 22);
            this.Load_Message.TabIndex = 3;
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(259, 138);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 4;
            this.Start.Text = "轉換開始";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "warp.csv";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "地圖傳送點.cs";
            // 
            // MapName_Message
            // 
            this.MapName_Message.Location = new System.Drawing.Point(17, 99);
            this.MapName_Message.Name = "MapName_Message";
            this.MapName_Message.Size = new System.Drawing.Size(236, 22);
            this.MapName_Message.TabIndex = 5;
            // 
            // btnLoad_MapName
            // 
            this.btnLoad_MapName.Location = new System.Drawing.Point(259, 99);
            this.btnLoad_MapName.Name = "btnLoad_MapName";
            this.btnLoad_MapName.Size = new System.Drawing.Size(75, 23);
            this.btnLoad_MapName.TabIndex = 6;
            this.btnLoad_MapName.Text = "瀏覽...";
            this.btnLoad_MapName.UseVisualStyleBackColor = true;
            this.btnLoad_MapName.Click += new System.EventHandler(this.btnLoad_MapName_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Map Name檔案位置";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "mapname.csv";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 173);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLoad_MapName);
            this.Controls.Add(this.MapName_Message);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Load_Message);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoad);
            this.Name = "Form1";
            this.Text = "Warp檔案轉換工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox Load_Message;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MaskedTextBox MapName_Message;
        private System.Windows.Forms.Button btnLoad_MapName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}

