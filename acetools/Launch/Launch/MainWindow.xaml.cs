using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using SagaLib;
using System.IO;
using System.Diagnostics;

namespace Launch
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
        public static MainWindow instance;
		public MainWindow()
		{
			this.InitializeComponent();
            instance = this;
			// 在此点下面插入创建对象所需的代码。
		}


        string configUrl = "http://lalala.yuki.cc/patch_list.rlf";
        Encryption enc = new Encryption();
        string title, notice, official, register;
        public static Dictionary<string, string> patch = new Dictionary<string, string>();
        public static string patchUrl;
        public static string NoticeUrl, OfficialUrl, RegisterUrl;
        public static string LabelLink, LabelLink2, LabelLink3, LabelLink4;
        public static bool isClose;
        public static string CloseMessage;
        public static int ver_rf, ver_limit ,ver_local;
        RFClient rfc = new RFClient();
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            string url;
            RandomStartButtonPic("",out url);//随机UI
            /////////////////////////读取COF配置文件/////////////////////////
            #region 读取COF配置文件
            try
            {
                BitmapImage bi = new BitmapImage(new Uri(url));
                ButtonImage.Source = bi;
                HttpClient client = new HttpClient(configUrl);
                byte[] tmp = client.GetBytes();
                tmp = enc.Decrypt(tmp, 0);
                MemoryStream ms = new MemoryStream(tmp);
                BinaryReader br = new BinaryReader(ms);
                if (br.ReadUInt32() == 0x23333333)
                {
                    Encoding enco = Encoding.UTF8;
                    title = enco.GetString(br.ReadBytes(br.ReadByte()));
                    notice = enco.GetString(br.ReadBytes(br.ReadByte()));
                    official = enco.GetString(br.ReadBytes(br.ReadByte()));
                    register = enco.GetString(br.ReadBytes(br.ReadByte()));
                    int count = br.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        patch.Add(enco.GetString(br.ReadBytes(br.ReadInt32())), enco.GetString(br.ReadBytes(br.ReadByte())));
                    }
                    patchUrl = enco.GetString(br.ReadBytes(br.ReadInt32()));
                    ver_limit = int.Parse(enco.GetString(br.ReadBytes(br.ReadInt32())));
                    isClose = false;
                    CloseMessage = " ";
                    LinkLabel.Content = enco.GetString(br.ReadBytes(br.ReadInt32()));
                    LabelLink = enco.GetString(br.ReadBytes(br.ReadInt32()));
                    LinkLabel2.Content = enco.GetString(br.ReadBytes(br.ReadInt32()));
                    LabelLink2 = enco.GetString(br.ReadBytes(br.ReadInt32()));
                    LinkLabel3.Content = enco.GetString(br.ReadBytes(br.ReadInt32()));
                    LabelLink3 = enco.GetString(br.ReadBytes(br.ReadInt32()));
                    LinkLabel4.Content = enco.GetString(br.ReadBytes(br.ReadInt32()));
                    LabelLink4 = enco.GetString(br.ReadBytes(br.ReadInt32()));

                    if (isClose)
                    {
                        MessageBox.Show(CloseMessage,"登录器已关闭");
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("配置文件读取出错！\r\n绑定信息不符，请与管理员联系。", "读取失败");
                    Close();
                }
            }
            catch (System.Net.WebException ex)
            {
                MessageBox.Show("远程列表提取失败！\r\n请联系管理员，错误内容：\r\n" + ex.Message, "读取失败");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("配置文件读取失败！\r\n配置文件可能已被损坏，或绑定信息不符", "读取失败");
                Close();
            }
            #endregion
            ///////////////////////结束读取COF配置文件///////////////////////
            //////////////////////////获取版本信息//////////////////////////
            #region 获取版本信息
            try
            {
                ver_local = rfc.LoadLocalVer();
                ver_rf = rfc.LoadRFVer();
                LocalVerText.Content = "当前本地版本:" + ver_local.ToString();
                NeedVerText.Content = "要求版本至:" + ver_limit.ToString();
                RFVerText.Content = "日服版本:" + ver_rf.ToString();
                ver_limit = ver_rf;
            }
            catch (System.Net.WebException ex)
            {
                MessageBox.Show("获取日服版本信息失败！\r\n请联系管理员，错误内容：\r\n" + ex.Message, "读取失败");
            }
            #endregion
            ///////////////////////结束获取版本信息/////////////////////////


            if (ver_local <= ver_rf)
            {
                if (ver_local < ver_limit)
                {
                    Button.Content = "更新游戏";
                    lb_state.Content = "客户端需要更新日服版本，请更新游戏。";
                }
                else
                {
                    NeedVerText.Content = "已达到所需版本";
                    Button.Content = "开始游戏";
                    lb_state.Content = "客户端达到了所需版本号。"   ;
                }
            }
            else
            {
                Close();
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)//大按钮
        {
            Button.IsEnabled = false;
            bool value = false;
            RFUpdata rfu = new RFUpdata();
            ThreadPool.QueueUserWorkItem((state) =>
            {
                if (rfu.check())
                {
                    MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.Pb_Total.Value = 100; }));
                    System.Diagnostics.Process.Start("eco.exe", "/launch");
                    this.Dispatcher.Invoke(new Action(() => Close()));

                }
           }, null);

        }
        void RandomStartButtonPic(string s,out string y)
        {
            Random random = new Random();
            y = "http://papapa.otaku.asia/image/startbutton/button" + random.Next(0, 13).ToString() + ".png";
        }


        private void Button_Click(object sender, RoutedEventArgs e)//关闭按钮
        {
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)//左键按下事件
        {
            DragMove();
        }

        private void LinkLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", LabelLink);
        }
        private void LinkLabel2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", LabelLink);
        }
        private void LinkLabel3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", LabelLink);
        }
        private void LinkLabel4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", LabelLink);
        }
    }
}