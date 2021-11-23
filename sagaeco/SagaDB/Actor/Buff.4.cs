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

        public bool 最大HP上昇
        {
            get
            {
                return buffs[3].Test(0x00000001);
            }
            set
            {
                buffs[3].SetValue(0x00000001, value);
            }
        }

        public bool 最大MP上昇
        {
            get
            {
                return buffs[3].Test(0x00000002);
            }
            set
            {
                buffs[3].SetValue(0x00000002, value);
            }
        }

        public bool 最大SP上昇
        {
            get
            {
                return buffs[3].Test(0x00000004);
            }
            set
            {
                buffs[3].SetValue(0x00000004, value);
            }
        }

        public bool 移動力上昇
        {
            get
            {
                return buffs[3].Test(0x00000008);
            }
            set
            {
                buffs[3].SetValue(0x00000008, value);
            }
        }

        public bool AtkMinUp
        {
            get
            {
                return buffs[3].Test(0x00000010);
            }
            set
            {
                buffs[3].SetValue(0x00000010, value);
            }
        }

        public bool AtkMaxUp
        {
            get
            {
                return buffs[3].Test(0x00000020);
            }
            set
            {
                buffs[3].SetValue(0x00000020, value);
            }
        }

        public bool MAtkMinUp
        {
            get
            {
                return buffs[3].Test(0x00000040);
            }
            set
            {
                buffs[3].SetValue(0x00000040, value);
            }
        }

        public bool MAtkMaxUp
        {
            get
            {
                return buffs[3].Test(0x00000080);
            }
            set
            {
                buffs[3].SetValue(0x00000080, value);
            }
        }

        public bool DefUp
        {
            get
            {
                return buffs[3].Test(0x00000100);
            }
            set
            {
                buffs[3].SetValue(0x00000100, value);
            }
        }

        public bool DefAddUp
        {
            get
            {
                return buffs[3].Test(0x00000200);
            }
            set
            {
                buffs[3].SetValue(0x00000200, value);
            }
        }

        public bool MDefUp
        {
            get
            {
                return buffs[3].Test(0x00000400);
            }
            set
            {
                buffs[3].SetValue(0x00000400, value);
            }
        }

        public bool MDefAddUp
        {
            get
            {
                return buffs[3].Test(0x00000800);
            }
            set
            {
                buffs[3].SetValue(0x00000800, value);
            }
        }

        public bool HitMeleeUp
        {
            get
            {
                return buffs[3].Test(0x00001000);
            }
            set
            {
                buffs[3].SetValue(0x00001000, value);
            }
        }

        public bool HitRangeUp
        {
            get
            {
                return buffs[3].Test(0x00002000);
            }
            set
            {
                buffs[3].SetValue(0x00002000, value);
            }
        }

        public bool HitMagicUp
        {
            get
            {
                return buffs[3].Test(0x00004000);
            }
            set
            {
                buffs[3].SetValue(0x00004000, value);
            }
        }

        public bool AvdMeleeUp
        {
            get
            {
                return buffs[3].Test(0x00008000);
            }
            set
            {
                buffs[3].SetValue(0x00008000, value);
            }
        }

        public bool AvdRangeUp
        {
            get
            {
                return buffs[3].Test(0x00010000);
            }
            set
            {
                buffs[3].SetValue(0x00010000, value);
            }
        }

        public bool AvdMagicUp
        {
            get
            {
                return buffs[3].Test(0x00020000);
            }
            set
            {
                buffs[3].SetValue(0x00020000, value);
            }
        }

        public bool HitCriUp
        {
            get
            {
                return buffs[3].Test(0x00040000);
            }
            set
            {
                buffs[3].SetValue(0x00040000, value);
            }
        }

        public bool AvdCriUp
        {
            get
            {
                return buffs[3].Test(0x00080000);
            }
            set
            {
                buffs[3].SetValue(0x00080000, value);
            }
        }

        public bool HPRecUp
        {
            get
            {
                return buffs[3].Test(0x00100000);
            }
            set
            {
                buffs[3].SetValue(0x00100000, value);
            }
        }

        public bool MPRecUp
        {
            get
            {
                return buffs[3].Test(0x00200000);
            }
            set
            {
                buffs[3].SetValue(0x00200000, value);
            }
        }

        public bool SPRecUp
        {
            get
            {
                return buffs[3].Test(0x00400000);
            }
            set
            {
                buffs[3].SetValue(0x00400000, value);
            }
        }

        public bool AspdUp
        {
            get
            {
                return buffs[3].Test(0x00800000);
            }
            set
            {
                buffs[3].SetValue(0x00800000, value);
            }
        }

        public bool CspdUp
        {
            get
            {
                return buffs[3].Test(0x01000000);
            }
            set
            {
                buffs[3].SetValue(0x01000000, value);
            }
        }
        public bool STR上昇
        {
            get
            {
                return buffs[3].Test(0x02000000);
            }
            set
            {
                buffs[3].SetValue(0x02000000, value);
            }
        }
        public bool DEX上昇
        {
            get
            {
                return buffs[3].Test(0x04000000);
            }
            set
            {
                buffs[3].SetValue(0x04000000, value);
            }
        }
        public bool INT上昇
        {
            get
            {
                return buffs[3].Test(0x08000000);
            }
            set
            {
                buffs[3].SetValue(0x08000000, value);
            }
        }
        public bool VIT上昇
        {
            get
            {
                return buffs[3].Test(0x10000000);
            }
            set
            {
                buffs[3].SetValue(0x10000000, value);
            }
        }
        public bool AGI上昇
        {
            get
            {
                return buffs[3].Test(0x20000000);
            }
            set
            {
                buffs[3].SetValue(0x20000000, value);
            }
        }
        public bool MAG上昇
        {
            get
            {
                return buffs[3].Test(0x40000000);
            }
            set
            {
                buffs[3].SetValue(0x40000000, value);
            }
        }
        #endregion

    }
}
