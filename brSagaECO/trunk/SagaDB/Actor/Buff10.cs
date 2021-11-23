using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public partial class Buff
    {
        #region Buffs
        public bool アルスマグナ
        {
            get
            {
                return buffs[9].Test(0x00000001);
            }
            set
            {
                buffs[9].SetValue(0x00000001, value);
            }
        }
        public bool 精密攻撃
        {
            get
            {
                return buffs[9].Test(0x00000002);
            }
            set
            {
                buffs[9].SetValue(0x00000002, value);
            }
        }
        public bool 悪炎
        {
            get
            {
                return buffs[9].Test(0x00000004);
            }
            set
            {
                buffs[9].SetValue(0x00000004, value);
            }
        }
        public bool 九尾狐の魅了
        {
            get
            {
                return buffs[9].Test(0x00000008);
            }
            set
            {
                buffs[9].SetValue(0x00000008, value);
            }
        }
        public bool 武装化
        {
            get
            {
                return buffs[9].Test(0x00000010);
            }
            set
            {
                buffs[9].SetValue(0x00000010, value);
            }
        }
        public bool 武装化の反動
        {
            get
            {
                return buffs[9].Test(0x00000020);
            }
            set
            {
                buffs[9].SetValue(0x00000020, value);
            }
        }
        public bool モルガナハーツ
        {
            get
            {
                return buffs[9].Test(0x00000040);
            }
            set
            {
                buffs[9].SetValue(0x00000040, value);
            }
        }
        public bool 耐える
        {
            get
            {
                return buffs[9].Test(0x00000080);
            }
            set
            {
                buffs[9].SetValue(0x00000080, value);
            }
        }
        public bool 追い討ちの極意
        {
            get
            {
                return buffs[9].Test(0x00000100);
            }
            set
            {
                buffs[9].SetValue(0x00000100, value);
            }
        }
        public bool 友情の一撃経験値上昇
        {
            get
            {
                return buffs[9].Test(0x00000200);
            }
            set
            {
                buffs[9].SetValue(0x00000200, value);
            }
        }
        public bool 経験値上昇
        {
            get
            {
                return buffs[9].Test(0x00000400);
            }
            set
            {
                buffs[9].SetValue(0x00000400, value);
            }
        }
        public bool 被クリティカルダメージ軽減率上昇
        {
            get
            {
                return buffs[9].Test(0x00000800);
            }
            set
            {
                buffs[9].SetValue(0x00000800, value);
            }
        }
        public bool 怒り
        {
            get
            {
                return buffs[9].Test(0x00001000);
            }
            set
            {
                buffs[9].SetValue(0x00001000, value);
            }
        }
        public bool HPMPSP自動回復
        {
            get
            {
                return buffs[9].Test(0x00002000);
            }
            set
            {
                buffs[9].SetValue(0x00002000, value);
            }
        }
        public bool ユニークカウンター
        {
            get
            {
                return buffs[9].Test(0x00004000);
            }
            set
            {
                buffs[9].SetValue(0x00004000, value);
            }
        }
        public bool 叩き攻撃耐性弱体化
        {
            get
            {
                return buffs[9].Test(0x00008000);
            }
            set
            {
                buffs[9].SetValue(0x00008000, value);
            }
        }
        public bool 無名1
        {
            get
            {
                return buffs[9].Test(0x00010000);
            }
            set
            {
                buffs[9].SetValue(0x00010000, value);
            }
        }
        public bool 無名2
        {
            get
            {
                return buffs[9].Test(0x00020000);
            }
            set
            {
                buffs[9].SetValue(0x00020000, value);
            }
        }
        public bool 無名3
        {
            get
            {
                return buffs[9].Test(0x00040000);
            }
            set
            {
                buffs[9].SetValue(0x00040000, value);
            }
        }
        public bool 無名4
        {
            get
            {
                return buffs[9].Test(0x00080000);
            }
            set
            {
                buffs[9].SetValue(0x00080000, value);
            }
        }
        public bool 無名5
        {
            get
            {
                return buffs[9].Test(0x00100000);
            }
            set
            {
                buffs[9].SetValue(0x00100000, value);
            }
        }
        public bool マガマガシイオーラ
        {
            get
            {
                return buffs[9].Test(0x00200000);
            }
            set
            {
                buffs[9].SetValue(0x00200000, value);
            }
        }
        public bool 無名6
        {
            get
            {
                return buffs[9].Test(0x00400000);
            }
            set
            {
                buffs[9].SetValue(0x00400000, value);
            }
        }
        public bool 被火属性ダメージ軽減率上昇
        {
            get
            {
                return buffs[9].Test(0x00800000);
            }
            set
            {
                buffs[9].SetValue(0x00800000, value);
            }
        }
        public bool 被水属性ダメージ軽減率上昇
        {
            get
            {
                return buffs[9].Test(0x01000000);
            }
            set
            {
                buffs[9].SetValue(0x01000000, value);
            }
        }
        public bool 被風属性ダメージ軽減率上昇
        {
            get
            {
                return buffs[9].Test(0x02000000);
            }
            set
            {
                buffs[9].SetValue(0x02000000, value);
            }
        }
        public bool 被土属性ダメージ軽減率上昇
        {
            get
            {
                return buffs[9].Test(0x04000000);
            }
            set
            {
                buffs[9].SetValue(0x04000000, value);
            }
        }
        public bool 被光属性ダメージ軽減率上昇
        {
            get
            {
                return buffs[9].Test(0x08000000);
            }
            set
            {
                buffs[9].SetValue(0x08000000, value);
            }
        }
        public bool 被暗属性ダメージ軽減率上昇
        {
            get
            {
                return buffs[9].Test(0x10000000);
            }
            set
            {
                buffs[9].SetValue(0x10000000, value);
            }
        }
        public bool 攻撃ダメージ上昇
        {
            get
            {
                return buffs[9].Test(0x20000000);
            }
            set
            {
                buffs[9].SetValue(0x20000000, value);
            }
        }
        public bool 被魔法ダメージ軽減率上昇
        {
            get
            {
                return buffs[9].Test(0x40000000);
            }
            set
            {
                buffs[9].SetValue(0x40000000, value);
            }
        }
        public bool ダメージミラー
        {
            get
            {
                return buffs[9].Test(0x80000000);
            }
            set
            {
                buffs[9].SetValue(0x80000000, value);
            }
        }
        #endregion
    }
}
