namespace MAP
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.MAPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAPNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subordinate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Healing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wrp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dominion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FGarden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MAPID,
            this.MAPNAME,
            this.subordinate,
            this.Healing,
            this.Cold,
            this.Hot,
            this.Wet,
            this.Wrp,
            this.Dominion,
            this.FGarden});
            this.dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(747, 414);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "1.读取mapname.csv";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(210, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "2.读取MapFlags.xml";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(454, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(107, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "保存当前数据";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MAPID
            // 
            this.MAPID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MAPID.HeaderText = "地图ID";
            this.MAPID.Name = "MAPID";
            this.MAPID.ReadOnly = true;
            this.MAPID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MAPID.Width = 47;
            // 
            // MAPNAME
            // 
            this.MAPNAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MAPNAME.HeaderText = "地图名称";
            this.MAPNAME.Name = "MAPNAME";
            this.MAPNAME.ReadOnly = true;
            this.MAPNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MAPNAME.Width = 59;
            // 
            // subordinate
            // 
            this.subordinate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.subordinate.HeaderText = "从属";
            this.subordinate.Name = "subordinate";
            this.subordinate.ReadOnly = true;
            this.subordinate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.subordinate.Width = 35;
            // 
            // Healing
            // 
            this.Healing.HeaderText = "恢复";
            this.Healing.Name = "Healing";
            this.Healing.ReadOnly = true;
            this.Healing.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Healing.Width = 40;
            // 
            // Cold
            // 
            this.Cold.HeaderText = "寒冷";
            this.Cold.Name = "Cold";
            this.Cold.ReadOnly = true;
            this.Cold.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Cold.Width = 40;
            // 
            // Hot
            // 
            this.Hot.HeaderText = "炎热";
            this.Hot.Name = "Hot";
            this.Hot.ReadOnly = true;
            this.Hot.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Hot.Width = 40;
            // 
            // Wet
            // 
            this.Wet.HeaderText = "水下";
            this.Wet.Name = "Wet";
            this.Wet.ReadOnly = true;
            this.Wet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Wet.Width = 40;
            // 
            // Wrp
            // 
            this.Wrp.HeaderText = "Wrp";
            this.Wrp.Name = "Wrp";
            this.Wrp.ReadOnly = true;
            this.Wrp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Wrp.Width = 30;
            // 
            // Dominion
            // 
            this.Dominion.HeaderText = "Dominion";
            this.Dominion.Name = "Dominion";
            this.Dominion.ReadOnly = true;
            this.Dominion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Dominion.Width = 60;
            // 
            // FGarden
            // 
            this.FGarden.HeaderText = "FGarden";
            this.FGarden.Name = "FGarden";
            this.FGarden.ReadOnly = true;
            this.FGarden.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FGarden.Width = 55;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 442);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAPID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAPNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn subordinate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Healing;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cold;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wrp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dominion;
        private System.Windows.Forms.DataGridViewTextBoxColumn FGarden;
    }
}

