using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
//using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions; 

namespace SagaECOLoader
{

    public partial class Form1 : Form
    {
        //[DllImport("user32.dll")]
        //private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        Process SagaLogin;
        Process SagaMap;

        StreamWriter SagaMapInput;

        private delegate void InvokeUpdateState(string s);
        private delegate void InvokeUpdateOnlinePlayer(string s);
        private delegate void InvokeClearOnlinePlayer();

        string SagaLogin_LastOut;
        string SagaMap_LastOut;
        bool SagaLogin_PercentOut = false;
        bool SagaMap_PercentOut = false;

        bool CloseMe = false;
        bool CloseSagaLogin = false;
        bool CloseSagaMap = false;

        bool SagaLoginStarted = false;
        bool CatchCommandResult = false;

        Queue<string> SagaLoginMessageQueue = new Queue<string>();
        Queue<string> SagaMapMessageQueue = new Queue<string>();

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void muStartLogin_Click(object sender, EventArgs e)
        {
            StartSagaECO();
        }
        private void muSagaMap_Click(object sender, EventArgs e)
        {
            StopSagaECO();
        }

        #region Process Control

        public void StartSagaMap()
        {
            CloseSagaMap = false;
            SagaECOTab.SelectTab(0);
            SagaMap = new Process();
            SagaMap.StartInfo.FileName = "SagaMap.exe";
            SagaMap.StartInfo.WorkingDirectory = Environment.CurrentDirectory ;
            SagaMap.StartInfo.UseShellExecute = false;
            SagaMap.StartInfo.CreateNoWindow = true;
            SagaMap.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            SagaMap.StartInfo.RedirectStandardOutput = true;
            SagaMap.StartInfo.RedirectStandardInput = true;
            SagaMap.OutputDataReceived += new DataReceivedEventHandler(SagaMap_OutputDataReceived);
            SagaMap.Start();
            SagaMap.Exited += new EventHandler(SagaMap_Exited);
            SagaMapInput = SagaMap.StandardInput;
            SagaMap.BeginOutputReadLine();
            //ShowWindow(SagaMap.MainWindowHandle, 0);
            cmuSagaMapStatus.Text = "狀態：啟動";
            lbSagaMapStatus.ForeColor = Color.Green;
        }

        void SagaMap_Exited(object sender, EventArgs e)
        {
            if (!CloseMe && !CloseSagaMap)
            {
                StartSagaMap();
            }
        }

        public void StartSagaLogin()
        {
            CloseSagaLogin = false;
            SagaECOTab.SelectTab(1);
            SagaLogin= new Process();
            SagaLogin.StartInfo.FileName = "SagaLogin.exe";
            SagaLogin.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            SagaLogin.StartInfo.UseShellExecute = false;
            SagaLogin.StartInfo.CreateNoWindow = true;
            SagaLogin.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            SagaLogin.StartInfo.RedirectStandardOutput = true;
            SagaLogin.OutputDataReceived += new DataReceivedEventHandler(SagaLogin_OutputDataReceived);
            SagaLogin.Start();
            SagaLogin.Exited += new EventHandler(SagaLogin_Exited);
            SagaLogin.BeginOutputReadLine();
            cmuSagaLoginStatus.Text = "狀態：啟動";
            lbSagaLoginStatus.ForeColor = Color.Green;
        }

        void SagaLogin_Exited(object sender, EventArgs e)
        {
            if (!CloseMe && !CloseSagaLogin)
            {
                StartSagaLogin();
            }
        }

        public void StopSagaLogin()
        {
            try
            {
                //ShowWindow(SagaLogin.MainWindowHandle, 1);
                //System.Threading.Thread.Sleep(1000);
                //SagaLogin.CloseMainWindow();
                //System.Threading.Thread.Sleep(1000);
                CloseSagaLogin = true;
                if (!SagaLogin.HasExited)
                {
                    SagaLogin.Kill();
                }
                SagaLogin.Close();

            }
            catch (Exception) { };
            cmuSagaLoginStatus.Text = "狀態：停止";
            SagaLoginStarted = false;
            lbSagaLoginStatus.ForeColor = Color.DarkGray;
        }

