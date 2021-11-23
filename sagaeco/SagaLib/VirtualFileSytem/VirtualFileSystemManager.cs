using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaLib.VirtualFileSystem
{
    public enum FileSystems
    {
        Real,
        LPK,
    }

    public class VirtualFileSystemManager : Singleton<VirtualFileSystemManager>
    {
        IFileSystem fs;
        public bool Init(FileSystems type, string path)
        {
            if (fs != null)
                fs.Close();
            switch (type)
            {
                case FileSystems.Real:
                    fs = new RealFileSystem();
                    break;
                case FileSystems.LPK:
                    fs = new LPKFileSystem();
                    break;
            }
            return fs.Init(path);
        }

        public IFileSystem FileSystem
        {
            get
            {
                return fs;
            }
        }
    }
}
