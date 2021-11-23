using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace XLSTool
{
    public class FaceFactory : Factory<FaceFactory, Item>
    {
        public Dictionary<uint, string> lines = new Dictionary<uint, string>();
    }
}
