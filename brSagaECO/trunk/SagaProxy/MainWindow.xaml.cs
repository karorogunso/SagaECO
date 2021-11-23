using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using SagaLib.VirtualFileSystem;
using Microsoft.Win32;

namespace SagaProxy
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<PacketInfo> PacketList = new List<PacketInfo>();
        List<PacketInfo> visiblePackets = new List<PacketInfo>();
        List<int> opcodeFilter = new List<int>();
        List<string> originFilter = new List<string>();
        List<string> serverFilter = new List<string>();
        public GameServerSession CurrentMapServer;
        public GameServerSession CurrentLoginServer;
        public GameServerSession CurrentValidationServer;
        public ProxyClient CurrentMapClient;
        public ProxyClient CurrentLoginClient;
        public ProxyClient CurrentValidationClient;
        public string TargetServerName=String.Empty;

        public MainWindow()
        {
            InitializeComponent();
            VirtualFileSystemManager.Instance.Init(FileSystems.Real, ".");
        }
        
        static MainWindow instance = new MainWindow();
        public static MainWindow Instance
        {
            get { return instance; }
        }
        

        private void Start(object sender, MouseButtonEventArgs e)
        {
            if (TargetIP.IsEnabled == false)
                return;
            TargetIP.IsEnabled = false;
            TargetPort.IsEnabled = false;
            LocalPort.IsEnabled = false;
            ProxyServer.Instance.Launch(TargetIP.Text, int.Parse(TargetPort.Text), int.Parse(LocalPort.Text), ProxyServer.ServerType.Validation);
            ProxyServer.Instance.Launch(TargetIP.Text, int.Parse(TargetPort.Text), int.Parse(LocalPort.Text), ProxyServer.ServerType.Login);
            ProxyServer.Instance.Launch(TargetIP.Text, int.Parse(TargetPort.Text), int.Parse(LocalPort.Text), ProxyServer.ServerType.Map);
            ProxyServer.Instance.StartLoop();
        }

        private void Stop(object sender, MouseButtonEventArgs e)
        {
            if (TargetIP.IsEnabled == true)
                return;
            ProxyServer.Instance.AbortLoop();
            TargetIP.IsEnabled = true;
            TargetPort.IsEnabled = true;
            LocalPort.IsEnabled = true;
        }

        public void UpdateGrid()
        {
            visiblePackets = PacketList;
            filterPacketByOpCode();
            filterPacketByOrigin();
            filterPacketByServer();
            PacketGrid.ItemsSource = visiblePackets;
            PacketGrid.Items.Refresh();
        }

        private void Filter(object sender, MouseButtonEventArgs e)
        {
            opcodeFilter.Add(visiblePackets[PacketGrid.SelectedIndex].OpCode);
            UpdateGrid();
        }

        private void filterPacketByOpCode()
        {
            IEnumerable<PacketInfo> query = visiblePackets;
            foreach (int opcode in opcodeFilter)
            {
                query = from PacketInfo pi in query
                        where pi.OpCode != opcode
                        select pi;
            }
            visiblePackets = query.ToList();
        }

        private void filterPacketByOrigin()
        {
            IEnumerable<PacketInfo> query = visiblePackets;
            foreach (string origin in originFilter)
            {
                query = from PacketInfo pi in query
                        where pi.Origin != origin
                        select pi;
            }
            visiblePackets = query.ToList();
        }

        private void filterPacketByServer()
        {
            IEnumerable<PacketInfo> query = visiblePackets;
            foreach (string server in serverFilter)
            {
                query = from PacketInfo pi in query
                        where pi.Server != server
                        select pi;
            }
            visiblePackets = query.ToList();
        }

        private void DisableFilter(object sender, MouseButtonEventArgs e)
        {
            opcodeFilter.Clear();
            UpdateGrid();
        }

        private void ExportPacket(object sender, MouseButtonEventArgs e)
        {
            StreamWriter sw = new StreamWriter(System.DateTime.Now.ToString("yyMMdd-hh-mm-ss") + ".csv", false, Encoding.ASCII);
            foreach (var pi in visiblePackets)
                sw.WriteLine(pi.ToString());
            sw.Close();
        }

        private void ImportPacket(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".csv";
            dialog.InitialDirectory = Environment.CurrentDirectory;
            if (dialog.ShowDialog() == true)
            {
                PacketList.Clear();
                Stream baseStream = VirtualFileSystemManager.Instance.FileSystem.OpenFile(dialog.FileName);
                TextFieldParser parser = new TextFieldParser(baseStream);
                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(",");
                string[] buf,paras;
                paras = parser.ReadFields();
                while (!parser.EndOfData)
                {
                    try
                    {
                        string data = paras[6];
                        if (!parser.EndOfData)
                        {
                            buf = parser.ReadFields();
                            while (!buf[0].StartsWith("Server") && !buf[0].StartsWith("Client"))
                            {
                                data += buf[0];
                                if (!parser.EndOfData)
                                    buf = parser.ReadFields();
                            }
                            PacketInfo pi = new PacketInfo(paras[0], paras[1], int.Parse(paras[2]), (int)new System.ComponentModel.Int32Converter().ConvertFromString(paras[3]), int.Parse(paras[4]), data);
                            PacketList.Add(pi);
                            paras = buf;
                        }
                        else
                        {
                            PacketInfo pi = new PacketInfo(paras[0], paras[1], int.Parse(paras[2]), (int)new System.ComponentModel.Int32Converter().ConvertFromString(paras[3]), int.Parse(paras[4]), data);
                            PacketList.Add(pi);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                UpdateGrid();
            }
        }

        private void Import(string[] paras,string[] buf)
        {
            try
            {
            }
            catch
            {

            }
        }

        private void Highlight(object sender, MouseButtonEventArgs e)
        {

        }

        private void ShowSent_Checked(object sender, RoutedEventArgs e)
        {
            originFilter.Remove("Client");
            UpdateGrid();
        }

        private void ShowReceived_Checked(object sender, RoutedEventArgs e)
        {
             originFilter.Remove("Server");
            UpdateGrid();
        }

        private void ShowLogin_Checked(object sender, RoutedEventArgs e)
        {
            serverFilter.Remove(ProxyServer.ServerType.Login.ToString());
            UpdateGrid();
        }

        private void ShowMap_Checked(object sender, RoutedEventArgs e)
        {
            serverFilter.Remove(ProxyServer.ServerType.Map.ToString());
           UpdateGrid();
        }

        private void ShowValidation_Checked(object sender, RoutedEventArgs e)
        {
            serverFilter.Remove(ProxyServer.ServerType.Validation.ToString());
            UpdateGrid();
        }

        private void ShowSent_Unchecked(object sender, RoutedEventArgs e)
        {
            originFilter.Add("Client");
            UpdateGrid();
        }

        private void ShowReceived_Unchecked(object sender, RoutedEventArgs e)
        {
            originFilter.Add("Server");
            UpdateGrid();
        }

        private void ShowLogin_Unchecked(object sender, RoutedEventArgs e)
        {
            serverFilter.Add(ProxyServer.ServerType.Login.ToString());
            UpdateGrid();
        }

        private void ShowMap_Unchecked(object sender, RoutedEventArgs e)
        {
            serverFilter.Add(ProxyServer.ServerType.Map.ToString());
            UpdateGrid();
        }

        private void ShowValidation_Unchecked(object sender, RoutedEventArgs e)
        {
            serverFilter.Add(ProxyServer.ServerType.Validation.ToString());
            UpdateGrid();
        }

        private void ClearPackets(object sender, MouseButtonEventArgs e)
        {
            PacketList.Clear();
            UpdateGrid();
        }
        private void IconLabel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://icons8.com/");
        }
        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    TargetServerName = String.Empty;
                    break;
                case 1:
                    TargetIP.Text = "211.13.229.49";
                    TargetPort.Text = "12200";
                    TargetServerName = "Lupinus";
                    break;
                case 2:
                    TargetIP.Text = "211.13.229.57";
                    TargetPort.Text = "12200";
                    TargetServerName = "Clover";
                    break;
                case 3:
                    TargetIP.Text = "211.13.229.65";
                    TargetPort.Text = "12200";
                    TargetServerName = "Freesia";
                    break;
                case 4:
                    TargetIP.Text = "211.13.229.73";
                    TargetPort.Text = "12200";
                    TargetServerName = "Zinnia";
                    break;
            }
        }

        private void SaftetyLock_Checked(object sender, RoutedEventArgs e)
        {
            PartLogin.IsEnabled = false;
            PartMap.IsEnabled = false;
            PartValidation.IsEnabled = false;
            ServerAsTarget.IsEnabled = false;
            ClientAsTarget.IsEnabled = false;
            SimulatedPacket.IsEnabled = false;
            Send.IsEnabled = false;
        }

        private void SaftetyLock_Unchecked(object sender, RoutedEventArgs e)
        {
            PartLogin.IsEnabled = true;
            PartMap.IsEnabled = true;
            PartValidation.IsEnabled = true;
            ServerAsTarget.IsEnabled = true;
            ClientAsTarget.IsEnabled = true;
            SimulatedPacket.IsEnabled = true;
            Send.IsEnabled = true;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string strbuf = new TextRange(SimulatedPacket.Document.ContentStart, SimulatedPacket.Document.ContentEnd).Text;
            strbuf = strbuf.Replace(" ", "");
            strbuf = strbuf.Replace("\r\n", "");
            byte[] buf = SagaLib.Conversions.HexStr2Bytes(strbuf);
            GameServerSession s = CurrentLoginServer;
            ProxyClient c = CurrentLoginClient;
            if ((bool)ServerAsTarget.IsChecked)
            {
                Packets.Server.RedirectUniversal p = new Packets.Server.RedirectUniversal();
                p.data = buf;
                if ((bool)PartLogin.IsChecked)
                    s = CurrentLoginServer;
                else if ((bool)PartMap.IsChecked)
                    s = CurrentMapServer;
                else if ((bool)PartValidation.IsChecked)
                    s = CurrentValidationServer;
                else
                    return;
                s.OnRedirectPacket(p);
                s.netIO.SendPacket(p);
            }
            else if ((bool)ClientAsTarget.IsChecked)
            {
                Packets.Client.RedirectUniversal p = new Packets.Client.RedirectUniversal();
                p.data = buf;
                if ((bool)PartLogin.IsChecked)
                    c = CurrentLoginClient;
                else if ((bool)PartMap.IsChecked)
                    c = CurrentMapClient;
                else if ((bool)PartValidation.IsChecked)
                    c = CurrentValidationClient;
                else
                    return;
                c.OnRedirectPacket(p);
                c.netIO.SendPacket(p);
            }
        }

        private void ToEventScript_Click(object sender, RoutedEventArgs e)
        {
            ScriptBuilder.Instance.Import(this.PacketList);
            ScriptBuilder.Instance.Export();
        }

        private void LoadPacketList(object sender, MouseButtonEventArgs e)
        {
            try
            {
                LoginClientPacketFactory.Instance.Init("LoginClientPackets.csv", Encoding.ASCII);
                LoginServerPacketFactory.Instance.Init("LoginServerPackets.csv", Encoding.ASCII);
                ValidationClientPacketFactory.Instance.Init("ValidationClientPackets.csv", Encoding.ASCII);
                ValidationServerPacketFactory.Instance.Init("ValidationServerPackets.csv", Encoding.ASCII);
                MapClientPacketFactory.Instance.Init("MapClientPackets.csv", Encoding.ASCII);
                MapServerPacketFactory.Instance.Init("MapServerPackets.csv", Encoding.ASCII);
                UpdateGrid();
            }
            catch (Exception ex)
            {
                Message.AppendText(ex.Message);
            }
        }
    }
}
