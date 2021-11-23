namespace GMTool
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSimpleQuery = new System.Windows.Forms.Button();
            this.tbSimpleQuery = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.chbOrderByMoney = new System.Windows.Forms.CheckBox();
            this.btnMoneyQuery = new System.Windows.Forms.Button();
            this.tbMoneyQuery = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.cbSQLSrc = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSQLQuery = new System.Windows.Forms.Button();
            this.tbSQLQuery = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbItemCharItemCount = new System.Windows.Forms.TextBox();
            this.btnItemCharAdd = new System.Windows.Forms.Button();
            this.tbItemCharItemIDName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lstItemUserName = new System.Windows.Forms.ListBox();
            this.btnItemSearch = new System.Windows.Forms.Button();
            this.tbItemNameQuery = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.btnItemDataAll = new System.Windows.Forms.Button();
            this.btnItemDataSearch = new System.Windows.Forms.Button();
            this.tbItemDataQuery = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.btnMobAll = new System.Windows.Forms.Button();
            this.btnMobSearch = new System.Windows.Forms.Button();
            this.tbMobQuery = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lan_CHS = new System.Windows.Forms.ToolStripMenuItem();
            this.lan_CHT = new System.Windows.Forms.ToolStripMenuItem();
            this.lan_JP = new System.Windows.Forms.ToolStripMenuItem();
            this.lan_EN = new System.Windows.Forms.ToolStripMenuItem();
            this.gvResult = new System.Windows.Forms.DataGridView();
            this.LstMsg = new System.Windows.Forms.ListBox();
            this.ItemCharMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刪除道具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.MoneyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.封鎖帳號ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvResult)).BeginInit();
            this.ItemCharMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.MoneyMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LstMsg);
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer2.Panel1.Controls.Add(this.menuStrip1);
            this.splitContainer2.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gvResult);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage8);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnSimpleQuery);
            this.tabPage1.Controls.Add(this.tbSimpleQuery);
            this.tabPage1.Controls.Add(this.label1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnSimpleQuery
            // 
            resources.ApplyResources(this.btnSimpleQuery, "btnSimpleQuery");
            this.btnSimpleQuery.Name = "btnSimpleQuery";
            this.btnSimpleQuery.UseVisualStyleBackColor = true;
            this.btnSimpleQuery.Click += new System.EventHandler(this.btnSimpleQuery_Click);
            // 
            // tbSimpleQuery
            // 
            resources.ApplyResources(this.tbSimpleQuery, "tbSimpleQuery");
            this.tbSimpleQuery.Name = "tbSimpleQuery";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.chbOrderByMoney);
            this.tabPage4.Controls.Add(this.btnMoneyQuery);
            this.tabPage4.Controls.Add(this.tbMoneyQuery);
            this.tabPage4.Controls.Add(this.label8);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.tabPage4.Click += new System.EventHandler(this.tabPage4_Click);
            this.tabPage4.Enter += new System.EventHandler(this.tabPage4_Enter);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // chbOrderByMoney
            // 
            resources.ApplyResources(this.chbOrderByMoney, "chbOrderByMoney");
            this.chbOrderByMoney.Name = "chbOrderByMoney";
            this.chbOrderByMoney.UseVisualStyleBackColor = true;
            // 
            // btnMoneyQuery
            // 
            resources.ApplyResources(this.btnMoneyQuery, "btnMoneyQuery");
            this.btnMoneyQuery.Name = "btnMoneyQuery";
            this.btnMoneyQuery.UseVisualStyleBackColor = true;
            this.btnMoneyQuery.Click += new System.EventHandler(this.btnMoneyQuery_Click);
            // 
            // tbMoneyQuery
            // 
            resources.ApplyResources(this.tbMoneyQuery, "tbMoneyQuery");
            this.tbMoneyQuery.Name = "tbMoneyQuery";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.cbSQLSrc);
            this.tabPage5.Controls.Add(this.label3);
            this.tabPage5.Controls.Add(this.btnSQLQuery);
            this.tabPage5.Controls.Add(this.tbSQLQuery);
            this.tabPage5.Controls.Add(this.label7);
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // cbSQLSrc
            // 
            this.cbSQLSrc.FormattingEnabled = true;
            this.cbSQLSrc.Items.AddRange(new object[] {
            resources.GetString("cbSQLSrc.Items"),
            resources.GetString("cbSQLSrc.Items1")});
            resources.ApplyResources(this.cbSQLSrc, "cbSQLSrc");
            this.cbSQLSrc.Name = "cbSQLSrc";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnSQLQuery
            // 
            resources.ApplyResources(this.btnSQLQuery, "btnSQLQuery");
            this.btnSQLQuery.Name = "btnSQLQuery";
            this.btnSQLQuery.UseVisualStyleBackColor = true;
            this.btnSQLQuery.Click += new System.EventHandler(this.btnSQLQuery_Click);
            // 
            // tbSQLQuery
            // 
            resources.ApplyResources(this.tbSQLQuery, "tbSQLQuery");
            this.tbSQLQuery.Name = "tbSQLQuery";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox1);
            this.tabPage6.Controls.Add(this.label11);
            this.tabPage6.Controls.Add(this.lstItemUserName);
            this.tabPage6.Controls.Add(this.btnItemSearch);
            this.tabPage6.Controls.Add(this.tbItemNameQuery);
            this.tabPage6.Controls.Add(this.label10);
            resources.ApplyResources(this.tabPage6, "tabPage6");
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbItemCharItemCount);
            this.groupBox1.Controls.Add(this.btnItemCharAdd);
            this.groupBox1.Controls.Add(this.tbItemCharItemIDName);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // tbItemCharItemCount
            // 
            resources.ApplyResources(this.tbItemCharItemCount, "tbItemCharItemCount");
            this.tbItemCharItemCount.Name = "tbItemCharItemCount";
            // 
            // btnItemCharAdd
            // 
            resources.ApplyResources(this.btnItemCharAdd, "btnItemCharAdd");
            this.btnItemCharAdd.Name = "btnItemCharAdd";
            this.btnItemCharAdd.UseVisualStyleBackColor = true;
            this.btnItemCharAdd.Click += new System.EventHandler(this.btnItemCharAdd_Click);
            // 
            // tbItemCharItemIDName
            // 
            this.tbItemCharItemIDName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbItemCharItemIDName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            resources.ApplyResources(this.tbItemCharItemIDName, "tbItemCharItemIDName");
            this.tbItemCharItemIDName.Name = "tbItemCharItemIDName";
            this.tbItemCharItemIDName.TextChanged += new System.EventHandler(this.tbItemCharItemIDName_TextChanged);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // lstItemUserName
            // 
            this.lstItemUserName.FormattingEnabled = true;
            resources.ApplyResources(this.lstItemUserName, "lstItemUserName");
            this.lstItemUserName.Name = "lstItemUserName";
            this.lstItemUserName.SelectedIndexChanged += new System.EventHandler(this.lstItemUserName_SelectedIndexChanged);
            // 
            // btnItemSearch
            // 
            resources.ApplyResources(this.btnItemSearch, "btnItemSearch");
            this.btnItemSearch.Name = "btnItemSearch";
            this.btnItemSearch.UseVisualStyleBackColor = true;
            this.btnItemSearch.Click += new System.EventHandler(this.btnItemSearch_Click);
            // 
            // tbItemNameQuery
            // 
            resources.ApplyResources(this.tbItemNameQuery, "tbItemNameQuery");
            this.tbItemNameQuery.Name = "tbItemNameQuery";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.btnItemDataAll);
            this.tabPage7.Controls.Add(this.btnItemDataSearch);
            this.tabPage7.Controls.Add(this.tbItemDataQuery);
            this.tabPage7.Controls.Add(this.label14);
            resources.ApplyResources(this.tabPage7, "tabPage7");
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // btnItemDataAll
            // 
            resources.ApplyResources(this.btnItemDataAll, "btnItemDataAll");
            this.btnItemDataAll.Name = "btnItemDataAll";
            this.btnItemDataAll.UseVisualStyleBackColor = true;
            this.btnItemDataAll.Click += new System.EventHandler(this.btnItemDataAll_Click);
            // 
            // btnItemDataSearch
            // 
            resources.ApplyResources(this.btnItemDataSearch, "btnItemDataSearch");
            this.btnItemDataSearch.Name = "btnItemDataSearch";
            this.btnItemDataSearch.UseVisualStyleBackColor = true;
            this.btnItemDataSearch.Click += new System.EventHandler(this.btnItemDataSearch_Click);
            // 
            // tbItemDataQuery
            // 
            resources.ApplyResources(this.tbItemDataQuery, "tbItemDataQuery");
            this.tbItemDataQuery.Name = "tbItemDataQuery";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.btnMobAll);
            this.tabPage8.Controls.Add(this.btnMobSearch);
            this.tabPage8.Controls.Add(this.tbMobQuery);
            this.tabPage8.Controls.Add(this.label15);
            resources.ApplyResources(this.tabPage8, "tabPage8");
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.UseVisualStyleBackColor = true;
            this.tabPage8.Enter += new System.EventHandler(this.tabPage8_Enter);
            // 
            // btnMobAll
            // 
            resources.ApplyResources(this.btnMobAll, "btnMobAll");
            this.btnMobAll.Name = "btnMobAll";
            this.btnMobAll.UseVisualStyleBackColor = true;
            this.btnMobAll.Click += new System.EventHandler(this.btnMobAll_Click);
            // 
            // btnMobSearch
            // 
            resources.ApplyResources(this.btnMobSearch, "btnMobSearch");
            this.btnMobSearch.Name = "btnMobSearch";
            this.btnMobSearch.UseVisualStyleBackColor = true;
            this.btnMobSearch.Click += new System.EventHandler(this.btnMobSearch_Click);
            // 
            // tbMobQuery
            // 
            resources.ApplyResources(this.tbMobQuery, "tbMobQuery");
            this.tbMobQuery.Name = "tbMobQuery";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lan_CHS,
            this.lan_CHT,
            this.lan_JP,
            this.lan_EN});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            // 
            // lan_CHS
            // 
            this.lan_CHS.Name = "lan_CHS";
            resources.ApplyResources(this.lan_CHS, "lan_CHS");
            this.lan_CHS.Click += new System.EventHandler(this.lan_CHS_Click);
            // 
            // lan_CHT
            // 
            this.lan_CHT.Name = "lan_CHT";
            resources.ApplyResources(this.lan_CHT, "lan_CHT");
            this.lan_CHT.Click += new System.EventHandler(this.lan_CHT_Click);
            // 
            // lan_JP
            // 
            this.lan_JP.Name = "lan_JP";
            resources.ApplyResources(this.lan_JP, "lan_JP");
            this.lan_JP.Click += new System.EventHandler(this.lan_JP_Click);
            // 
            // lan_EN
            // 
            this.lan_EN.Name = "lan_EN";
            resources.ApplyResources(this.lan_EN, "lan_EN");
            this.lan_EN.Click += new System.EventHandler(this.lan_EN_Click);
            // 
            // gvResult
            // 
            this.gvResult.AllowUserToAddRows = false;
            this.gvResult.AllowUserToDeleteRows = false;
            this.gvResult.AllowUserToOrderColumns = true;
            this.gvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.gvResult, "gvResult");
            this.gvResult.Name = "gvResult";
            this.gvResult.ReadOnly = true;
            this.gvResult.RowTemplate.Height = 24;
            this.gvResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvResult_CellContentClick);
            this.gvResult.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gvResult_KeyUp);
            this.gvResult.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvResult_MouseUp);
            // 
            // LstMsg
            // 
            resources.ApplyResources(this.LstMsg, "LstMsg");
            this.LstMsg.FormattingEnabled = true;
            this.LstMsg.Name = "LstMsg";
            // 
            // ItemCharMenu
            // 
            this.ItemCharMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刪除道具ToolStripMenuItem});
            this.ItemCharMenu.Name = "ItemCharMenu";
            resources.ApplyResources(this.ItemCharMenu, "ItemCharMenu");
            // 
            // 刪除道具ToolStripMenuItem
            // 
            this.刪除道具ToolStripMenuItem.Name = "刪除道具ToolStripMenuItem";
            resources.ApplyResources(this.刪除道具ToolStripMenuItem, "刪除道具ToolStripMenuItem");
            this.刪除道具ToolStripMenuItem.Click += new System.EventHandler(this.刪除道具ToolStripMenuItem_Click);
            // 
            // MoneyMenu
            // 
            this.MoneyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.封鎖帳號ToolStripMenuItem});
            this.MoneyMenu.Name = "MoneyMenu";
            resources.ApplyResources(this.MoneyMenu, "MoneyMenu");
            // 
            // 封鎖帳號ToolStripMenuItem
            // 
            this.封鎖帳號ToolStripMenuItem.Name = "封鎖帳號ToolStripMenuItem";
            resources.ApplyResources(this.封鎖帳號ToolStripMenuItem, "封鎖帳號ToolStripMenuItem");
            this.封鎖帳號ToolStripMenuItem.Click += new System.EventHandler(this.封鎖帳號ToolStripMenuItem_Click);
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvResult)).EndInit();
            this.ItemCharMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.MoneyMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView gvResult;
        private System.Windows.Forms.ListBox LstMsg;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnSimpleQuery;
        private System.Windows.Forms.TextBox tbSimpleQuery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbSQLQuery;
        private System.Windows.Forms.Button btnSQLQuery;
        private System.Windows.Forms.Button btnMoneyQuery;
        private System.Windows.Forms.TextBox tbMoneyQuery;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chbOrderByMoney;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnItemSearch;
        private System.Windows.Forms.TextBox tbItemNameQuery;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox lstItemUserName;
        private System.Windows.Forms.ContextMenuStrip ItemCharMenu;
        private System.Windows.Forms.ToolStripMenuItem 刪除道具ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbItemCharItemIDName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbItemCharItemCount;
        private System.Windows.Forms.Button btnItemCharAdd;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TextBox tbItemDataQuery;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Button btnItemDataSearch;
        private System.Windows.Forms.Button btnItemDataAll;
        private System.Windows.Forms.Button btnMobAll;
        private System.Windows.Forms.Button btnMobSearch;
        private System.Windows.Forms.TextBox tbMobQuery;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lan_CHS;
        private System.Windows.Forms.ToolStripMenuItem lan_CHT;
        private System.Windows.Forms.ToolStripMenuItem lan_JP;
        private System.Windows.Forms.ToolStripMenuItem lan_EN;
        private System.Windows.Forms.ContextMenuStrip MoneyMenu;
        private System.Windows.Forms.ToolStripMenuItem 封鎖帳號ToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbSQLSrc;
        private System.Windows.Forms.Label label3;


    }
}

