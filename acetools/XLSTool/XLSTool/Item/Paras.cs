using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLSTool
{
    public class Paras
    {
        public string[] paras;
        public string[] npcparas;
        public static Paras Instance
        {
            get { return SingletonHolder.instance; }
            set { SingletonHolder.instance = value; }
        }

        private sealed class SingletonHolder
        {
            internal static Paras instance = new Paras();
            static SingletonHolder()
            {
            }
        }
    }
}
