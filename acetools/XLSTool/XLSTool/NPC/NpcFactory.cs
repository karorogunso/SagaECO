using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace XLSTool
{
    public class NpcFactory : Factory<NpcFactory, Item>
    {
        public Dictionary<uint, string> lines = new Dictionary<uint, string>();
        //public List<string> lines = new List<string>();
        //public List<uint> IDList = new List<uint>();

        public Dictionary<uint,string> PictLines = new Dictionary<uint,string>();
    }
}
