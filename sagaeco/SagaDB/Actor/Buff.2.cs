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

        public bool 狂戦士
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

        public bool 透視
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

        public bool 浮遊
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

        public bool 水中呼吸
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

        public bool 硬直
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

        public bool 呪縛
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

        public bool 封印
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

        public bool 封魔
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

        public bool 憑依準備
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

        public bool 熱波防御
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

        public bool 寒波防御
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

        public bool 真空防御
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

        public bool 猛毒
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

        public bool 亀の構え
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

        public bool 必中陣
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

        public bool 点火
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
