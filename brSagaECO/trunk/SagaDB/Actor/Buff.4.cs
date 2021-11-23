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

        /// <summary>
        /// 最大HP上昇
        /// </summary>
        public bool MaxHPUp
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

        /// <summary>
        /// 最大MP上昇
        /// </summary>
        public bool MaxMPUp
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

        /// <summary>
        /// 最大SP上昇
        /// </summary>
        public bool MaxSPUp
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

        /// <summary>
        /// 移動力上昇
        /// </summary>
        public bool MoveSpeedUp
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

        /// <summary>
        /// 最小攻撃力上昇
        /// </summary>
        public bool MinAtkUp
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

        /// <summary>
        /// 最大攻撃力上昇
        /// </summary>
        public bool MaxAtkUp
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

        /// <summary>
        /// 最小魔法攻撃力上昇
        /// </summary>
        public bool MinMagicAtkUp
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

        /// <summary>
        /// 最大魔法攻撃力上昇
        /// </summary>
        public bool MaxMagicAtkUp
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

        /// <summary>
        /// 防御率上昇
        /// </summary>
        public bool DefRateUp
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

        /// <summary>
        /// 防御力上昇
        /// </summary>
        public bool DefUp
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

        /// <summary>
        /// 魔法防御率上昇
        /// </summary>
        public bool MagicDefRateUp
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

        /// <summary>
        /// 魔法防御力上昇
        /// </summary>
        public bool MagicDefUp
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

        /// <summary>
        /// 近距離命中率上昇
        /// </summary>
        public bool ShortHitUp
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

        /// <summary>
        /// 遠距離命中率上昇
        /// </summary>
        public bool LongHitUp
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
        
        /// <summary>
        /// 魔法命中率上昇
        /// </summary>
        public bool MagicHitUp
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

        /// <summary>
        /// 近距離回避率上昇
        /// </summary>
        public bool ShortDodgeUp
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

        /// <summary>
        /// 遠距離回避上昇
        /// </summary>
        public bool LongDodgeUp
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

        /// <summary>
        /// 魔法抵抗上昇
        /// </summary>
        public bool MagicAvoidUp
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

        /// <summary>
        /// クリティカル率上昇
        /// </summary>
        public bool CriticalRateUp
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

        /// <summary>
        /// クリティカル回避率上昇
        /// </summary>
        public bool CriticalDodgeUp
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
        
        /// <summary>
        /// HP回復率上昇
        /// </summary>
        public bool HPRegenUp
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

        /// <summary>
        /// MP回復率上昇
        /// </summary>
        public bool MPRegenUp
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

        /// <summary>
        /// SP回復率上昇
        /// </summary>
        public bool SPRegenUp
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

        /// <summary>
        /// 攻撃スピード上昇
        /// </summary>
        public bool AttackSpeedUp
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

        /// <summary>
        /// 詠唱スピード上昇
        /// </summary>
        public bool CastSpeedUp
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

        /// <summary>
        /// STR上昇
        /// </summary>
        public bool STRUp
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

        /// <summary>
        /// DEX上昇
        /// </summary>
        public bool DEXUp
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

        /// <summary>
        /// INT上昇
        /// </summary>
        public bool INTUp
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

        /// <summary>
        /// VIT上昇
        /// </summary>
        public bool VITUp
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

        /// <summary>
        /// AGI上昇
        /// </summary>
        public bool AGIUp
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

        /// <summary>
        /// MAG上昇
        /// </summary>
        public bool MagUp
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
