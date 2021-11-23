using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Actor
{
    public partial class Buff
    {
        BitMask[] buffs = new BitMask[12] { new BitMask(), new BitMask(), new BitMask(), new BitMask(), new BitMask(), new BitMask(), new BitMask(), new BitMask(), new BitMask(), new BitMask(), new BitMask(), new BitMask() };
        public BitMask[] Buffs { get { return this.buffs; } set { this.buffs = value; } }

        public void Clear()
        {
            buffs[0].Value = 0;
            buffs[1].Value = 0;
            buffs[2].Value = 0;
            buffs[3].Value = 0;
            buffs[4].Value = 0;
            buffs[5].Value = 0;
            buffs[6].Value = 0;
            buffs[7].Value = 0;
            buffs[8].Value = 0;
            buffs[9].Value = 0;
            buffs[10].Value = 0;
            buffs[11].Value = 0;
        }
    }
}
