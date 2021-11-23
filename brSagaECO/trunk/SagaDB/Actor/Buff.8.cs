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
        /// 三转HP吸收
        /// </summary>
        public bool HPDrain3RD
        {
            get
            {
                return buffs[7].Test(0x00000001);
            }
            set
            {
                buffs[7].SetValue(0x00000001, value);
            }
        }

        /// <summary>
        /// 三转MP吸收
        /// </summary>
        public bool MPDrain3RD
        {
            get
            {
                return buffs[7].Test(0x00000002);
            }
            set
            {
                buffs[7].SetValue(0x00000002, value);
            }
        }

        /// <summary>
        /// 三转SP吸收
        /// </summary>
        public bool SPDrain3RD
        {
            get
            {
                return buffs[7].Test(0x00000004);
            }
            set
            {
                buffs[7].SetValue(0x00000004, value);
            }
        }
        public bool 无1
        {
            get
            {
                return buffs[7].Test(0x00000008);
            }
            set
            {
                buffs[7].SetValue(0x00000008, value);
            }
        }
        public bool 无2
        {
            get
            {
                return buffs[7].Test(0x00000010);
            }
            set
            {
                buffs[7].SetValue(0x00000010, value);
            }
        }
        public bool 无3
        {
            get
            {
                return buffs[7].Test(0x00000020);
            }
            set
            {
                buffs[7].SetValue(0x00000020, value);
            }
        }
        public bool 三转波动伤害固定
        {
            get
            {
                return buffs[7].Test(0x00000040);
            }
            set
            {
                buffs[7].SetValue(0x00000040, value);
            }
        }
        public bool 三转枪连弹
        {
            get
            {
                return buffs[7].Test(0x00000080);
            }
            set
            {
                buffs[7].SetValue(0x00000080, value);
            }
        }
        public bool KillingMark
        {
            get
            {
                return buffs[7].Test(0x00000100);
            }
            set
            {
                buffs[7].SetValue(0x00000100, value);
            }
        }
        public bool 三转ATK与MATK互换
        {
            get
            {
                return buffs[7].Test(0x00000200);
            }
            set
            {
                buffs[7].SetValue(0x00000200, value);
            }
        }
        public bool 三转元素身体属性赋予
        {
            get
            {
                return buffs[7].Test(0x00000400);
            }
            set
            {
                buffs[7].SetValue(0x00000400, value);
            }
        }
        public bool 三转元素武器属性赋予
        {
            get
            {
                return buffs[7].Test(0x00000800);
            }
            set
            {
                buffs[7].SetValue(0x00000800, value);
            }
        }
        public bool 三转2足ATKUP
        {
            get
            {
                return buffs[7].Test(0x00001000);
            }
            set
            {
                buffs[7].SetValue(0x00001000, value);
            }
        }
        public bool RobotUnknowStateDown3RD
        {
            get
            {
                return buffs[7].Test(0x00002000);
            }
            set
            {
                buffs[7].SetValue(0x00002000, value);
            }
        }
        public bool WeaponFobbiden3RD
        {
            get
            {
                return buffs[7].Test(0x00004000);
            }
            set
            {
                buffs[7].SetValue(0x00004000, value);
            }
        }

        /// <summary>
        /// 三转受伤害提升伤害标记
        /// </summary>
        public bool GetDamageUpDamageMark3RD
        {
            get
            {
                return buffs[7].Test(0x00008000);
            }
            set
            {
                buffs[7].SetValue(0x00008000, value);
            }
        }

        /// <summary>
        /// 三转伤害降低精神标记
        /// </summary>
        public bool DamageReduceSpriteMark3RD
        {
            get
            {
                return buffs[7].Test(0x00010000);
            }
            set
            {
                buffs[7].SetValue(0x00010000, value);
            }
        }

        /// <summary>
        /// 三转J速
        /// </summary>
        public bool JSpeed3RD
        {
            get
            {
                return buffs[7].Test(0x00020000);
            }
            set
            {
                buffs[7].SetValue(0x00020000, value);
            }
        }

        /// <summary>
        /// 三转人血管
        /// </summary>
        public bool 三转人血管
        {
            get
            {
                return buffs[7].Test(0x00040000);
            }
            set
            {
                buffs[7].SetValue(0x00040000, value);
            }
        }

        /// <summary>
        /// 三转荆棘刺
        /// </summary>
        public bool 三转荆棘刺
        {
            get
            {
                return buffs[7].Test(0x00080000);
            }
            set
            {
                buffs[7].SetValue(0x00080000, value);
            }
        }

        /// <summary>
        /// 三转鬼人斩
        /// </summary>
        public bool DevilStance
        {
            get
            {
                return buffs[7].Test(0x00100000);
            }
            set
            {
                buffs[7].SetValue(0x00100000, value);
            }
        }

        /// <summary>
        /// 三转宙斯盾イージス
        /// </summary>
        public bool Aegis3RD
        {
            get
            {
                return buffs[7].Test(0x00200000);
            }
            set
            {
                buffs[7].SetValue(0x00200000, value);
            }
        }
        public bool 三转凭依者封印
        {
            get
            {
                return buffs[7].Test(0x00400000);
            }
            set
            {
                buffs[7].SetValue(0x00400000, value);
            }
        }
        public bool 三转四属性赋予アンプリエレメント
        {
            get
            {
                return buffs[7].Test(0x00800000);
            }
            set
            {
                buffs[7].SetValue(0x00800000, value);
            }
        }
        public bool 三转铁匠2足DEFUP
        {
            get
            {
                return buffs[7].Test(0x01000000);
            }
            set
            {
                buffs[7].SetValue(0x01000000, value);
            }
        }
        public bool 三转机器人UNKNOWS
        {
            get
            {
                return buffs[7].Test(0x02000000);
            }
            set
            {
                buffs[7].SetValue(0x02000000, value);
            }
        }
        public bool 三转禁言レストスキル
        {
            get
            {
                return buffs[7].Test(0x04000000);
            }
            set
            {
                buffs[7].SetValue(0x04000000, value);
            }
        }
        public bool 三转指定对象被会心率UPクリティカルマーキング
        {
            get
            {
                return buffs[7].Test(0x08000000);
            }
            set
            {
                buffs[7].SetValue(0x08000000, value);
            }
        }
        public bool 三转凭依保护ソウルプロテクト
        {
            get
            {
                return buffs[7].Test(0x10000000);
            }
            set
            {
                buffs[7].SetValue(0x10000000, value);
            }
        }
        public bool 三转見切り
        {
            get
            {
                return buffs[7].Test(0x20000000);
            }
            set
            {
                buffs[7].SetValue(0x20000000, value);
            }
        }
        public bool 三转魔法抗体
        {
            get
            {
                return buffs[7].Test(0x40000000);
            }
            set
            {
                buffs[7].SetValue(0x40000000, value);
            }
        }
        #endregion
    }
}
