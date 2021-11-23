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
        public bool アクロバットイベイジョン
        {
            get
            {
                return buffs[6].Test(0x00000001);
            }
            set
            {
                buffs[6].SetValue(0x00000001, value);
            }
        }

        /// <summary>
        /// オーバーワーク
        /// </summary>
        public bool OverWork
        {
            get
            {
                return buffs[6].Test(0x00000002);
            }
            set
            {
                buffs[6].SetValue(0x00000002, value);
            }
        }

        /// <summary>
        /// ディレイキャンセル
        /// </summary>
        public bool DelayCancel3RD
        {
            get
            {
                return buffs[6].Test(0x00000004);
            }
            set
            {
                buffs[6].SetValue(0x00000004, value);
            }
        }

        /// <summary>
        /// 赤くなる
        /// </summary>
        public bool TurningRed
        {
            get
            {
                return buffs[6].Test(0x00000008);
            }
            set
            {
                buffs[6].SetValue(0x00000008, value);
            }
        }

        /// <summary>
        /// フェニックス
        /// </summary>
        public bool Phoenix
        {
            get
            {
                return buffs[6].Test(0x00000010);
            }
            set
            {
                buffs[6].SetValue(0x00000010, value);
            }
        }

        /// <summary>
        /// スタミナテイク
        /// </summary>
        public bool StaminaTake
        {
            get
            {
                return buffs[6].Test(0x00000020);
            }
            set
            {
                buffs[6].SetValue(0x00000020, value);
            }
        }


        //public bool 未知BUFFER
        //{
        //    get
        //    {
        //        return buffs[6].Test(0x00000040);
        //    }
        //    set
        //    {
        //        buffs[6].SetValue(0x00000040, value);
        //    }
        //}

        /// <summary>
        /// マナの守護
        /// </summary>
        public bool ManaGuard
        {
            get
            {
                return buffs[6].Test(0x00000080);
            }
            set
            {
                buffs[6].SetValue(0x00000080, value);
            }
        }

        /// <summary>
        /// チャンプモンスターキラー状態
        /// </summary>
        public bool StateOfMonsterKillerChamp
        {
            get
            {
                return buffs[6].Test(0x00000100);
            }
            set
            {
                buffs[6].SetValue(0x00000100, value);
            }
        }

        /// <summary>
        /// 竜眼開放
        /// </summary>
        public bool OpenDragonEye
        {
            get
            {
                return buffs[6].Test(0x00000200);
            }
            set
            {
                buffs[6].SetValue(0x00000200, value);
            }
        }

        /// <summary>
        /// 温泉効果
        /// </summary>
        public bool EffectOfHotSpring
        {
            get
            {
                return buffs[6].Test(0x00000400);
            }
            set
            {
                buffs[6].SetValue(0x00000400, value);
            }
        }

        /// <summary>
        /// 武器属性無効化
        /// </summary>
        public bool WeaponElementInvalid
        {
            get
            {
                return buffs[6].Test(0x00000800);
            }
            set
            {
                buffs[6].SetValue(0x00000800, value);
            }
        }

        /// <summary>
        /// 防御属性無効化
        /// </summary>
        public bool GuardElementInvalid
        {
            get
            {
                return buffs[6].Test(0x00001000);
            }
            set
            {
                buffs[6].SetValue(0x00001000, value);
            }
        }

        /// <summary>
        /// ロケットブースター点火
        /// </summary>
        public bool RocketBoosterIgintion
        {
            get
            {
                return buffs[6].Test(0x00002000);
            }
            set
            {
                buffs[6].SetValue(0x00002000, value);
            }
        }

        /// <summary>
        /// 斧头达人
        /// </summary>
        public bool MasterOfAxe
        {
            get
            {
                return buffs[6].Test(0x00004000);
            }
            set
            {
                buffs[6].SetValue(0x00004000, value);
            }
        }

        /// <summary>
        /// 剑达人
        /// </summary>
        public bool MasterOfSword
        {
            get
            {
                return buffs[6].Test(0x00008000);
            }
            set
            {
                buffs[6].SetValue(0x00008000, value);
            }
        }

        /// <summary>
        /// 矛达人
        /// </summary>
        public bool MasterOfSpear
        {
            get
            {
                return buffs[6].Test(0x00010000);
            }
            set
            {
                buffs[6].SetValue(0x00010000, value);
            }
        }

        /// <summary>
        /// 枪达人
        /// </summary>
        public bool MasterOfGun
        {
            get
            {
                return buffs[6].Test(0x00020000);
            }
            set
            {
                buffs[6].SetValue(0x00020000, value);
            }
        }

        /// <summary>
        /// 三转主要技能效果强力增强
        /// </summary>
        public bool MainSkillPowerUp3RD
        {
            get
            {
                return buffs[6].Test(0x00040000);
            }
            set
            {
                buffs[6].SetValue(0x00040000, value);
            }
        }

        /// <summary>
        /// 三转HP增强
        /// </summary>
        public bool MaxHPUp3RD
        {
            get
            {
                return buffs[6].Test(0x00080000);
            }
            set
            {
                buffs[6].SetValue(0x00080000, value);
            }
        }

        /// <summary>
        /// 三转MP增强
        /// </summary>
        public bool MaxMPUp3RD
        {
            get
            {
                return buffs[6].Test(0x00100000);
            }
            set
            {
                buffs[6].SetValue(0x00100000, value);
            }
        }

        /// <summary>
        /// 三转SP增强
        /// </summary>
        public bool MaxSPUp3RD
        {
            get
            {
                return buffs[6].Test(0x00200000);
            }
            set
            {
                buffs[6].SetValue(0x000200000, value);
            }
        }

        /// <summary>
        /// 三转ATK增强
        /// </summary>
        public bool AtkUp3RD
        {
            get
            {
                return buffs[6].Test(0x00400000);
            }
            set
            {
                buffs[6].SetValue(0x000400000, value);
            }
        }

        /// <summary>
        /// 三转MATK增强
        /// </summary>
        public bool MagicAtkUp3RD
        {
            get
            {
                return buffs[6].Test(0x00800000);
            }
            set
            {
                buffs[6].SetValue(0x00800000, value);
            }
        }

        /// <summary>
        /// 三转DEF增强
        /// </summary>
        public bool DefUp3RD
        {
            get
            {
                return buffs[6].Test(0x01000000);
            }
            set
            {
                buffs[6].SetValue(0x01000000, value);
            }
        }

        /// <summary>
        /// 三转MDEF增强
        /// </summary>
        public bool MagicDefUp3RD
        {
            get
            {
                return buffs[6].Test(0x02000000);
            }
            set
            {
                buffs[6].SetValue(0x02000000, value);
            }
        }

        /// <summary>
        /// 三转CSPD增强
        /// </summary>
        public bool CastSpeedUp3RD
        {
            get
            {
                return buffs[6].Test(0x04000000);
            }
            set
            {
                buffs[6].SetValue(0x04000000, value);
            }
        }

        /// <summary>
        /// 三转AVOID增强
        /// </summary>
        public bool AvoidUp3RD
        {
            get
            {
                return buffs[6].Test(0x08000000);
            }
            set
            {
                buffs[6].SetValue(0x08000000, value);
            }
        }

        /// <summary>
        /// 三转HIT增强
        /// </summary>
        public bool HitUp3RD
        {
            get
            {
                return buffs[6].Test(0x10000000);
            }
            set
            {
                buffs[6].SetValue(0x10000000, value);
            }
        }

        /// <summary>
        /// 三转PAYL增强
        /// </summary>
        public bool PaylUp3RD
        {
            get
            {
                return buffs[6].Test(0x20000000);
            }
            set
            {
                buffs[6].SetValue(0x20000000, value);
            }
        }

        /// <summary>
        /// 三转CAPA增强
        /// </summary>
        public bool CAPAUp3RD
        {
            get
            {
                return buffs[6].Test(0x40000000);
            }
            set
            {
                buffs[6].SetValue(0x40000000, value);
            }
        }
        
        /*public bool Unknow18
        {
            get
            {
                return buffs[6].Test(0x80000000);
            }
            set
            {
                buffs[6].SetValue(0x80000000, value);
            }
        }*/
        #endregion

    }
}
