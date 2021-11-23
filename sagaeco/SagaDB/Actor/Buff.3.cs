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

        public bool 武器の無属性上昇
        {
            get
            {
                return buffs[2].Test(0x1);
            }
            set
            {
                buffs[2].SetValue(0x1, value);
            }
        }
        /// <summary>
        /// 武器火属性上升
        /// </summary>
        public bool WeaponFire
        {
            get
            {
                return buffs[2].Test(0x2);
            }
            set
            {
                buffs[2].SetValue(0x2, value);
            }
        }
        /// <summary>
        /// 武器水属性上升
        /// </summary>
        public bool WeaponWater
        {
            get
            {
                return buffs[2].Test(0x4);
            }
            set
            {
                buffs[2].SetValue(0x4, value);
            }
        }
        /// <summary>
        /// 武器风属性上升
        /// </summary>
        public bool WeaponWind
        {
            get
            {
                return buffs[2].Test(0x8);
            }
            set
            {
                buffs[2].SetValue(0x8, value);
            }
        }
        /// <summary>
        /// 武器地属性上升
        /// </summary>
        public bool WeaponEarth
        {
            get
            {
                return buffs[2].Test(0x10);
            }
            set
            {
                buffs[2].SetValue(0x10, value);
            }
        }
        /// <summary>
        /// 武器光属性上升
        /// </summary>
        public bool WeaponHoly
        {
            get
            {
                return buffs[2].Test(0x20);
            }
            set
            {
                buffs[2].SetValue(0x20, value);
            }
        }
        /// <summary>
        /// 武器暗属性上升
        /// </summary>
        public bool WeaponDark
        {
            get
            {
                return buffs[2].Test(0x40);
            }
            set
            {
                buffs[2].SetValue(0x40, value);
            }
        }
        public bool 武器の無属性減少
        {
            get
            {
                return buffs[2].Test(0x00000080);
            }
            set
            {
                buffs[2].SetValue(0x00000080, value);
            }
        }
        public bool 武器の火属性減少
        {
            get
            {
                return buffs[2].Test(0x00000100);
            }
            set
            {
                buffs[2].SetValue(0x00000100, value);
            }
        }
        public bool 武器の水属性減少
        {
            get
            {
                return buffs[2].Test(0x00000200);
            }
            set
            {
                buffs[2].SetValue(0x00000200, value);
            }
        }
        public bool 武器の風属性減少
        {
            get
            {
                return buffs[2].Test(0x00000400);
            }
            set
            {
                buffs[2].SetValue(0x00000400, value);
            }
        }
        public bool 武器の土属性減少
        {
            get
            {
                return buffs[2].Test(0x00000800);
            }
            set
            {
                buffs[2].SetValue(0x00000800, value);
            }
        }
        public bool 武器の光属性減少
        {
            get
            {
                return buffs[2].Test(0x00001000);
            }
            set
            {
                buffs[2].SetValue(0x00001000, value);
            }
        }
        public bool 武器の闇属性減少
        {
            get
            {
                return buffs[2].Test(0x00002000);
            }
            set
            {
                buffs[2].SetValue(0x00002000, value);
            }
        }
        public bool 体の無属性上昇
        {
            get
            {
                return buffs[2].Test(0x4000);
            }
            set
            {
                buffs[2].SetValue(0x4000, value);
            }
        }
        /// <summary>
        /// 防御火属性上升
        /// </summary>
        public bool ShieldFire
        {
            get
            {
                return buffs[2].Test(0x8000);
            }
            set
            {
                buffs[2].SetValue(0x8000, value);
            }
        }
        /// <summary>
        /// 防御水属性上升
        /// </summary>
        public bool ShieldWater
        {
            get
            {
                return buffs[2].Test(0x10000);
            }
            set
            {
                buffs[2].SetValue(0x10000, value);
            }
        }
        /// <summary>
        /// 防御风属性上升
        /// </summary>
        public bool ShieldWind
        {
            get
            {
                return buffs[2].Test(0x20000);
            }
            set
            {
                buffs[2].SetValue(0x20000, value);
            }
        }
        /// <summary>
        /// 防御地属性上升
        /// </summary>
        public bool ShieldEarth
        {
            get
            {
                return buffs[2].Test(0x40000);
            }
            set
            {
                buffs[2].SetValue(0x40000, value);
            }
        }
        /// <summary>
        /// 防御光属性上升
        /// </summary>
        public bool ShieldHoly
        {
            get
            {
                return buffs[2].Test(0x80000);
            }
            set
            {
                buffs[2].SetValue(0x80000, value);
            }
        }
        /// <summary>
        /// 防御暗属性上升
        /// </summary>
        public bool ShieldDark
        {
            get
            {
                return buffs[2].Test(0x100000);
            }
            set
            {
                buffs[2].SetValue(0x100000, value);
            }
        }
        public bool 体の無属性減少
        {
            get
            {
                return buffs[2].Test(0x00200000);
            }
            set
            {
                buffs[2].SetValue(0x00200000, value);
            }
        }
        public bool 体の火属性減少
        {
            get
            {
                return buffs[2].Test(0x00400000);
            }
            set
            {
                buffs[2].SetValue(0x00400000, value);
            }
        }
        public bool 体の水属性減少
        {
            get
            {
                return buffs[2].Test(0x100000);
            }
            set
            {
                buffs[2].SetValue(0x100000, value);
            }
        }
        public bool 体の風属性減少
        {
            get
            {
                return buffs[2].Test(0x01000000);
            }
            set
            {
                buffs[2].SetValue(0x01000000, value);
            }
        }
        public bool 体の土属性減少
        {
            get
            {
                return buffs[2].Test(0x02000000);
            }
            set
            {
                buffs[2].SetValue(0x02000000, value);
            }
        }
        public bool 体の光属性減少
        {
            get
            {
                return buffs[2].Test(0x04000000);
            }
            set
            {
                buffs[2].SetValue(0x04000000, value);
            }
        }
        public bool 体の闇属性減少
        {
            get
            {
                return buffs[2].Test(0x08000000);
            }
            set
            {
                buffs[2].SetValue(0x08000000, value);
            }
        }

        #endregion

    }
}
