using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace LaunchCab
{
    ///

    /// CAB文件压缩解压类
    ///

    public class Cab
    {

        #region 属性列表 Properties

        private string _cabFileName;
        ///

        /// 生成或者解压的Cab文件路径
        ///

        public string CabFileName
        {
            get { return _cabFileName; }
            set { _cabFileName = value; }
        }
        
        private List<string> _fileList = new List<string>();
        ///

        /// “将被压缩”或者“解压出”的文件列表
        ///

        public List<string> FileList
        {
            get { return _fileList; }
            set { _fileList = value; }
        }

        ///

        /// 临时目录
        ///

        private string TempDir;

        #endregion

        #region 构造函数 Structure

        public Cab()
        {
            //指定并创建临时目录
            this.TempDir = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
            this.TempDir = string.Format("{0}\\CabTemp\\", this.TempDir);
            //如果存在，先删除
            if (Directory.Exists(this.TempDir)) Directory.Delete(this.TempDir, true);
            Directory.CreateDirectory(this.TempDir);
        }

        #endregion

        #region 私有方法列表 Private Methods

        ///

        /// 生成 list.txt 文件
        ///

        private string CreateListFile()
        {
            try
            {
                string listFilePath = Path.Combine(this.TempDir, "list.txt");
                string listContent = string.Empty;//文件内容
                for (int i = 0; i < this.FileList.Count; i++)
                {
                    listContent += string.Format("\"{0}\"\r\n", this.FileList[i]);
                }
                using (FileStream fs = new FileStream(listFilePath, FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                    sw.Write(listContent);
                    sw.Flush();
                    sw.Close();

                    fs.Close();
                }

                return listFilePath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        ///

        /// 执行CMD命令
        ///

        private string RunCommand(string cmdString)
        {
            Process p = new Process();
            //启动DOS程序
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;//不用Shell启动
            p.StartInfo.RedirectStandardInput = true;//重定向输入
            p.StartInfo.RedirectStandardOutput = true;//重定向输出
            p.StartInfo.CreateNoWindow = true;//不显示窗口
            p.Start();//开始进程
            p.StandardInput.WriteLine(cmdString);// 向cmd.exe输入command
            p.StandardInput.WriteLine("exit");//结束
            p.WaitForExit(60000);//等等执行完成
            string outPutString = p.StandardOutput.ReadToEnd();// 得到cmd.exe的输出
            p.Close();
            return outPutString;
        }

        ///

        /// 分析并找到cab文件，然后提取到输出目录
        ///

        private void MoveCabFile()
        {
            string cabFilePath = string.Empty;

            List<string> allFilesInTempDir = this.GetFilesFromDir(this.TempDir);
            foreach (string file in allFilesInTempDir)
            {
                if (Path.GetExtension(file).ToLower() == ".cab")
                {
                    cabFilePath = file;
                    break;
                }
                else
                {
                    //否则删除之
                    File.Delete(file);
                }
            }

            //转移文件
            File.Move(cabFilePath, this.CabFileName);
        }

        ///

        /// 从指定目录读取文件列表
        ///

        /// 要搜索的目录
        /// 后缀列表
        /// 
        private List<string> GetFilesFromDir(string dir)
        {
            List<string> files = new List<string>(Directory.GetFiles(dir));

            List<string> dirs = new List<string>(Directory.GetDirectories(dir));
            foreach (string childDir in dirs)
            {
                files.AddRange(this.GetFilesFromDir(childDir));
            }

            return files;
        }

        ///

        /// 从DOS命令返回的结果，分析并得到解压后的文件列表
        ///

        /// DOS返回的结果
        private void GetExpandedFileByCmdResult(string cmdResult)
        {
            //文件列表
            this._fileList = new List<string>();

            //分割结果
            string[] resultRowList = cmdResult.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            //关键字
            string keyString = string.Format("将 {0} 展开成", this.CabFileName.ToLower());
            foreach (string resultRow in resultRowList)
            {
                if (resultRow.Trim().StartsWith(keyString))
                {
                    string filePath = resultRow.Replace(keyString, "").Trim().Replace("。", "");
                    if (File.Exists(filePath))
                    {
                        this._fileList.Add(filePath);
                    }
                }
            }
        }

        #endregion

        #region 公有方法列表 Public Methods

        ///

        /// 制作CAB文件
        /// 【执行此方法前请先指定“FileList”属性的值】
        ///

        /// 返回的错误信息
        /// 是否成功
        public bool MakeCab(out string errorInfo)
        {
            errorInfo = string.Empty;

            try
            {
                //第一步：写临时文件　 list.txt
                string listFile = this.CreateListFile();
                if (File.Exists(listFile) == false) throw new Exception("生成文件　 list.txt　 失败！");

                //第二步：执行CMD命令
                string cmdString = "makecab /F list.txt";
                string cmdResult = this.RunCommand(cmdString);

                //第三步：分析并找到cab文件，然后提取到输出目录
                this.MoveCabFile();

                if (File.Exists(this.CabFileName))
                {
                    return true;
                }
                else
                {
                    errorInfo = string.Format("文件 {0} 生成失败！", this.CabFileName);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                return false;
            }
        }

        ///

        /// 解压缩CAB文件
        /// 【执行此方法前请先指定“CabFileName”属性的值】
        ///

        /// 返回的错误信息
        /// 是否成功
        public bool ExpandCab(out string errorInfo)
        {
            errorInfo = string.Empty;

            try
            {
                //第一步：检查源文件和目录文件夹
                if (File.Exists(this.CabFileName) == false) throw new Exception("CAB源文件不存在！请指定正确的CabFileName的值！");
                if (Directory.Exists(this.TempDir) == false) Directory.CreateDirectory(this.TempDir);

                //第二步：执行CMD命令
                string tempPath = this.TempDir.EndsWith("\\") ? this.TempDir.Substring(0, this.TempDir.Length - 1) : this.TempDir;
                string cmdString = string.Format("expand \"{0}\" \"{1}\" -f:*", this.CabFileName, tempPath);
                string cmdResult = this.RunCommand(cmdString);

                //第三步：分析得到解压的文件列表
                this.GetExpandedFileByCmdResult(cmdResult);

                return true;
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                return false;
            }
        }

        #endregion
    }
}