        public void StopSagaMap()
        {
            try
            {
                int count = 0;
                CloseSagaMap = true;
                while (!SagaMap.HasExited)
                {
                    try
                    {
                        SagaMapWriteLine("quit");
                    }
                    catch (Exception e)
                    {
                        SagaMap_OutputLog(string.Format("[Error]SagaMap 關閉時發生錯誤：{0}" + e.Message));
                        SagaMap_OutputLog("[Error]SagaMap 1秒後嘗試重新關閉..");
                    }
                    System.Threading.Thread.Sleep(1000);
                    count++;
                    if (count > 50)
                    {
                        SagaMap.Kill();
                        SagaMap_OutputLog("[Error]SagaMap 無法正常終止..");
                        SagaMap_OutputLog("[Error]SagaMap 已強制關閉..");
                    }
                }
                SagaMap.Close();
                SagaMapInput = null;
                //SagaMap_OutputLog("[Info]SagaMap Closed");
            }
            catch (Exception) { };
            cmuSagaMapStatus.Text = "狀態：停止";
            lbSagaMapStatus.ForeColor = Color.DarkGray;
        }

        public void StartSagaECO()
        {
            StartSagaLogin();
            while (!SagaLoginStarted)
            {
                //System.Threading.Thread.Sleep(5000);
                Application.DoEvents();
            }
            StartSagaMap();
        }

        public void StopSagaECO()
        {
            StopSagaMap();
            StopSagaLogin();
        }

        #endregion

        #region Output Log
        public void SagaLogin_OutputDataReceived(object sendingProcess, DataReceivedEventArgs outLine)
        {
           //SagaLogin_OutputLog(outLine.Data);
            SagaLoginMessageQueue.Enqueue(outLine.Data);
        }
        public void SagaMap_OutputDataReceived(object sendingProcess, DataReceivedEventArgs outLine)
        {
            //SagaMap_OutputLog(outLine.Data);
            SagaMapMessageQueue.Enqueue(outLine.Data);
        }

        public void SagaLogin_OutputLog(string s)
        {
            if (s == "")
            {
                return;
            }
            if (!SagaLoginStarted)
            {
                if (s == "Accepting clients.")
                {
                    SagaLoginStarted = true;
                }
            }
            if (!Properties.Settings.Default.SagaLoginOut )
            {
                return;
            }
            //if (this.SagaLoginOutput.InvokeRequired)
            //{
            //    try
            //    {
            //        this.Invoke(
            //          new InvokeUpdateState(this.SagaLogin_OutputLog), new object[] { s }
            //        );
            //    }
            //    catch (Exception) { };
            //}
            //else
            //{
                s = ProcessColor(s);
                if (s.IndexOf("%") > 0)
                {
                    if (s.IndexOf(" 0%") > 0)
                    {
                        if (this.SagaLogin_LastOut != s)
                        {
                            SagaLogin_WriteLine(s);
                            SagaLogin_PercentOut = true;
                        }
                    }
                    else
                    {
                        SagaLogin_ReplaceLastLine(s);
                    }
                }
                else
                {
                    if (SagaLogin_PercentOut)
                    {
                        SagaLogin_PercentOut = false;
                        SagaLogin_ReplaceLastLine(s);
                    }
                    else
                    {
                        SagaLogin_WriteLine(s);
                    }
                }
                SagaLogin_LastOut = s;
            //}
        }

        
        private void SagaLogin_WriteLine(string s)
        {
            Log(SagaLoginOutput,s, "Log");
        }
        private void SagaLogin_ReplaceLastLine(string s)
        {
            Log(SagaLoginOutput ,s, "ReplaceLastLine");
        }
        private void SagaMap_WriteLine(string s)
        {
            Log(SagaMapOutput , s, "Log");
        }
        private void SagaMap_ReplaceLastLine(string s)
        {
            Log(SagaMapOutput, s, "ReplaceLastLine");
        }
        private void Log(WebBrowser wb, string s,string fun)
        {
            try
            {
                Object[] args = new Object[1];
                args[0] = s;
                wb.Document.InvokeScript(fun, args);
            }
           catch (Exception ex)
            { }
        }
        

