using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace XLSTool
{
    public class MobFactory : Factory<MobFactory, Item>
    {
        public Dictionary<uint, string> lines = new Dictionary<uint, string>();
        //public List<string> lines = new List<string>();
        //public List<uint> IDList = new List<uint>();
    }
}
