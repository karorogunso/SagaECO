using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using System.Runtime.InteropServices;
namespace Launch
{
    public class RFClient
    {
        public class path
        {
            public string filename, filesize;
        }
        string VerFileAddress = "http://patch.gungho.jp/eco/econew.ver";

        /// <summary>
        /// 通过本地文件eco.ver获取本地版本号
        /// </summary>
        /// <returns>返回版本号</returns>
        public int LoadLocalVer()
        {
            if (File.Exists("eco.ver"))
            {
                FileStream fs = new FileStream("eco.ver", FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                int value = int.Parse(Encoding.ASCII.GetString(br.ReadBytes(3)));
                fs.Close();
                return value;
            }
            else
            {
                System.Windows.MessageBox.Show("请将登录器放置游戏主目录", "失败");
                MainWindow.instance.Dispatcher.Invoke(new Action(() => MainWindow.instance.Close()));
                return 0;
            }
        }
        /// <summary>
        /// 通过VerFileAddress获取日服的版本号
        /// </summary>
        /// <returns>返回版本号</returns>
        public int LoadRFVer()
        {
            HttpClient req = new HttpClient(VerFileAddress);
            byte[] tmp = req.GetBytes();
            MemoryStream ms = new MemoryStream(tmp);
            BinaryReader br = new BinaryReader(ms);
            int value = int.Parse(Encoding.ASCII.GetString(br.ReadBytes(3)));
            br.Close();
            ms.Close();
            return value;
        }
    }
}
