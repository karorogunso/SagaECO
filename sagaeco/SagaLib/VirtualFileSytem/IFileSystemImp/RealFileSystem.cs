using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaLib.VirtualFileSystem
{
    public class RealFileSystem : IFileSystem
    {
        string rootPath = ".";
        #region IFileSystem Members

        public bool Init(string path)
        {
            if (path != "")
                rootPath = path + "/";
            else
                rootPath = "";
            return true;
        }

        public System.IO.Stream OpenFile(string path)
        {
            if (path.IndexOf(":") < 0)
            {
                return new System.IO.FileStream(rootPath + path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            }
            else
            {
                return new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            }
        }

        public string[] SearchFile(string path, string pattern)
        {
            return System.IO.Directory.GetFiles(rootPath + path, pattern);
        }

        public string[] SearchFile(string path, string pattern, System.IO.SearchOption option)
        {
            return System.IO.Directory.GetFiles(rootPath + path, pattern, option);
        }

        public void Close()
        {
        }

        #endregion
    }
}