        public void SagaMap_OutputLog(string s)
        {
            if (!Properties.Settings.Default.SagaMapOut)
            {
                return;
            }
            try
            {
                if (this.SagaMapOutput.InvokeRequired)
                {
                    try
                    {
                        this.Invoke(
                          new InvokeUpdateState(this.SagaMap_OutputLog), new object[] { s }
                        );
                    }
                    catch (Exception) { };
                }
                else
                {

                    if (s == "")
                    {
                        return;
                    }
                    if (CatchCommandResult)
                    {
                        OnCatchCommandResult(s);
                    }
                    s = ProcessColor(s);
                    if (s.IndexOf("%") > 0)
                    {
                        if (s.IndexOf(" 0%") > 0)
                        {
                            if (this.SagaMap_LastOut != s)
                            {
                                SagaMap_WriteLine(s);
                                SagaMap_PercentOut = true;
                            }
                        }
                        else
                        {
                            SagaMap_ReplaceLastLine(s);
                        }
                    }
                    else
                    {
                        if (SagaMap_PercentOut)
                        {
                            SagaMap_PercentOut = false;
                            SagaMap_ReplaceLastLine(s);
                        }
                        else
                        {
                            SagaMap_WriteLine(s);
                        }
                    }
                    SagaMap_LastOut = s;
                }
            }
            catch (Exception) { };
        }
        #endregion

        #region Colored
        public string ProcessColor(string s)
        {
            if (!Properties.Settings.Default.EnableColor)
            {
                return s;
            }
            string ColorHtmlTemplete = "<font color='{0}'>{1}</font>";
            string DefaultColor = "black";
            try
            {
                if (s.IndexOf("[Info]") >= 0)
                {
                    string strLeft, strRight;
                    Split2(s, "]", out strLeft, out strRight, 1);
                    return string.Format(ColorHtmlTemplete, "ForestGreen", strLeft) + string.Format(ColorHtmlTemplete, DefaultColor, strRight);
                }
                if (s.IndexOf("[Debug]") >= 0)
                {
                    string strLeft, strRight;
                    Split2(s, "]", out strLeft, out strRight, 1);
                    return string.Format(ColorHtmlTemplete, "DodgerBlue", strLeft) + string.Format(ColorHtmlTemplete, DefaultColor, strRight);
                }
                if (s.IndexOf("[Warning]") >= 0)
                {
                    string strLeft, strRight;
                    Split2(s, "]", out strLeft, out strRight, 1);
                    return string.Format(ColorHtmlTemplete, "Peru", strLeft) + string.Format(ColorHtmlTemplete, DefaultColor, strRight);
                }
                if (s.IndexOf("[SQL]") >= 0)
                {
                    string strLeft, strRight;
                    Split2(s, "]", out strLeft, out strRight, 1);
                    return string.Format(ColorHtmlTemplete, "Magenta", strLeft) + string.Format(ColorHtmlTemplete, DefaultColor, strRight);
                }
                if (s.IndexOf("[Error]") >= 0)
                {
                    string strLeft, strRight;
                    Split2(s, "]", out strLeft, out strRight, 1);
                    return string.Format(ColorHtmlTemplete, "Red", strLeft) + string.Format(ColorHtmlTemplete, DefaultColor, strRight);
                }
                if (s == "======================================================================")
                {
                    return string.Format(ColorHtmlTemplete, "Chocolate", s);
                }
                if (s.IndexOf("SagaECO") > 0)
                {
                    return string.Format(ColorHtmlTemplete, "DodgerBlue", s);
                }
                if ((s.IndexOf("SagaMap") >= 0) || (s.IndexOf("SagaLogin") >= 0) || (s.IndexOf("SagaLib") >= 0) || (s.IndexOf("SagaDB") >= 0))
                {
                    string strLeft, strRight;
                    Split2(s, ":", out strLeft, out strRight);
                    return string.Format(ColorHtmlTemplete, "Orage", strLeft) + string.Format(ColorHtmlTemplete, DefaultColor, strRight);
                }
                if (s.IndexOf("Current Packet Version:[") >= 0)
                {
                    return string.Format(ColorHtmlTemplete, "Chocolate", s);
                }
            }
            catch (Exception)
            {
            }
            return string.Format(ColorHtmlTemplete, "black", s);
        }
        private void Split2(string strOri,string delimiter, out string strLeft, out string strRight)
        {
            int pos = strOri.IndexOf(delimiter);
            strLeft = strOri.Substring(0, pos);
            strRight = strOri.Substring(pos );
        }
        private void Split2(string strOri, string delimiter, out string strLeft, out string strRight,int offset)
        {
            int pos = strOri.IndexOf(delimiter);
            strLeft = strOri.Substring(0, pos + offset);
            strRight = strOri.Substring(pos + offset);
        }
        #endregion

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopSagaECO();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path="SagaECOLoader.htm";
            string html = @"
<htm>
<head>
<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>
</head>
<body bgcolor='white'>
<div id='log'></div>
</body>
<script>
var MaxLine=500;
function Log(str)
{
    var log = document.getElementById('log');
    if(log.childNodes.length >= MaxLine)
    {
        DeleteFirstLine();
    }
    WriteLine(str);
}
function WriteLine(str) {
  var ni = document.getElementById('log');
  var newdiv = document.createElement('div');
  //newdiv.setAttribute('id',divIdName);
  newdiv.innerHTML =str;
  ni.appendChild(newdiv);
    ScrollDown();
}
function ReplaceLastLine(str)
{
     var log = document.getElementById('log');
     log.childNodes.item(log.childNodes.length -1).innerHTML=str;
}
function DeleteFirstLine()
{
    var log = document.getElementById('log');
    if(log.childNodes.length >0)
    {
        log.removeChild(log.childNodes(0));
    }
}
function ScrollDown()
{
	document.body.scrollTop =document.body.scrollHeight;
}
</script>
</htm>
";
            try
            {
                StreamWriter s = new StreamWriter(path);
                s.WriteLine(html);
                s.Close();
            }
            catch (Exception) { };
            SagaLoginOutput.Navigate(Environment.CurrentDirectory+"/"+path );
            SagaMapOutput.Navigate(Environment.CurrentDirectory + "/" + path);

