using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaLib.VirtualFileSystem
{
    public class LPKFileSystem : IFileSystem
    {
        Lpk.LpkFile lpk;
        #region IFileSystem Members

        public bool Init(string path)
        {
            try
            {
                lpk = new SagaLib.VirtualFileSystem.Lpk.LpkFile(new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read));
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return false;
            }
            return true;
        }

        public System.IO.Stream OpenFile(string path)
        {
            path = path.Replace("/", "\\");
            if (lpk.Exists(path))
            {
                return lpk.OpenFile(path);
            }
            else
                throw new System.IO.IOException("Cannot find file:" + path);
        }

        public string[] SearchFile(string path, string pattern)
        {
            return SearchFile(path, pattern, System.IO.SearchOption.AllDirectories);
        }

        public string[] SearchFile(string path, string pattern, System.IO.SearchOption option)
        {
            List<Lpk.LpkFileInfo> files = lpk.GetFileNames;
            List<string> result = new List<string>();
            if (path.Substring(path.Length - 1) != "/" && path.Substring(path.Length - 1) != "\\")
                path = path + "\\";
            path = path.Replace("/", "\\");
            pattern = pattern.Replace("*", "\\w*");
            foreach (Lpk.LpkFileInfo i in files)
            {
                if (i.Name.StartsWith(path))
                {
                    string s = i.Name.Replace(path, "");
                    string[] token = s.Split('\\');
                    if (option == System.IO.SearchOption.TopDirectoryOnly && token.Length > 1)
                        continue;
                    string filename = token[token.Length - 1];
                    if (System.Text.RegularExpressions.Regex.IsMatch(filename, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        result.Add(i.Name);
                }
            }
            return result.ToArray();
        }

        public void Close()
        {
            lpk.Close();
        }
        #endregion
    }
}
