using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_OPTION : Packet
    {
        public enum Options
        {
            NONE = 0,
            NO_TRADE = 0x00000001, // トレード不許可
            NO_PARTY = 0x00000002, // パーティ不許可
            NO_SPIRIT_POSSESSION, // 憑依不許可
            NO_RING, //0x00000008 リング不許可
            //0x00000010 蘇生選択しない
            //0x00000020 作業請負不許可
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
    }
}

