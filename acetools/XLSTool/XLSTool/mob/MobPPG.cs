using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace XLSTool
{
    public class MobPPGSettings
    {
        public string[] paras;
        public MobPPGSettings(string[] paras)
        {
            this.paras = paras;
        }
        string GetString(int idx)
        {
            if (idx < this.paras.Length)
                return this.paras[idx];
            else
                return "";
        }
        [CategoryAttribute("掉落物品"), DescriptionAttribute("格式：物品ID|掉率")]
        public string 掉落1
        {
            get { return GetString(113); }
            set { this.paras[113] = value; }
        }
        [CategoryAttribute("掉落物品"), DescriptionAttribute("格式：物品ID|掉率")]
        public string 掉落2
        {
            get { return GetString(114); }
            set { this.paras[114] = value; }
        }
        [CategoryAttribute("掉落物品"), DescriptionAttribute("格式：物品ID|掉率")]
        public string 掉落3
        {
            get { return GetString(115); }
            set { this.paras[115] = value; }
        }
        [CategoryAttribute("掉落物品"), DescriptionAttribute("格式：物品ID|掉率")]
        public string 掉落4
        {
            get { return GetString(116); }
            set { this.paras[116] = value; }
        }
        [CategoryAttribute("掉落物品"), DescriptionAttribute("格式：物品ID|掉率")]
        public string 掉落5
        {
            get { return GetString(117); }
            set { this.paras[117] = value; }
        }
        [CategoryAttribute("掉落物品"), DescriptionAttribute("格式：物品ID|掉率")]
        public string 掉落6
        {
            get { return GetString(118); }
            set { this.paras[118] = value; }
        }
        [CategoryAttribute("掉落物品"), DescriptionAttribute("格式：物品ID|掉率")]
        public string 掉落7
        {
            get { return GetString(119); }
            set { this.paras[119] = value; }
        }
        [CategoryAttribute("掉落物品")]
        public string 掉落图章
        {
            get { return GetString(120); }
            set { this.paras[120] = value; }
        }
        [CategoryAttribute("掉落物品")]
        public string 图章的概率
        {
            get { return GetString(121); }
            set { this.paras[121] = value; }
        }
    }
}