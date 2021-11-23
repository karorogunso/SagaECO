using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using CabLib;

namespace Launch
{
    public partial class RFUpdata
    {
        [DllImport("kernel32.dll", EntryPoint = "GetCurrentDirectoryA", CharSet = CharSet.Ansi)]
        private static extern int GetCurrentDirectory(int MaxCount, StringBuilder s_Text);
        string PathFileAddress = "http://patch.gungho.jp/eco/D00000";
        string TempFolderName = "temp";
        RFClient rfc = new RFClient();
        DateTime time = DateTime.Now;

        public bool check()
        {
            MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "正在检测是否需要从日服更新..."; }));
            int localver = rfc.LoadLocalVer();
            System.Threading.Thread.Sleep(500);
            MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.Pb_Total.Value = 8; }));
            if (localver < MainWindow.ver_limit)
            {
                MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "需要进行更新."; }));
                System.Threading.Thread.Sleep(200);
                while (localver < MainWindow.ver_limit)
                {
                    MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "需要更新" + (MainWindow.ver_limit - localver).ToString() + "个版本."; }));
                    System.Threading.Thread.Sleep(300);
                    int nextver = localver + 1;
                    MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "开始更新" + nextver.ToString() + "版本"; }));
                    System.Threading.Thread.Sleep(300);
                    try
                    {
                        if (updata(nextver))//下载新版本
                        {
                            MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_evolve.Content = ""; }));
                            ExtractFiles(nextver);//解压文件
                            DisposeDFL();//更新下载的文件
                            MoveFiles();//移动文件
                            MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "版本" + nextver.ToString() + "更新完成"; }));
                            localver = rfc.LoadLocalVer();
                            MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.LocalVerText.Content = "当前本地版本:" + localver.ToString(); }));
                            System.Threading.Thread.Sleep(2000);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.ToString());
                    }
                    int needver = MainWindow.ver_limit - localver;
                    MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.Pb_Total.Value = 8 + needver /77; }));
                }
            }
            MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.Pb_Total.Value = 85; }));
            MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "客户端已更新到了所需版本号。"; }));
            System.Threading.Thread.Sleep(1000);
            MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "开始与COF服务器进行文件检测。"; }));
            System.Threading.Thread.Sleep(1000);
            COFUpdata cu = new COFUpdata();
            if (cu.check())
                return true;
            else
                return false;
        }
        public void DisposeDFL()
        {
            //获取DFL文件
            List<FileInfo> dflFiles = new List<FileInfo>();
            DirectoryInfo di = new DirectoryInfo(TempFolderName + "\\cab");
            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Name.Contains(".dfl"))
                    dflFiles.Add(file);
            }
            MainWindow.instance.Dispatcher.Invoke(new Action(() => {MainWindow.instance.lb_state.Content = "*开始升级..."; }));
            System.Threading.Thread.Sleep(300);
            //进行修补
            for (int i = 0; i < dflFiles.Count; i++)
            {
                //DFLDatFile DFLdat = OpenDFL(dflFiles[i], TempFolderName + "\\cab");
                ECODatFile ECOdat = OpenECO(dflFiles[i]);
                DFLDatFile DFLdat = new DFLDatFile();
                string path = TempFolderName + "\\cab" + "\\" + dflFiles[i].Name;
                DFLdat.ExtractDFL(ECOdat, path);
                /*foreach (KeyValuePair<string, Header> item in DFLdat.Files)内存问题被重写！
                {
                    if (ECOdat.Files.ContainsKey(item.Key))
                    {
                        MainWindow.instance.Dispatcher.Invoke(new Action(() => {MainWindow.instance.lb_state.Content = "替换：" + item.Key; }));
                        ECOdat.Replace(item.Key, item.Value.filecontent);
                        //ECOdat.Files.Remove(item.Key);
                        //ECOdat.Files.Add(item.Key, item.Value);
                    }
                    else
                    {
                        MainWindow.instance.Dispatcher.Invoke(new Action(() => {MainWindow.instance.lb_state.Content = "添加：" + item.Key; }));
                        ECOdat.Add(item.Key, item.Value.filecontent);
                    }
                }
                //ECOdat.Repack(System.Environment.CurrentDirectory + rfp.DisposeDFLName(dflFiles[i].Name));*/
                ECOdat.Close();
            }
        }
        public bool MoveFiles()
        {
            string path = TempFolderName + "\\cab" + "\\add_list.txt";
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            byte count = byte.Parse(sr.ReadLine());
            while (!sr.EndOfStream)
            {
                string file = sr.ReadLine();
                if (File.Exists(file))
                {
                    FileInfo fi = new FileInfo(file);
                    fi.Attributes = FileAttributes.Normal;
                    File.Delete(file);
                }
                MainWindow.instance.Dispatcher.Invoke(new Action(() => {MainWindow.instance.lb_state.Content = "移动：" + Path.GetFileName(file); }));
                string folder = System.IO.Path.GetDirectoryName(file);
                if (!System.IO.Directory.Exists(folder) && folder != "")
                    System.IO.Directory.CreateDirectory(folder);
                System.IO.File.Move(TempFolderName + "\\cab" + "\\" + file, file);
            }
            File.Delete("eco.ver");
            File.Move(TempFolderName + "\\eco.ver", "eco.ver");
            fs.Close();
            return true;
        }
        /// <summary>
        /// 根据DFL打开ECO文件
        /// </summary>
        /// <param name="file">DFL文件</param>
        /// <returns>ECO的文件内容</returns>
        public ECODatFile OpenECO(FileInfo file)
        {
            ECODatFile ECOdat = new ECODatFile();
            string path = System.Environment.CurrentDirectory + DisposeDFLName(file.Name);
            ECOdat.Open(path);
            return ECOdat;
        }
        /// <summary>
        /// 打开DFL文件
        /// </summary>
        /// <param name="file">DFL文件</param>
        /// <returns>DFL的文件内容</returns>
        public DFLDatFile OpenDFL(FileInfo file, string tfn)
        {
            DFLDatFile DFLdat = new DFLDatFile();

            DFLdat.Open(tfn + "\\" + file.Name);
            return DFLdat;
        }
        /// <summary>
        /// 根据DFL文件名称获取ECO路径
        /// </summary>
        /// <param name="name">DFL文件名</param>
        /// <returns>路径</returns>
        public string DisposeDFLName(string name)
        {
            string outname = "";
            string[] param;
            name = name.Substring(0, name.IndexOf('.'));
            param = name.Split('_');
            for (int i = 0; i < param.Length; i++)
            {
                outname += "\\" + param[i];
            }
            outname = outname + ".hed";
            return outname;
        }
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="ver">版本号</param>
        /// <returns></returns>
        public bool ExtractFiles(int ver)
        {
            MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "开始解压文件..."; }));
            System.Threading.Thread.Sleep(300);
            List<RFClient.path> FilesList = LoadFilesList(ver);
            StringBuilder s_WorkDir = new StringBuilder(500);
            GetCurrentDirectory(500, s_WorkDir);
            Extract et = new Extract();
            try
            {
                for (int i = 0; i < FilesList.Count; i++)
                {
                    if (FilesList[i].filename.Contains(".cab"))
                    {
                        MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "解压：" + FilesList[i].filename; }));
                        et.ExtractFile(s_WorkDir + "\\" + TempFolderName + "\\" + FilesList[i].filename, s_WorkDir + "\\" + TempFolderName + "\\cab");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 通过版本号获取补丁文件列表
        /// </summary>
        /// <param name="ver">版本号</param>
        public List<RFClient.path> LoadFilesList(int ver)
        {
            List<RFClient.path> filelist = new List<RFClient.path>();
            string ListAddress = PathFileAddress + ver.ToString() + "/dl_list.txt";
            HttpClient req = new HttpClient(ListAddress);
            byte[] tmp = req.GetBytes();
            MemoryStream ms = new MemoryStream(tmp);
            StreamReader sr = new StreamReader(ms);
            string[] paras;
            while (!sr.EndOfStream)
            {
                RFClient.path path = new RFClient.path();
                string line;
                line = sr.ReadLine();
                paras = line.Split(',');
                path.filename = paras[0];
                path.filesize = paras[1];
                filelist.Add(path);
            }
            ms.Close();
            sr.Close();
            return filelist;
        }
        public bool DeleteTemp(DirectoryInfo di)
        {
            try
            {
                foreach (FileInfo file in di.GetFiles())//首先删除文件
                {
                    file.Attributes = FileAttributes.Normal;
                    file.Delete();
                }
                foreach (DirectoryInfo dire in di.GetDirectories())
                {
                    if (dire.Exists)
                    {
                        dire.Attributes = FileAttributes.Normal & FileAttributes.Directory;
                        DeleteTemp(dire);
                        dire.Delete(true);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "在删除临时文件夹时发生了错误！"; }));
                return false;
            }
            
        }
        public bool updata(int ver)
        {
            try
            {
                List<RFClient.path> filelist = LoadFilesList(ver);
                MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "文件列表获取完成，开始下载..."; }));
                System.Threading.Thread.Sleep(200);
                if (Directory.Exists(TempFolderName))//如果存在temp文件夹则删除
                {
                    try
                    {
                        {
                            DirectoryInfo di = new DirectoryInfo(TempFolderName);
                            DeleteTemp(di);
                            di.Delete(true);
                        }
                        //Directory.Delete(TempFolderName);
                    }
                    catch (Exception ex)
                    {
                        MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = ex.ToString(); }));
                    }
                }
                Directory.CreateDirectory(TempFolderName);
                for (int i = 0; i < filelist.Count; i++)
                {
                    MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = "(" + i.ToString() + "/" + filelist.Count.ToString() + ")文件:" + filelist[i].filename; }));
                    try
                    {

                        string FileAddress = PathFileAddress + ver + "/" + filelist[i].filename;
                        System.Net.WebClient wc = new System.Net.WebClient();
                        System.Threading.AutoResetEvent sync = new System.Threading.AutoResetEvent(false);
                        wc.DownloadProgressChanged += DownloadProgressChanged;
                        wc.DownloadFileCompleted += (s, e) => { sync.Set(); };
                        wc.DownloadFileAsync(new Uri(FileAddress), TempFolderName + "\\" + filelist[i].filename);
                        sync.WaitOne();

                        /*HttpClient req = new HttpClient(FileAddress);
                        req.StatusUpdate += onDownloadUpdate;
                        byte[] tmp = req.GetBytes();
                        using (FileStream fs = new FileStream(TempFolderName + "\\" + filelist[i].filename, FileMode.Create, FileAccess.Write))
                        {
                            BinaryWriter bw = new BinaryWriter(fs);
                            bw.Write(tmp);
                            bw.Close();
                        }*/
                        //舍弃的HttpClient部分,原因：造成内存溢出
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("下载文件出错！\r\n错误编号：004", "下载失败");
                        return false;
                    }
                }
                System.Threading.Thread.Sleep(200);
                /*if (Directory.Exists(TempFolderName))//如果存在temp文件夹则删除
                {
                    try
                    {
                        {
                            DirectoryInfo di = new DirectoryInfo(TempFolderName);
                            DeleteTemp(di);
                            di.Delete(true);
                        }
                        //Directory.Delete(TempFolderName);
                    }
                    catch (Exception ex)
                    {
                        MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_state.Content = ex.ToString(); }));
                    }
                }*/
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return false;
            }
        }
        void DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            if ((DateTime.Now - time).TotalMilliseconds > 80)
            {
                time = DateTime.Now;
                MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.Pb_Single.Value = (int)(((float)e.BytesReceived / e.TotalBytesToReceive) * 100); }));
                MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_evolve.Content = e.BytesReceived + "/" + e.TotalBytesToReceive; }));
            }
        }
        /*void onDownloadUpdate(object sender, Launch.StatusUpdateEventArgs e)
        {
            if ((DateTime.Now - time).TotalMilliseconds > 80)
            {
                time = DateTime.Now;
                MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.Pb_Single.Value = (int)(((float)e.BytesGot / e.BytesTotal) * 100); }));
                MainWindow.instance.Dispatcher.Invoke(new Action(() => { MainWindow.instance.lb_evolve.Content = e.BytesGot + "/" + e.BytesTotal; }));
            }
        }*/
    }
}
