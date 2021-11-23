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
        public bool state190
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
        /// 狂乱时间
        /// </summary>
        public bool オーバーワーク
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
        /// 拳头dc
        /// </summary>
        public bool ディレイキャンセル
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
        public bool 赤くなる
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
        public bool フェニックス
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
        /// sp吸收
        /// </summary>
        public bool スタミナテイク
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
        public bool マナの守護
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
        public bool チャンプモンスターキラー状態
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
        public bool 竜眼開放
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
        public bool 温泉効果
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
        public bool 武器属性無効化
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
        public bool 防御属性無効化
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
        public bool ロケットブースター点火
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
        public bool 斧头达人
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
        public bool 剑达人
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
        public bool 矛达人
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
        public bool 枪达人
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
        /// 站桩
        /// </summary>
        public bool 三转未知强力增强
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
        public bool 三转HP增强
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
        public bool 三转MP增强
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
        public bool 三转SP增强
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
        public bool 三转ATK增强
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
        public bool 三转MATK增强
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
        public bool 三转DEF增强
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
        public bool 三转MDEF增强
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
        public bool 三转HIT增强
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
        public bool 三转AVOID增强
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
        public bool 三转CSPD增强
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
        public bool 三转PAYL增强
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
        public bool 三转CAPA增强
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
        
        /*public bool 不知道18
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
