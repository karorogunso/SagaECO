using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Actor
{
    public partial class Buff
    {
        public bool 魂之手
        {
            get
            {
                return buffs[9].Test(0x1);
            }
            set
            {
                buffs[9].SetValue(0x1, value);
            }
        }
        public bool 精准攻击
        {
            get
            {
                return buffs[9].Test(0x2);
            }
            set
            {
                buffs[9].SetValue(0x2, value);
            }
        }
        public bool 恶炎
        {
            get
            {
                return buffs[9].Test(0x4);
            }
            set
            {
                buffs[9].SetValue(0x4, value);
            }
        }
        public bool 九尾狐魅惑
        {
            get
            {
                return buffs[9].Test(0x8);
            }
            set
            {
                buffs[9].SetValue(0x8, value);
            }
        }
        public bool 武装化
        {
            get
            {
                return buffs[9].Test(0x10);
            }
            set
            {
                buffs[9].SetValue(0x10, value);
            }
        }
        public bool 武装化副作用
        {
            get
            {
                return buffs[9].Test(0x20);
            }
            set
            {
                buffs[9].SetValue(0x20, value);
            }
        }
        public bool 恶魂
        {
            get
            {
                return buffs[9].Test(0x40);
            }
            set
            {
                buffs[9].SetValue(0x40, value);
            }
        }
    }
}
