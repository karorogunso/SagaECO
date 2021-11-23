using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Actor
{
    public partial class Buff
    {
        
        #region Buffs
        public bool 最大HP減少
        {
            get
            {
                return buffs[4].Test(0x00000001);
            }
            set
            {
                buffs[4].SetValue(0x00000001, value);
            }
        }
        public bool 最大MP減少
        {
            get
            {
                return buffs[4].Test(0x00000002);
            }
            set
            {
                buffs[4].SetValue(0x00000002, value);
            }
        }
        public bool 最大SP減少
        {
            get
            {
                return buffs[4].Test(0x00000004);
            }
            set
            {
                buffs[4].SetValue(0x00000004, value);
            }
        }
        public bool 最小攻撃力減少
        {
            get
            {
                return buffs[4].Test(0x00000010);
            }
            set
            {
                buffs[4].SetValue(0x00000010, value);
            }
        }
        public bool 最大攻撃力減少
        {
            get
            {
                return buffs[4].Test(0x00000020);
            }
            set
            {
                buffs[4].SetValue(0x00000020, value);
            }
        }
        public bool 最小魔法攻撃力減少
        {
            get
            {
                return buffs[4].Test(0x00000040);
            }
            set
            {
                buffs[4].SetValue(0x00000040, value);
            }
        }
        public bool 最大魔法攻撃力減少
        {
            get
            {
                return buffs[4].Test(0x00000080);
            }
            set
            {
                buffs[4].SetValue(0x00000080, value);
            }
        }
        public bool 防御率減少
        {
            get
            {
                return buffs[4].Test(0x00000100);
            }
            set
            {
                buffs[4].SetValue(0x00000100, value);
            }
        }
        public bool 防御力減少
        {
            get
            {
                return buffs[4].Test(0x00000200);
            }
            set
            {
                buffs[4].SetValue(0x00000200, value);
            }
        }
        public bool 魔法防御率減少
        {
            get
            {
                return buffs[4].Test(0x00000400);
            }
            set
            {
                buffs[4].SetValue(0x00000400, value);
            }
        }
        public bool 魔法防御力減少
        {
            get
            {
                return buffs[4].Test(0x00000800);
            }
            set
            {
                buffs[4].SetValue(0x00000800, value);
            }
        }
        public bool 近距離命中率減少
        {
            get
            {
                return buffs[4].Test(0x00001000);
            }
            set
            {
                buffs[4].SetValue(0x00001000, value);
            }
        }
        public bool 遠距離命中率減少
        {
            get
            {
                return buffs[4].Test(0x00002000);
            }
            set
            {
                buffs[4].SetValue(0x00002000, value);
            }
        }
        public bool 魔法命中率減少
        {
            get
            {
                return buffs[4].Test(0x00004000);
            }
            set
            {
                buffs[4].SetValue(0x00004000, value);
            }
        }
        public bool 近距離回避率減少
        {
            get
            {
                return buffs[4].Test(0x00008000);
            }
            set
            {
                buffs[4].SetValue(0x00008000, value);
            }
        }
        public bool 遠距離回避率減少
        {
            get
            {
                return buffs[4].Test(0x00010000);
            }
            set
            {
                buffs[4].SetValue(0x00010000, value);
            }
        }
        public bool 魔法抵抗率減少
        {
            get
            {
                return buffs[4].Test(0x00020000);
            }
            set
            {
                buffs[4].SetValue(0x00020000, value);
            }
        }
        public bool クリティカル率減少

        {
            get
            {
                return buffs[4].Test(0x00040000);
            }
            set
            {
                buffs[4].SetValue(0x00040000, value);
            }
        }
        public bool クリティカル回避率減少
        {
            get
            {
                return buffs[4].Test(0x00080000);
            }
            set
            {
                buffs[4].SetValue(0x00080000, value);
            }
        }
        public bool HP回復率減少
        {
            get
            {
                return buffs[4].Test(0x00100000);
            }
            set
            {
                buffs[4].SetValue(0x00100000, value);
            }
        }
        public bool MP回復率減少
        {
            get
            {
                return buffs[4].Test(0x00200000);
            }
            set
            {
                buffs[4].SetValue(0x00200000, value);
            }
        }
        public bool SP回復率減少
        {
            get
            {
                return buffs[4].Test(0x00400000);
            }
            set
            {
                buffs[4].SetValue(0x00400000, value);
            }
        }
        public bool 攻撃スピード減少
        {
            get
            {
                return buffs[4].Test(0x00800000);
            }
            set
            {
                buffs[4].SetValue(0x00800000, value);
            }
        }
        public bool 詠唱スピード減少
        {
            get
            {
                return buffs[4].Test(0x01000000);
            }
            set
            {
                buffs[4].SetValue(0x01000000, value);
            }
        }
        public bool STR減少
        {
            get
            {
                return buffs[4].Test(0x02000000);
            }
            set
            {
                buffs[4].SetValue(0x02000000, value);
            }
        }
        public bool DEX減少
        {
            get
            {
                return buffs[4].Test(0x04000000);
            }
            set
            {
                buffs[4].SetValue(0x04000000, value);
            }
        }
        public bool INT減少
        {
            get
            {
                return buffs[4].Test(0x08000000);
            }
            set
            {
                buffs[4].SetValue(0x08000000, value);
            }
        }
        public bool VIT減少
        {
            get
            {
                return buffs[4].Test(0x10000000);
            }
            set
            {
                buffs[4].SetValue(0x10000000, value);
            }
        }
        public bool AGI減少
        {
            get
            {
                return buffs[4].Test(0x20000000);
            }
            set
            {
                buffs[4].SetValue(0x20000000, value);
            }
        }
        public bool MAG減少
        {
            get
            {
                return buffs[4].Test(0x40000000);
            }
            set
            {
                buffs[4].SetValue(0x40000000, value);
            }
        }
        #endregion

    }
}
