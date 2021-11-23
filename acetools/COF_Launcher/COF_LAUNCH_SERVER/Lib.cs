using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COF_LAUNCH_SERVER_Lib
{
    public class Files
    {
        public string name;
        public string path;
        public string md5;
    }
    public static class Conversions
    {
        public static string bytes2HexString(byte[] b)
        {
            string tmp = "";
            int i;
            for (i = 0; i < b.Length; i++)
            {
                string tmp2 = b[i].ToString("X2");
                tmp = tmp + tmp2;
            }
            return tmp;
        }
    }
}
