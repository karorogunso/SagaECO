using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using System.Runtime.InteropServices;
namespace COF_LAUNCH
{
    class RFPatch
    {
        class path
        {
            public string filename, filesize;
        }
        [DllImport("kernel32.dll", EntryPoint = "GetCurrentDirectoryA", CharSet = CharSet.Ansi)]
        private static extern int GetCurrentDirectory(int MaxCount, StringBuilder s_Text); 

        string VerFileAddress = "http://patch.gungho.jp/eco/econew.ver";
        string PathFileAddress = "http://patch.gungho.jp/eco/D00000";
        string TempFolderName = "temp";
        
        ECODatFile ECOdat;

        public void acquire()
        {
            int RfVer = LoadRFVer();
            int LocalVer = LoadLocalVer();
            int UpdataVer = LocalVer + 1;
            if (LocalVer < RfVer)
            {
                PatchingFromDFL();
                //DownloadFiles(LoadFilesList(UpdataVer), UpdataVer);
                //ExtractFiles(LoadFilesList(UpdataVer));
            }
        }
        /// <summary>
        /// 移动其他文件
        /// </summary>
        void LoadAddList()
        {
            string path = TempFolderName + "\\add_list.txt";
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            byte count = byte.Parse(sr.ReadLine());
            while(!sr.EndOfStream)
            {
                string file = sr.ReadLine();
                System.IO.File.Move(TempFolderName + "\\" + file, file);
            }
        }
        /// <summary>
        /// 根据DFL文件对客户端进行修补
        /// </summary>
        void PatchingFromDFL()
        {
            List<FileInfo> DFLFlies = LoadDFList();
            for (int i = 0; i < DFLFlies.Count; i++)
            {
                DFLDatFile DFLdat = OpenDFL(DFLFlies[i]);
                ECODatFile ECOdat = OpenECO(DFLFlies[i]);
                foreach (KeyValuePair<string,Header> item in DFLdat.Files)
                {
                    if (ECOdat.Files.ContainsKey(item.Key))
                    {
                        ECOdat.Files.Remove(item.Key);
                        ECOdat.Files.Add(item.Key, item.Value);
                    }
                    else
                        ECOdat.Files.Add(item.Key, item.Value);
                }
                ECOdat.Repack(System.Environment.CurrentDirectory + DisposeDFLName(DFLFlies[i].Name));
                ECOdat.Close();
            }
        }
        /// <summary>
        /// 打开DFL文件
        /// </summary>
        /// <param name="file">DFL文件</param>
        /// <returns>DFL的文件内容</returns>
        DFLDatFile OpenDFL(FileInfo file)
        {
            DFLDatFile DFLdat = new DFLDatFile();
            
            DFLdat.Open(TempFolderName + "\\" + file.Name);
            return DFLdat;
        }
        /// <summary>
        /// 根据DFL打开ECO文件
        /// </summary>
        /// <param name="file">DFL文件</param>
        /// <returns>ECO的文件内容</returns>
        ECODatFile OpenECO(FileInfo file)
        {
            ECODatFile ECOdat = new ECODatFile();
            string path = System.Environment.CurrentDirectory + DisposeDFLName(file.Name);
            ECOdat.Open(path);
            return ECOdat;
        }
        /// <summary>
        /// 根据DFL文件名称获取ECO路径
        /// </summary>
        /// <param name="name">DFL文件名</param>
        /// <returns>路径</returns>
        string DisposeDFLName(string name)
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
        /// 获取临时文件夹里的DFL文件列表
        /// </summary>
        /// <returns>文件列表</returns>
        List<FileInfo> LoadDFList()
        {
            List<FileInfo> dflFiles = new List<FileInfo>();
            DirectoryInfo di = new DirectoryInfo(TempFolderName);
            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Name.Contains(".dfl"))
                    dflFiles.Add(file);
            }
            return dflFiles;
        }
        /// <summary>
        /// 从下载的cab文件中提出文件
        /// </summary>
        /// <param name="FilesList">文件列表</param>
        void ExtractFiles(List<path> FilesList)
        {
            StringBuilder s_WorkDir = new StringBuilder(500);
            GetCurrentDirectory(500, s_WorkDir);
            CabLib.Extract et = new CabLib.Extract();
            for (int i = 0; i < FilesList.Count; i++)
            {
                if (FilesList[i].filename.Contains(".cab"))
                    et.ExtractFile(s_WorkDir + "\\" + FilesList[i].filename, s_WorkDir + TempFolderName);
            }
        }
        /// <summary>
        /// 根据补丁文件列表下载文件
        /// </summary>
        /// <param name="FilesList">文件列表</param>
        /// <param name="ver">版本号</param>
        void DownloadFiles(List<path> FilesList, int ver)
        {
            if (Directory.Exists(TempFolderName))
            {
                DirectoryInfo di = new DirectoryInfo(TempFolderName);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                Directory.Delete(TempFolderName);
            }
            Directory.CreateDirectory(TempFolderName);

            for (int i = 0; i < FilesList.Count; i++)
            {
                string FileAddress = PathFileAddress + ver + "/" + FilesList[i].filename;
                HttpClient req = new HttpClient(FileAddress);
                byte[] tmp = req.GetBytes();
                using (FileStream fs = new FileStream(TempFolderName + "\\" + FilesList[i].filename, FileMode.Create, FileAccess.Write))
                {
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(tmp);
                }
            }
        }
        /// <summary>
        /// 通过版本号获取补丁文件列表
        /// </summary>
        /// <param name="ver">版本号</param>
        List<path> LoadFilesList(int ver)
        {
            List<path> filelist = new List<path>();
            string ListAddress = PathFileAddress + ver.ToString() + "/dl_list.txt";
            HttpClient req = new HttpClient(ListAddress);
            byte[] tmp = req.GetBytes();
            MemoryStream ms = new MemoryStream(tmp);
            StreamReader sr = new StreamReader(ms);
            string[] paras;
            while (!sr.EndOfStream)
            {
                path path = new RFPatch.path();
                string line;
                line = sr.ReadLine();
                paras = line.Split(',');
                path.filename = paras[0];
                path.filesize = paras[1];
                filelist.Add(path);
            }
            return filelist;
        }
        /// <summary>
        /// 通过VerFileAddress获取日服的版本号
        /// </summary>
        /// <returns>返回版本号</returns>
        int LoadRFVer()
        {
            HttpClient req = new HttpClient(VerFileAddress);
            byte[] tmp = req.GetBytes();
            MemoryStream ms = new MemoryStream(tmp);
            BinaryReader br = new BinaryReader(ms);
            return int.Parse(Encoding.ASCII.GetString(br.ReadBytes(3)));
        }
        /// <summary>
        /// 通过本地文件eco.ver获取本地版本号
        /// </summary>
        /// <returns>返回版本号</returns>
        int LoadLocalVer()
        {
            FileStream fs = new FileStream("eco.ver", FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            return int.Parse(Encoding.ASCII.GetString(br.ReadBytes(3)));
        }
    }
}