            SagaMapOutput.Height = tabPage1.ClientSize.Height - tbSagaMapCmd.Height;

            autoStartServerToolStripMenuItem.Checked = Properties.Settings.Default.AutoStartServer;
            enableColorToolStripMenuItem.Checked = Properties.Settings.Default.EnableColor;
            enableSagaLoginOutputToolStripMenuItem.Checked = Properties.Settings.Default.SagaLoginOut;
            sagaMapOutputToolStripMenuItem.Checked = Properties.Settings.Default.SagaMapOut;

            if (Properties.Settings.Default.AutoStartServer)
            {
                StartSagaECO();
            }
        }

        private void SagaLoginOutput_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void tbAnnounce_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbAnnounce_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                SagaMapWriteLine(string.Format("announce {0}", tbAnnounce.Text));
                tbAnnounce.Text = "";
            }
        }
        public void SagaMapWriteLine(string s)
        {
            try
            {
                SagaMapInput.WriteLine(s);
            }
            catch (Exception) 
            { 
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                HideMe();
            }
            else
            {
                SagaMapOutput.Height=tabPage1.ClientSize.Height -tbSagaMapCmd.Height ;
                //tbSagaMapCmd
            }
        }

        public void HideMe()
        {
            this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CloseMe)
            {
                e.Cancel = true;
                HideMe();
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        #region Menu
        private void 關閉LoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopSagaECO();
            StartSagaECO();
        }

        private void 關閉MapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopSagaMap();
        }

        private void OpenMainWindowToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Show();
        }

        private void 啟動伺服器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartSagaECO();
        }

        private void 關於SagaECOToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CloseMe = true;
            StopSagaECO();
            Application.Exit();
        }

        private void 啟動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartSagaLogin();
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopSagaLogin();
        }

        private void 重新啟動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopSagaLogin();
            StartSagaLogin();
        }

        private void 啟動ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StartSagaMap();
        }

        private void 停止ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StopSagaMap();
        }

        private void 重新啟動ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StopSagaMap();
            StartSagaMap();
        }

        private void sagaLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopSagaECO();
        }

        private void 重新啟動伺服器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopSagaECO();
            StartSagaECO();
        }

        private void 啟動ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            StartSagaLogin();
        }

        private void 停止ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            StopSagaLogin();
        }

        private void 重新啟動ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            StopSagaLogin();
            StartSagaLogin();
        }

        private void 啟動ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            StartSagaMap();
        }

        private void 停止ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            StopSagaMap();
        }

        private void 重新啟動ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            StopSagaMap();
            StartSagaMap();
        }

        private void 關閉SagaECOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            關於SagaECOToolStripMenuItem_Click_1(sender, e);
        }
        #endregion 

        private void nfIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }
        public void GetOnlineUsers()
        {
            try
            {
                ClearOnlinePlayer();
                lbOnlinePlayerCount.Text = "0";
                CatchCommandResult = true;
                SagaMapWriteLine("who");
            }
            catch (Exception)
            {
                CatchCommandResult = false;
            }
        }

        public void OnCatchCommandResult(string msg)
        {
            var r = Regex.Match(msg, "[C][h][a][r][I][D]");
            if (r.Success)
            {
                AddOnlinePlayer(msg);
            }
            else
            {
                CatchCommandResult = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetOnlineUsers();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            GetOnlineUsers();
        }

        private void OnlinePlayers_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                OPMenu.Show(OnlinePlayers, e.Location);
            }
        }

        private void 強制下線ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnlinePlayers.SelectedIndex > -1)
            {
                var r=Regex.Match(OnlinePlayers.SelectedItem.ToString() ,"[\\d]+");
                if (r.Success)
                {
                    SagaMapWriteLine(string.Format("kick2 {0}", r.Value));
                    GetOnlineUsers();
                }
            }
        }
        public void AddOnlinePlayer(string s)
        {
            if (this.OnlinePlayers.InvokeRequired)
            {
                try
                {
                    this.Invoke(
                      new InvokeUpdateOnlinePlayer(this.AddOnlinePlayer), new object[] { s }
                    );
                }
                catch (Exception) { };
            }
            else
            {
                OnlinePlayers.Items.Add(s);
                lbOnlinePlayerCount.Text = OnlinePlayers.Items.Count.ToString();
            }

        }
        public void ClearOnlinePlayer()
        {
            if (this.OnlinePlayers.InvokeRequired)
            {
                try
                {
                    this.Invoke(
                      new InvokeClearOnlinePlayer(this.ClearOnlinePlayer), new object[] {}
                    );
                }
                catch (Exception) { };
            }
            else
            {
                OnlinePlayers.Items.Clear();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SagaMapWriteLine(string.Format("announce {0}", tbAnnounce.Text));
            if (tbAnnounce.Items.Count > 10)
                tbAnnounce.Items.RemoveAt(tbAnnounce.Items.Count - 1);
            tbAnnounce.Items.Insert (0,tbAnnounce.Text);
            tbAnnounce.Text = "";
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void tbSagaMapCmd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                SagaMapWriteLine(tbSagaMapCmd.Text);
                tbSagaMapCmd.Text="";
            }
        }

        private void autoStartServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoStartServerToolStripMenuItem.Checked = !autoStartServerToolStripMenuItem.Checked;
            Properties.Settings.Default.AutoStartServer = autoStartServerToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void enableColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enableColorToolStripMenuItem.Checked = !enableColorToolStripMenuItem.Checked;
            Properties.Settings.Default.EnableColor = enableColorToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void enableSagaLoginOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enableSagaLoginOutputToolStripMenuItem.Checked = !enableSagaLoginOutputToolStripMenuItem.Checked;
            Properties.Settings.Default.SagaLoginOut = enableSagaLoginOutputToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void sagaMapOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sagaMapOutputToolStripMenuItem.Checked = !sagaMapOutputToolStripMenuItem.Checked;
            Properties.Settings.Default.SagaMapOut = sagaMapOutputToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void SagaLoginTimer_Tick(object sender, EventArgs e)
        {
            if (SagaLoginMessageQueue.Count > 0)
            {
                string[] SagaLoginMessageQueueOut;
                lock (SagaLoginMessageQueue)
                {
                    SagaLoginMessageQueueOut = SagaLoginMessageQueue.ToArray();
                    SagaLoginMessageQueue.Clear();
                }
                foreach (string s in SagaLoginMessageQueueOut)
                {
                    SagaLogin_OutputLog(s);
                }
            }
        }

        private void SagaMapTimer_Tick(object sender, EventArgs e)
        {
            if (SagaMapMessageQueue.Count > 0)
            {
                string[] SagaMapMessageQueueOut;
                lock (SagaMapMessageQueue)
                {
                    SagaMapMessageQueueOut = SagaMapMessageQueue.ToArray();
                    SagaMapMessageQueue.Clear();
                }
                foreach (string s in SagaMapMessageQueueOut)
                {
                    SagaMap_OutputLog(s);
                }
            }
        }

    
    }
}
