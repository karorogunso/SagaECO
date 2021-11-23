using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using SagaLib;
using System.Security.Cryptography;

namespace Launch
{
    public partial class COFUpdata
    {
        DateTime time = DateTime.Now;
        public bool check()
        {
            try
            {
                int count = 0;
                foreach (string st in MainWindow.patch.Keys)
                {
                    string i = st;
                    string folder = System.IO.Path.GetDirectoryName(i);
                    string foldername = System.IO.Path.GetFileName(folder);
                    string filename = System.IO.Path.GetFileName(i);
                    if (foldername.EndsWith(".hed"))//自动解冻填入
                    {
                        ECODatFile file = new ECODatFile();
                        file.Open(folder);
                        if (file.Exists(filename))//文件重复时
                        {
                            byte[] buf = file.Extract(filename);
                            MD5 md5 = MD5.Create();
                            string hash = lib.Conversions.bytes2HexString(md5.ComputeHash(buf));
                            if (hash != MainWindow.patch[st])
                            {
                                MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "文件需要更新，下载更新中..."; }));
                                MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "替换文件:" + i; }));
                                try
                                {
                                    HttpClient http = new HttpClient((MainWindow.patchUrl) + "/" + i.Replace("\\", "/"));
                                    http.StatusUpdate += onDownloadUpdate;
                                    buf = http.GetBytes();
                                    file.Replace(filename, buf);
                                }
                                catch
                                {
                                    System.Windows.MessageBox.Show("下载 " + i + "文件出错！\r\n错误编号：003", "下载失败");
                                    file.Close();
                                    return false;
                                }
                            }
                        }
                        else//文件不存在时
                        {
                            MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "添加文件:" + i; }));
                            try
                            {
                                HttpClient http = new HttpClient((MainWindow.patchUrl) + "/" + i.Replace("\\", "/"));
                                http.StatusUpdate += onDownloadUpdate;
                                byte[] buf = http.GetBytes();
                                file.Add(filename, buf);
                            }
                            catch
                            {
                                System.Windows.MessageBox.Show("下载 " + i + "文件出错！\r\n错误编号：002", "下载失败");
                                file.Close();
                                return false;
                            }
                        }
                        file.Close();
                        count++;
                        MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.Pb_Single.Value = 100; }));
                        //pb_Total.Value = (int)((float)(count / frm_Main.patch.Count) * 100);
                    }
                    else//如果endwith不是.hed（直接处理文件
                    {
                        if (folder != "")
                        {
                            if (!System.IO.Directory.Exists(folder))
                                System.IO.Directory.CreateDirectory(folder);
                        }
                        bool needDownload = false;
                        if (!System.IO.File.Exists(i))
                            needDownload = true;
                        if (!needDownload)
                        {
                            System.IO.FileStream fs = new System.IO.FileStream(i, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                            MD5 md5 = MD5.Create();
                            string hash = lib.Conversions.bytes2HexString(md5.ComputeHash(fs));
                            fs.Close();
                            if (MainWindow.patch[st] != hash)
                                needDownload = true;
                        }
                        if (needDownload)
                        {
                            MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "下载文件:" + i; })); //+ "(" + valuecount + "/" + frm_Main.patch.Count + ")";
                            try
                            {

                                HttpClient http = new HttpClient(MainWindow.patchUrl + "/" + i.Replace("\\", "/"));
                                http.StatusUpdate += onDownloadUpdate;
                                byte[] buf = http.GetBytes();
                                System.IO.FileStream fs = new System.IO.FileStream(i, System.IO.FileMode.Create);
                                fs.Write(buf, 0, buf.Length);
                                fs.Close();
                            }
                            catch
                            {
                                System.Windows.MessageBox.Show("下载 " + i + "文件出错！\r\n错误编号：001", "下载失败");

                                return false;
                            }
                        }
                        count++;
                        MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.Pb_Single.Value = 100; }));
                        //MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.Pb_Total.Value = (int)((float)count / MainWindow.patch.Count * 100); }));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "更新完成"; }));
            return true;
        }
        void onDownloadUpdate(object sender, Launch.StatusUpdateEventArgs e)
        {
            if ((DateTime.Now - time).TotalMilliseconds > 80)
            {
                time = DateTime.Now;
                MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.Pb_Single.Value = (int)(((float)e.BytesGot / e.BytesTotal) * 100); }));
                MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_evolve.Content = e.BytesGot + "/" + e.BytesTotal; }));
            }
        }
    }
}
