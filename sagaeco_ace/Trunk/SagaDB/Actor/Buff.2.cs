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
        /// 狂战士
        /// </summary>
        public bool Berserker
        {
            get
            {
                return buffs[1].Test(0x1);
            }
            set
            {
                buffs[1].SetValue(0x1, value);
            }
        }

        /// <summary>
        /// 诅咒
        /// </summary>
        public bool Curse
        {
            get
            {
                return buffs[1].Test(0x2);
            }
            set
            {
                buffs[1].SetValue(0x2, value);
            }
        }

        /// <summary>
        /// 透视
        /// </summary>
        public bool Perspective
        {
            get
            {
                return buffs[1].Test(0x4);
            }
            set
            {
                buffs[1].SetValue(0x4, value);
            }
        }

        /// <summary>
        /// 浮游
        /// </summary>
        public bool Float
        {
            get
            {
                return buffs[1].Test(0x8);
            }
            set
            {
                buffs[1].SetValue(0x8, value);
            }
        }

        /// <summary>
        /// 水中呼吸
        /// </summary>
        public bool BreathingInWater
        {
            get
            {
                return buffs[1].Test(0x10);
            }
            set
            {
                buffs[1].SetValue(0x10, value);
            }
        }

        /// <summary>
        /// 透明
        /// </summary>
        public bool Transparent
        {
            get
            {
                return buffs[1].Test(0x20);
            }
            set
            {
                buffs[1].SetValue(0x20, value);
            }
        }

        /// <summary>
        /// 不死
        /// </summary>
        public bool Undead
        {
            get
            {
                return buffs[1].Test(0x40);
            }
            set
            {
                buffs[1].SetValue(0x40, value);
            }
        }

        /// <summary>
        /// 蘑菇
        /// </summary>
        public bool Mushroom
        {
            get
            {
                return buffs[1].Test(0x80);
            }
            set
            {
                buffs[1].SetValue(0x80, value);
            }
        }

        /// <summary>
        /// 硬直
        /// </summary>
        public bool Stiff
        {
            get
            {
                return buffs[1].Test(0x100);
            }
            set
            {
                buffs[1].SetValue(0x100, value);
            }
        }

        /// <summary>
        /// 咒缚
        /// </summary>
        public bool TheDamed
        {
            get
            {
                return buffs[1].Test(0x200);
            }
            set
            {
                buffs[1].SetValue(0x200, value);
            }
        }

        /// <summary>
        /// 封印
        /// </summary>
        public bool Sealed
        {
            get
            {
                return buffs[1].Test(0x400);
            }
            set
            {
                buffs[1].SetValue(0x400, value);
            }
        }

        /// <summary>
        /// 封魔
        /// </summary>
        public bool MagicSealed
        {
            get
            {
                return buffs[1].Test(0x800);
            }
            set
            {
                buffs[1].SetValue(0x800, value);
            }
        }

        /// <summary>
        /// 准备PY Possession
        /// </summary>
        public bool GetReadyPossession
        {
            get
            {
                return buffs[1].Test(0x1000);
            }
            set
            {
                buffs[1].SetValue(0x1000, value);
            }
        }

        /// <summary>
        /// 热波防御
        /// </summary>
        public bool HotGuard
        {
            get
            {
                return buffs[1].Test(0x2000);
            }
            set
            {
                buffs[1].SetValue(0x2000, value);
            }
        }

        /// <summary>
        /// 寒波防御
        /// </summary>
        public bool ColdGuard
        {
            get
            {
                return buffs[1].Test(0x4000);
            }
            set
            {
                buffs[1].SetValue(0x4000, value);
            }
        }

        /// <summary>
        /// 真空防御
        /// </summary>
        public bool VacuumGuard
        {
            get
            {
                return buffs[1].Test(0x8000);
            }
            set
            {
                buffs[1].SetValue(0x8000, value);
            }
        }

        /// <summary>
        /// 猛毒
        /// </summary>
        public bool DeadlyPoison
        {
            get
            {
                return buffs[1].Test(0x10000);
            }
            set
            {
                buffs[1].SetValue(0x10000, value);
            }
        }

        /// <summary>
        /// 神圣羽毛
        /// </summary>
        public bool HolyFeather
        {
            get
            {
                return buffs[1].Test(0x20000);
            }
            set
            {
                buffs[1].SetValue(0x20000, value);
            }
        }

        /// <summary>
        /// 乌龟架势
        /// </summary>
        public bool ConstructionOfTheTurtle
        {
            get
            {
                return buffs[1].Test(0x40000);
            }
            set
            {
                buffs[1].SetValue(0x40000, value);
            }
        }

        /// <summary>
        /// 必中阵
        /// </summary>
        public bool FormationOfDodgeless
        {
            get
            {
                return buffs[1].Test(0x80000);
            }
            set
            {
                buffs[1].SetValue(0x80000, value);
            }
        }

        /// <summary>
        /// 短 剑延迟取消
        /// </summary>
        public bool ShortSwordDelayCancel
        {
            get
            {
                return buffs[1].Test(0x100000);
            }
            set
            {
                buffs[1].SetValue(0x100000, value);
            }
        }

        /// <summary>
        /// 延迟取消
        /// </summary>
        public bool DelayCancel
        {
            get
            {
                return buffs[1].Test(0x200000);
            }
            set
            {
                buffs[1].SetValue(0x200000, value);
            }
        }

        /// <summary>
        /// 斧延迟取消
        /// </summary>
        public bool AxeDelayCancel
        {
            get
            {
                return buffs[1].Test(0x400000);
            }
            set
            {
                buffs[1].SetValue(0x400000, value);
            }
        }

        /// <summary>
        /// 矛延迟取消
        /// </summary>
        public bool SpearDelayCancel
        {
            get
            {
                return buffs[1].Test(0x800000);
            }
            set
            {
                buffs[1].SetValue(0x800000, value);
            }
        }

        /// <summary>
        /// 弓延迟取消
        /// </summary>
        public bool BowDelayCancel
        {
            get
            {
                return buffs[1].Test(0x1000000);
            }
            set
            {
                buffs[1].SetValue(0x1000000, value);
            }
        }

        /// <summary>
        /// 斩击抵抗
        /// </summary>
        public bool DefenseSlash
        {
            get
            {
                return buffs[1].Test(0x2000000);
            }
            set
            {
                buffs[1].SetValue(0x2000000, value);
            }
        }

        /// <summary>
        /// 戳刺抵抗
        /// </summary>
        public bool DefenseStub
        {
            get
            {
                return buffs[1].Test(0x4000000);
            }
            set
            {
                buffs[1].SetValue(0x4000000, value);
            }
        }

        /// <summary>
        /// 打击抵抗
        /// </summary>
        public bool DefenseBlow
        {
            get
            {
                return buffs[1].Test(0x8000000);
            }
            set
            {
                buffs[1].SetValue(0x8000000, value);
            }
        }

        /// <summary>
        /// 再生
        /// </summary>
        public bool Revive
        {
            get
            {
                return buffs[1].Test(0x10000000);
            }
            set
            {
                buffs[1].SetValue(0x10000000, value);
            }
        }

        /// <summary>
        /// 这是什么
        /// </summary>
        public bool PetUp
        {
            get
            {
                return buffs[1].Test(0x20000000);
            }
            set
            {
                buffs[1].SetValue(0x20000000, value);
            }
        }

        /// <summary>
        /// 点火
        /// </summary>
        public bool Ignition
        {
            get
            {
                return buffs[1].Test(0x40000000);
            }
            set
            {
                buffs[1].SetValue(0x40000000, value);
            }
        }
        #endregion

    }
}
