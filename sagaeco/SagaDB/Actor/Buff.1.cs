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
        
        public bool Poison
        {
            get
            {
                return buffs[0].Test(0x1);
            }
            set
            {
                buffs[0].SetValue(0x1, value);
            }
        }

        public bool Stone
        {
            get
            {
                return buffs[0].Test(0x2);
            }
            set
            {
                buffs[0].SetValue(0x2, value);
            }
        }

        public bool Paralysis
        {
            get
            {
                return buffs[0].Test(0x4);
            }
            set
            {
                buffs[0].SetValue(0x4, value);
            }
        }

        public bool Sleep
        {
            get
            {
                return buffs[0].Test(0x8);
            }
            set
            {
                buffs[0].SetValue(0x8, value);
            }
        }

        public bool Silence
        {
            get
            {
                return buffs[0].Test(0x10);
            }
            set
            {
                buffs[0].SetValue(0x10, value);
            }
        }

        public bool SpeedDown
        {
            get
            {
                return buffs[0].Test(0x20);
            }
            set
            {
                buffs[0].SetValue(0x20, value);
            }
        }

        public bool Confused
        {
            get
            {
                return buffs[0].Test(0x40);
            }
            set
            {
                buffs[0].SetValue(0x40, value);
            }
        }

        public bool Frosen
        {
            get
            {
                return buffs[0].Test(0x80);
            }
            set
            {
                buffs[0].SetValue(0x80, value);
            }
        }

        public bool Stun
        {
            get
            {
                return buffs[0].Test(0x100);
            }
            set
            {
                buffs[0].SetValue(0x100, value);
            }
        }

        public bool Dead
        {
            get
            {
                return buffs[0].Test(0x200);
            }
            set
            {
                buffs[0].SetValue(0x200, value);
            }
        }

        public bool CannotMove
        {
            get
            {
                return buffs[0].Test(0x400);
            }
            set
            {
                buffs[0].SetValue(0x400, value);
            }
        }

        public bool PoisonResist
        {
            get
            {
                return buffs[0].Test(0x800);
            }
            set
            {
                buffs[0].SetValue(0x800, value);
            }
        }

        public bool StoneResist
        {
            get
            {
                return buffs[0].Test(0x1000);
            }
            set
            {
                buffs[0].SetValue(0x1000, value);
            }
        }

        public bool ParalysisResist
        {
            get
            {
                return buffs[0].Test(0x2000);
            }
            set
            {
                buffs[0].SetValue(0x2000, value);
            }
        }

        public bool SleepResist
        {
            get
            {
                return buffs[0].Test(0x4000);
            }
            set
            {
                buffs[0].SetValue(0x4000, value);
            }
        }

        public bool SilenceResist
        {
            get
            {
                return buffs[0].Test(0x8000);
            }
            set
            {
                buffs[0].SetValue(0x8000, value);
            }
        }

        public bool SpeedDownResist
        {
            get
            {
                return buffs[0].Test(0x10000);
            }
            set
            {
                buffs[0].SetValue(0x10000, value);
            }
        }

        public bool ConfuseResist
        {
            get
            {
                return buffs[0].Test(0x20000);
            }
            set
            {
                buffs[0].SetValue(0x20000, value);
            }
        }

        public bool FrosenResist
        {
            get
            {
                return buffs[0].Test(0x40000);
            }
            set
            {
                buffs[0].SetValue(0x40000, value);
            }
        }

        public bool FaintResist
        {
            get
            {
                return buffs[0].Test(0x80000);
            }
            set
            {
                buffs[0].SetValue(0x80000, value);
            }
        }

        public bool Sit
        {
            get
            {
                return buffs[0].Test(0x100000);
            }
            set
            {
                buffs[0].SetValue(0x100000, value);
            }
        }

        public bool Spirit
        {
            get
            {
                return buffs[0].Test(0x200000);
            }
            set
            {
                buffs[0].SetValue(0x200000, value);
            }
        }
        #endregion

    }
}
