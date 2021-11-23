using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Actor
{
    public partial class Buff
    {
        public bool 审判
        {
            get
            {
                return buffs[10].Test(0x20);
            }
            set
            {
                buffs[10].SetValue(0x20, value);
            }
        }
        public bool 食物效果
        {
            get
            {
                return buffs[10].Test(0x400);
            }
            set
            {
                buffs[10].SetValue(0x400, value);
            }
        }
    }
}
