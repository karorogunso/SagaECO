namespace SagaECOLoader
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
            try
            {
                base.Dispose(disposing);
            }
            catch (System.Exception)
            { }
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.nfIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenMainWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.啟動伺服器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sagaLoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新啟動伺服器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmuSagaLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.cmuSagaLoginStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.啟動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新啟動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmuSagaMap = new System.Windows.Forms.ToolStripMenuItem();
            this.cmuSagaMapStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.啟動ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.停止ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.重新啟動ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.關於SagaECOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.伺服器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.muStartLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.muSagaMap = new System.Windows.Forms.ToolStripMenuItem();
            this.關閉LoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.關閉MapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.啟動ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.停止ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.重新啟動ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.sagaMApToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.啟動ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.停止ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.重新啟動ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.關閉SagaECOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoStartServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.enableColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableSagaLoginOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sagaMapOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SagaECOTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbSagaMapCmd = new System.Windows.Forms.TextBox();
            this.SagaMapOutput = new System.Windows.Forms.WebBrowser();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.SagaLoginOutput = new System.Windows.Forms.WebBrowser();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.OnlinePlayers = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbOnlinePlayerCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbSagaMapStatus = new System.Windows.Forms.Label();
            this.lbSagaLoginStatus = new System.Windows.Forms.Label();
            this.OPMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.強制下線ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SagaLoginTimer = new System.Windows.Forms.Timer(this.components);
            this.SagaMapTimer = new System.Windows.Forms.Timer(this.components);
            this.tbAnnounce = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SagaECOTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.OPMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // nfIcon
            // 
            this.nfIcon.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.nfIcon, "nfIcon");
            this.nfIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nfIcon_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMainWindowToolStripMenuItem,
            this.toolStripSeparator1,
            this.啟動伺服器ToolStripMenuItem,
            this.sagaLoginToolStripMenuItem,
            this.重新啟動伺服器ToolStripMenuItem,
            this.toolStripSeparator2,
            this.cmuSagaLogin,
            this.cmuSagaMap,
            this.toolStripSeparator3,
            this.關於SagaECOToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // OpenMainWindowToolStripMenuItem
            // 
            this.OpenMainWindowToolStripMenuItem.Name = "OpenMainWindowToolStripMenuItem";
            resources.ApplyResources(this.OpenMainWindowToolStripMenuItem, "OpenMainWindowToolStripMenuItem");
            this.OpenMainWindowToolStripMenuItem.Click += new System.EventHandler(this.OpenMainWindowToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // 啟動伺服器ToolStripMenuItem
            // 
            this.啟動伺服器ToolStripMenuItem.Name = "啟動伺服器ToolStripMenuItem";
            resources.ApplyResources(this.啟動伺服器ToolStripMenuItem, "啟動伺服器ToolStripMenuItem");
            this.啟動伺服器ToolStripMenuItem.Click += new System.EventHandler(this.啟動伺服器ToolStripMenuItem_Click);
            // 
            // sagaLoginToolStripMenuItem
            // 
            this.sagaLoginToolStripMenuItem.Name = "sagaLoginToolStripMenuItem";
            resources.ApplyResources(this.sagaLoginToolStripMenuItem, "sagaLoginToolStripMenuItem");
            this.sagaLoginToolStripMenuItem.Click += new System.EventHandler(this.sagaLoginToolStripMenuItem_Click);
            // 
            // 重新啟動伺服器ToolStripMenuItem
            // 
            this.重新啟動伺服器ToolStripMenuItem.Name = "重新啟動伺服器ToolStripMenuItem";
            resources.ApplyResources(this.重新啟動伺服器ToolStripMenuItem, "重新啟動伺服器ToolStripMenuItem");
            this.重新啟動伺服器ToolStripMenuItem.Click += new System.EventHandler(this.重新啟動伺服器ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // cmuSagaLogin
            // 
            this.cmuSagaLogin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmuSagaLoginStatus,
            this.toolStripSeparator4,
            this.啟動ToolStripMenuItem,
            this.停止ToolStripMenuItem,
            this.重新啟動ToolStripMenuItem});
            this.cmuSagaLogin.Name = "cmuSagaLogin";
            resources.ApplyResources(this.cmuSagaLogin, "cmuSagaLogin");
            // 
            // cmuSagaLoginStatus
            // 
            resources.ApplyResources(this.cmuSagaLoginStatus, "cmuSagaLoginStatus");
            this.cmuSagaLoginStatus.Name = "cmuSagaLoginStatus";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // 啟動ToolStripMenuItem
            // 
            this.啟動ToolStripMenuItem.Name = "啟動ToolStripMenuItem";
            resources.ApplyResources(this.啟動ToolStripMenuItem, "啟動ToolStripMenuItem");
            this.啟動ToolStripMenuItem.Click += new System.EventHandler(this.啟動ToolStripMenuItem_Click);
            // 
            // 停止ToolStripMenuItem
            // 
            this.停止ToolStripMenuItem.Name = "停止ToolStripMenuItem";
            resources.ApplyResources(this.停止ToolStripMenuItem, "停止ToolStripMenuItem");
            this.停止ToolStripMenuItem.Click += new System.EventHandler(this.停止ToolStripMenuItem_Click);
            // 
            // 重新啟動ToolStripMenuItem
            // 
            this.重新啟動ToolStripMenuItem.Name = "重新啟動ToolStripMenuItem";
            resources.ApplyResources(this.重新啟動ToolStripMenuItem, "重新啟動ToolStripMenuItem");
            this.重新啟動ToolStripMenuItem.Click += new System.EventHandler(this.重新啟動ToolStripMenuItem_Click);
            // 
            // cmuSagaMap
            // 
            this.cmuSagaMap.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmuSagaMapStatus,
            this.toolStripSeparator5,
            this.啟動ToolStripMenuItem1,
            this.停止ToolStripMenuItem1,
            this.重新啟動ToolStripMenuItem1});
            this.cmuSagaMap.Name = "cmuSagaMap";
            resources.ApplyResources(this.cmuSagaMap, "cmuSagaMap");
            // 
            // cmuSagaMapStatus
            // 
            resources.ApplyResources(this.cmuSagaMapStatus, "cmuSagaMapStatus");
            this.cmuSagaMapStatus.Name = "cmuSagaMapStatus";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // 啟動ToolStripMenuItem1
            // 
            this.啟動ToolStripMenuItem1.Name = "啟動ToolStripMenuItem1";
            resources.ApplyResources(this.啟動ToolStripMenuItem1, "啟動ToolStripMenuItem1");
            this.啟動ToolStripMenuItem1.Click += new System.EventHandler(this.啟動ToolStripMenuItem1_Click);
            // 
            // 停止ToolStripMenuItem1
            // 
            this.停止ToolStripMenuItem1.Name = "停止ToolStripMenuItem1";
            resources.ApplyResources(this.停止ToolStripMenuItem1, "停止ToolStripMenuItem1");
            this.停止ToolStripMenuItem1.Click += new System.EventHandler(this.停止ToolStripMenuItem1_Click);
            // 
            // 重新啟動ToolStripMenuItem1
            // 
            this.重新啟動ToolStripMenuItem1.Name = "重新啟動ToolStripMenuItem1";
            resources.ApplyResources(this.重新啟動ToolStripMenuItem1, "重新啟動ToolStripMenuItem1");
            this.重新啟動ToolStripMenuItem1.Click += new System.EventHandler(this.重新啟動ToolStripMenuItem1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // 關於SagaECOToolStripMenuItem
            // 
            this.關於SagaECOToolStripMenuItem.Name = "關於SagaECOToolStripMenuItem";
            resources.ApplyResources(this.關於SagaECOToolStripMenuItem, "關於SagaECOToolStripMenuItem");
            this.關於SagaECOToolStripMenuItem.Click += new System.EventHandler(this.關於SagaECOToolStripMenuItem_Click_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.伺服器ToolStripMenuItem,
            this.settingToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // 伺服器ToolStripMenuItem
            // 
            this.伺服器ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.muStartLogin,
            this.muSagaMap,
            this.關閉LoginToolStripMenuItem,
            this.toolStripSeparator6,
            this.關閉MapToolStripMenuItem,
            this.sagaMApToolStripMenuItem,
            this.toolStripSeparator7,
            this.關閉SagaECOToolStripMenuItem});
            this.伺服器ToolStripMenuItem.Name = "伺服器ToolStripMenuItem";
            resources.ApplyResources(this.伺服器ToolStripMenuItem, "伺服器ToolStripMenuItem");
            // 
            // muStartLogin
            // 
            this.muStartLogin.Name = "muStartLogin";
            resources.ApplyResources(this.muStartLogin, "muStartLogin");
            this.muStartLogin.Click += new System.EventHandler(this.muStartLogin_Click);
            // 
            // muSagaMap
            // 
            this.muSagaMap.Name = "muSagaMap";
            resources.ApplyResources(this.muSagaMap, "muSagaMap");
            this.muSagaMap.Click += new System.EventHandler(this.muSagaMap_Click);
            // 
            // 關閉LoginToolStripMenuItem
            // 
            this.關閉LoginToolStripMenuItem.Name = "關閉LoginToolStripMenuItem";
            resources.ApplyResources(this.關閉LoginToolStripMenuItem, "關閉LoginToolStripMenuItem");
            this.關閉LoginToolStripMenuItem.Click += new System.EventHandler(this.關閉LoginToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // 關閉MapToolStripMenuItem
            // 
            this.關閉MapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.啟動ToolStripMenuItem2,
            this.停止ToolStripMenuItem2,
            this.重新啟動ToolStripMenuItem2});
            this.關閉MapToolStripMenuItem.Name = "關閉MapToolStripMenuItem";
            resources.ApplyResources(this.關閉MapToolStripMenuItem, "關閉MapToolStripMenuItem");
            this.關閉MapToolStripMenuItem.Click += new System.EventHandler(this.關閉MapToolStripMenuItem_Click);
            // 
            // 啟動ToolStripMenuItem2
            // 
            this.啟動ToolStripMenuItem2.Name = "啟動ToolStripMenuItem2";
            resources.ApplyResources(this.啟動ToolStripMenuItem2, "啟動ToolStripMenuItem2");
            this.啟動ToolStripMenuItem2.Click += new System.EventHandler(this.啟動ToolStripMenuItem2_Click);
            // 
            // 停止ToolStripMenuItem2
            // 
            this.停止ToolStripMenuItem2.Name = "停止ToolStripMenuItem2";
            resources.ApplyResources(this.停止ToolStripMenuItem2, "停止ToolStripMenuItem2");
            this.停止ToolStripMenuItem2.Click += new System.EventHandler(this.停止ToolStripMenuItem2_Click);
            // 
            // 重新啟動ToolStripMenuItem2
            // 
            this.重新啟動ToolStripMenuItem2.Name = "重新啟動ToolStripMenuItem2";
            resources.ApplyResources(this.重新啟動ToolStripMenuItem2, "重新啟動ToolStripMenuItem2");
            this.重新啟動ToolStripMenuItem2.Click += new System.EventHandler(this.重新啟動ToolStripMenuItem2_Click);
            // 
            // sagaMApToolStripMenuItem
            // 
            this.sagaMApToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.啟動ToolStripMenuItem3,
            this.停止ToolStripMenuItem3,
            this.重新啟動ToolStripMenuItem3});
            this.sagaMApToolStripMenuItem.Name = "sagaMApToolStripMenuItem";
            resources.ApplyResources(this.sagaMApToolStripMenuItem, "sagaMApToolStripMenuItem");
            // 
            // 啟動ToolStripMenuItem3
            // 
            this.啟動ToolStripMenuItem3.Name = "啟動ToolStripMenuItem3";
            resources.ApplyResources(this.啟動ToolStripMenuItem3, "啟動ToolStripMenuItem3");
            this.啟動ToolStripMenuItem3.Click += new System.EventHandler(this.啟動ToolStripMenuItem3_Click);
            // 
            // 停止ToolStripMenuItem3
            // 
            this.停止ToolStripMenuItem3.Name = "停止ToolStripMenuItem3";
            resources.ApplyResources(this.停止ToolStripMenuItem3, "停止ToolStripMenuItem3");
            this.停止ToolStripMenuItem3.Click += new System.EventHandler(this.停止ToolStripMenuItem3_Click);
            // 
            // 重新啟動ToolStripMenuItem3
            // 
            this.重新啟動ToolStripMenuItem3.Name = "重新啟動ToolStripMenuItem3";
            resources.ApplyResources(this.重新啟動ToolStripMenuItem3, "重新啟動ToolStripMenuItem3");
            this.重新啟動ToolStripMenuItem3.Click += new System.EventHandler(this.重新啟動ToolStripMenuItem3_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // 關閉SagaECOToolStripMenuItem
            // 
            this.關閉SagaECOToolStripMenuItem.Name = "關閉SagaECOToolStripMenuItem";
            resources.ApplyResources(this.關閉SagaECOToolStripMenuItem, "關閉SagaECOToolStripMenuItem");
            this.關閉SagaECOToolStripMenuItem.Click += new System.EventHandler(this.關閉SagaECOToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoStartServerToolStripMenuItem,
            this.toolStripSeparator8,
            this.enableColorToolStripMenuItem,
            this.enableSagaLoginOutputToolStripMenuItem,
            this.sagaMapOutputToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            resources.ApplyResources(this.settingToolStripMenuItem, "settingToolStripMenuItem");
            // 
            // autoStartServerToolStripMenuItem
            // 
            this.autoStartServerToolStripMenuItem.Name = "autoStartServerToolStripMenuItem";
            resources.ApplyResources(this.autoStartServerToolStripMenuItem, "autoStartServerToolStripMenuItem");
            this.autoStartServerToolStripMenuItem.Click += new System.EventHandler(this.autoStartServerToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // enableColorToolStripMenuItem
            // 
            this.enableColorToolStripMenuItem.Name = "enableColorToolStripMenuItem";
            resources.ApplyResources(this.enableColorToolStripMenuItem, "enableColorToolStripMenuItem");
            this.enableColorToolStripMenuItem.Click += new System.EventHandler(this.enableColorToolStripMenuItem_Click);
            // 
            // enableSagaLoginOutputToolStripMenuItem
            // 
            this.enableSagaLoginOutputToolStripMenuItem.Name = "enableSagaLoginOutputToolStripMenuItem";
            resources.ApplyResources(this.enableSagaLoginOutputToolStripMenuItem, "enableSagaLoginOutputToolStripMenuItem");
            this.enableSagaLoginOutputToolStripMenuItem.Click += new System.EventHandler(this.enableSagaLoginOutputToolStripMenuItem_Click);
            // 
            // sagaMapOutputToolStripMenuItem
            // 
            this.sagaMapOutputToolStripMenuItem.Name = "sagaMapOutputToolStripMenuItem";
            resources.ApplyResources(this.sagaMapOutputToolStripMenuItem, "sagaMapOutputToolStripMenuItem");
            this.sagaMapOutputToolStripMenuItem.Click += new System.EventHandler(this.sagaMapOutputToolStripMenuItem_Click);
            // 
            // SagaECOTab
            // 
            this.SagaECOTab.Controls.Add(this.tabPage1);
            this.SagaECOTab.Controls.Add(this.tabPage2);
            this.SagaECOTab.Controls.Add(this.tabPage4);
            resources.ApplyResources(this.SagaECOTab, "SagaECOTab");
            this.SagaECOTab.Name = "SagaECOTab";
            this.SagaECOTab.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbSagaMapCmd);
            this.tabPage1.Controls.Add(this.SagaMapOutput);
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbSagaMapCmd
            // 
            this.tbSagaMapCmd.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.tbSagaMapCmd, "tbSagaMapCmd");
            this.tbSagaMapCmd.Name = "tbSagaMapCmd";
            this.tbSagaMapCmd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSagaMapCmd_KeyDown);
            // 
            // SagaMapOutput
            // 
            resources.ApplyResources(this.SagaMapOutput, "SagaMapOutput");
            this.SagaMapOutput.MinimumSize = new System.Drawing.Size(20, 20);
            this.SagaMapOutput.Name = "SagaMapOutput";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.SagaLoginOutput);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // SagaLoginOutput
            // 
            resources.ApplyResources(this.SagaLoginOutput, "SagaLoginOutput");
            this.SagaLoginOutput.MinimumSize = new System.Drawing.Size(20, 20);
            this.SagaLoginOutput.Name = "SagaLoginOutput";
            this.SagaLoginOutput.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.SagaLoginOutput_DocumentCompleted);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tbAnnounce);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.button2);
            this.tabPage4.Controls.Add(this.OnlinePlayers);
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Controls.Add(this.lbOnlinePlayerCount);
            this.tabPage4.Controls.Add(this.label1);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.tabPage4.Click += new System.EventHandler(this.tabPage4_Click);
            this.tabPage4.Enter += new System.EventHandler(this.tabPage4_Enter);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // OnlinePlayers
            // 
            this.OnlinePlayers.FormattingEnabled = true;
            resources.ApplyResources(this.OnlinePlayers, "OnlinePlayers");
            this.OnlinePlayers.Name = "OnlinePlayers";
            this.OnlinePlayers.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnlinePlayers_MouseUp);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbOnlinePlayerCount
            // 
            resources.ApplyResources(this.lbOnlinePlayerCount, "lbOnlinePlayerCount");
            this.lbOnlinePlayerCount.Name = "lbOnlinePlayerCount";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lbSagaMapStatus
            // 
            resources.ApplyResources(this.lbSagaMapStatus, "lbSagaMapStatus");
            this.lbSagaMapStatus.BackColor = System.Drawing.Color.White;
            this.lbSagaMapStatus.ForeColor = System.Drawing.Color.DarkGray;
            this.lbSagaMapStatus.Name = "lbSagaMapStatus";
            // 
            // lbSagaLoginStatus
            // 
            resources.ApplyResources(this.lbSagaLoginStatus, "lbSagaLoginStatus");
            this.lbSagaLoginStatus.BackColor = System.Drawing.Color.White;
            this.lbSagaLoginStatus.ForeColor = System.Drawing.Color.DarkGray;
            this.lbSagaLoginStatus.Name = "lbSagaLoginStatus";
            // 
            // OPMenu
            // 
            this.OPMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.強制下線ToolStripMenuItem});
            this.OPMenu.Name = "OPMenu";
            resources.ApplyResources(this.OPMenu, "OPMenu");
            // 
            // 強制下線ToolStripMenuItem
            // 
            this.強制下線ToolStripMenuItem.Name = "強制下線ToolStripMenuItem";
            resources.ApplyResources(this.強制下線ToolStripMenuItem, "強制下線ToolStripMenuItem");
            this.強制下線ToolStripMenuItem.Click += new System.EventHandler(this.強制下線ToolStripMenuItem_Click);
            // 
            // SagaLoginTimer
            // 
            this.SagaLoginTimer.Enabled = true;
            this.SagaLoginTimer.Interval = 1000;
            this.SagaLoginTimer.Tick += new System.EventHandler(this.SagaLoginTimer_Tick);
            // 
            // SagaMapTimer
            // 
            this.SagaMapTimer.Enabled = true;
            this.SagaMapTimer.Interval = 1000;
            this.SagaMapTimer.Tick += new System.EventHandler(this.SagaMapTimer_Tick);
            // 
            // tbAnnounce
            // 
            this.tbAnnounce.FormattingEnabled = true;
            resources.ApplyResources(this.tbAnnounce, "tbAnnounce");
            this.tbAnnounce.Name = "tbAnnounce";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SagaECOTab);
            this.Controls.Add(this.lbSagaLoginStatus);
            this.Controls.Add(this.lbSagaMapStatus);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.SagaECOTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.OPMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon nfIcon;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 伺服器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem muStartLogin;
        private System.Windows.Forms.TabControl SagaECOTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem muSagaMap;
        private System.Windows.Forms.WebBrowser SagaLoginOutput;
        private System.Windows.Forms.ToolStripMenuItem 關閉LoginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 關閉MapToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem OpenMainWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 啟動伺服器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sagaLoginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新啟動伺服器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 關於SagaECOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmuSagaLogin;
        private System.Windows.Forms.ToolStripMenuItem cmuSagaLoginStatus;
        private System.Windows.Forms.ToolStripMenuItem cmuSagaMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem 啟動ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新啟動ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmuSagaMapStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 啟動ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 重新啟動ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem 啟動ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 重新啟動ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem sagaMApToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 啟動ToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 重新啟動ToolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem 關閉SagaECOToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label lbSagaMapStatus;
        private System.Windows.Forms.Label lbSagaLoginStatus;
        private System.Windows.Forms.Label lbOnlinePlayerCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox OnlinePlayers;
        private System.Windows.Forms.ContextMenuStrip OPMenu;
        private System.Windows.Forms.ToolStripMenuItem 強制下線ToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox tbSagaMapCmd;
        private System.Windows.Forms.WebBrowser SagaMapOutput;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoStartServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem enableColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableSagaLoginOutputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sagaMapOutputToolStripMenuItem;
        private System.Windows.Forms.Timer SagaLoginTimer;
        private System.Windows.Forms.Timer SagaMapTimer;
        private System.Windows.Forms.ComboBox tbAnnounce;
    }
}

