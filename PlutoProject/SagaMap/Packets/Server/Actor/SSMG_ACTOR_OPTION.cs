using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_OPTION : Packet
    {
        [Flags]
        public enum Options
        {
            NONE                    = 0,
            NO_TRADE                = 1, // トレード不許可
            NO_PARTY                = 2, // パーティ邀請不許可
            NO_SPIRIT_POSSESSION    = 4, // 憑依邀請不許可
            NO_RING                 = 8, // リング邀請不許可
            NO_REVIVE_MESSAGE       = 16, // 蘇生選択しない
            NO_SKILL                = 32, // 作業請負不許可
            NO_BOND                 = 256, // 師徒邀請不許可
            NO_EQUPMENT             = 512, // 看裝備不許可
            NO_PATNER               = 1024, // Patner外觀   不許可
            NO_FRIEND               = 2048, // 朋友邀請不許可
            
        }
        public SSMG_ACTOR_OPTION()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1A5F;   
        }

       
        public Options Option
        {
            set
            {
                this.PutUInt((uint)value, 2);
            }
        }
        public int RawOption
        {
            set
            {
                this.PutUInt((uint)value, 2);
            }
        }
    }
}

