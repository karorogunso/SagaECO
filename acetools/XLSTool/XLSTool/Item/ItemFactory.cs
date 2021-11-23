using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace XLSTool
{
    public class ItemFactory : Factory<ItemFactory, Item>
    {
        public Dictionary<uint, string> lines = new Dictionary<uint, string>();
        //public List<string> lines = new List<string>();
        public List<uint> IDList = new List<uint>();

        public Dictionary<uint, string> PictLines = new Dictionary<uint, string>();

        public Image ShowTGA(string[] paras, LoadTGA dat)
        {
            return ShowTGA(uint.Parse(paras[2]), dat);
        }
        public Image ShowTGA(uint IconID, LoadTGA dat)
        {
            string TGAName = string.Format("{0}" + "p.TGA", IconID);
            string TGAName2 = string.Format("{0}" + "p.tga", IconID);
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
