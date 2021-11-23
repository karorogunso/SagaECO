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
        public bool  ゾンビ
        {
            get
            {
                return buffs[5].Test(0x00000001);
            }
            set
            {
                buffs[5].SetValue(0x00000001, value);
            }
        }
        public bool リボーン
        {
            get
            {
                return buffs[5].Test(0x00000002);
            }
            set
            {
                buffs[5].SetValue(0x00000002, value);
            }
        }
        public bool 演奏中
        {
            get
            {
                return buffs[5].Test(0x00000004);
            }
            set
            {
                buffs[5].SetValue(0x00000004, value);
            }
        }
        public bool 羽交い絞め
        {
            get
            {
                return buffs[5].Test(0x00000008);
            }
            set
            {
                buffs[5].SetValue(0x00000008, value);
            }
        }
        public bool 黑暗压制
        {
            get
            {
                return buffs[5].Test(0x00000010);
            }
            set
            {
                buffs[5].SetValue(0x00000010, value);
            }
        }
        public bool オーバーレンジ
        {
            get
            {
                return buffs[5].Test(0x00000020);
            }
            set
            {
                buffs[5].SetValue(0x00000020, value);
            }
        }
        public bool ライフテイク
        {
            get
            {
                return buffs[5].Test(0x00000040);
            }
            set
            {
                buffs[5].SetValue(0x00000040, value);
            }
        }
        /// <summary>
        /// 颤栗恐惧
        /// </summary>
        public bool Chilling
        {
            get
            {
                return buffs[5].Test(0x00000080);
            }
            set
            {
                buffs[5].SetValue(0x00000080, value);
            }
        }
        public bool 経験値上昇
        {
            get
            {
                return buffs[5].Test(0x00000100);
            }
            set
            {
                buffs[5].SetValue(0x00000100, value);
            }
        }
        public bool パッシング
        {
            get
            {
                return buffs[5].Test(0x00000200);
            }
            set
            {
                buffs[5].SetValue(0x00000200, value);
            }
        }
        public bool 回復不可能
        {
            get
            {
                return buffs[5].Test(0x00000400);
            }
            set
            {
                buffs[5].SetValue(0x00000400, value);
            }
        }
        public bool エンチャントブロック
        {
            get
            {
                return buffs[5].Test(0x00000800);
            }
            set
            {
                buffs[5].SetValue(0x00000800, value);
            }
        }
        public bool ソリッドボディ
        {
            get
            {
                return buffs[5].Test(0x00001000);
            }
            set
            {
                buffs[5].SetValue(0x00001000, value);
            }
        }
        public bool ブラッディウエポン
        {
            get
            {
                return buffs[5].Test(0x00002000);
            }
            set
            {
                buffs[5].SetValue(0x00002000, value);
            }
        }
        /// <summary>
        /// 灼烧
        /// </summary>
        public bool Burning
        {
            get
            {
                return buffs[5].Test(0x00004000);
            }
            set
            {
                buffs[5].SetValue(0x00004000, value);
            }
        }
        /// <summary>
        /// 枪dc
        /// </summary>
        public bool ガンディレイキャンセル
        {
            get
            {
                return buffs[5].Test(0x00008000);
            }
            set
            {
                buffs[5].SetValue(0x00008000, value);
            }
        }
        public bool ダブルアップ
        {
            get
            {
                return buffs[5].Test(0x00010000);
            }
            set
            {
                buffs[5].SetValue(0x00010000, value);
            }
        }
        public bool ATフィールド
        {
            get
            {
                return buffs[5].Test(0x00020000);
            }
            set
            {
                buffs[5].SetValue(0x00020000, value);
            }
        }
        public bool 根性
        {
            get
            {
                return buffs[5].Test(0x00040000);
            }
            set
            {
                buffs[5].SetValue(0x00040000, value);
            }
        }
        public bool 物理攻撃付加
        {
            get
            {
                return buffs[5].Test(0x00080000);
            }
            set
            {
                buffs[5].SetValue(0x00080000, value);
            }
        }
        public bool 死んだふり
        {
            get
            {
                return buffs[5].Test(0x00100000);
            }
            set
            {
                buffs[5].SetValue(0x00100000, value);
            }
        }
        public bool パパ点火
        {
            get
            {
                return buffs[5].Test(0x00200000);
            }
            set
            {
                buffs[5].SetValue(0x00200000, value);
            }
        }
        public bool 紫になる
        {
            get
            {
                return buffs[5].Test(0x00400000);
            }
            set
            {
                buffs[5].SetValue(0x00400000, value);
            }
        }
        public bool 精密射撃
        {
            get
            {
                return buffs[5].Test(0x00800000);
            }
            set
            {
                buffs[5].SetValue(0x00800000, value);
            }
        }
        public bool オーバーチューン
        {
            get
            {
                return buffs[5].Test(0x01000000);
            }
            set
            {
                buffs[5].SetValue(0x01000000, value);
            }
        }
        public bool 警戒
        {
            get
            {
                return buffs[5].Test(0x02000000);
            }
            set
            {
                buffs[5].SetValue(0x02000000, value);
            }
        }
        /// <summary>
        /// 魔法反射
        /// </summary>
        public bool リフレクション
        {
            get
            {
                return buffs[5].Test(0x04000000);
            }
            set
            {
                buffs[5].SetValue(0x04000000, value);
            }
        }
        public bool エンチャントウエポン
        {
            get
            {
                return buffs[5].Test(0x08000000);
            }
            set
            {
                buffs[5].SetValue(0x08000000, value);
            }
        }
        /// <summary>
        /// 清唱
        /// </summary>
        public bool オラトリオ
        {
            get
            {
                return buffs[5].Test(0x10000000);
            }
            set
            {
                buffs[5].SetValue(0x10000000, value);
            }
        }
        /// <summary>
        /// 暗极大
        /// </summary>
        public bool イビルソウル
        {
            get
            {
                return buffs[5].Test(0x20000000);
            }
            set
            {
                buffs[5].SetValue(0x20000000, value);
            }
        }
        /// <summary>
        /// 火心
        /// </summary>
        public bool フレイムハート
        {
            get
            {
                return buffs[5].Test(0x40000000);
            }
            set
            {
                buffs[5].SetValue(0x40000000, value);
            }
        }
        /*public bool アトラクトマーチ
        {
            get
            {
                return buffs[5].Test(0x80000000);
            }
            set
            {
                buffs[5].SetValue(0x80000000, value);
            }
        }*/
        #endregion

    }
}
