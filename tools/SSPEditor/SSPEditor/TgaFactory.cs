using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace SSPEditor.TGA
{
    public class TgaFactory : Singleton<TgaFactory>
    {
        public Dictionary<uint, string> PictLines = new Dictionary<uint, string>();

        public Image ShowTGA(ushort IconID, LoadTGA dat)
        {
            //这里要用{0:D4}补0，不能直接用{0}，因为id为100的技能实际文件名是0100而不是100。
            string TGAName = string.Format("SI_{0:D4}.TGA", IconID);
            string TGAName2 = string.Format("SI_{0:D4}.tga", IconID);
            if (LoadTGA.Files.ContainsKey(TGAName))
            {
                return dat.Extract(TGAName);
            }

            else if (LoadTGA.Files.ContainsKey(TGAName2))
            {
                return dat.Extract(TGAName2);
            }
            else return null;
        }
    }
}
