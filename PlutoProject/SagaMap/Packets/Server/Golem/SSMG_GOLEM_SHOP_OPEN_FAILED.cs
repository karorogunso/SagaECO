using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_GOLEM_SHOP_OPEN_FAILED : Packet
    {
        public SSMG_GOLEM_SHOP_OPEN_FAILED()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x17FD;
        }

        /// <summary>
        /// GAME_SMSG_GOLEM_ACCESSERR1,";何らかの原因でゴーレムにアクセスできません";
        /// GAME_SMSG_GOLEM_ACCESSERR2,";徘徊しているゴーレムです";
        /// GAME_SMSG_GOLEM_ACCESSERR3,";商品がないのでゴーレムショップは開けません";
        /// GAME_SMSG_GOLEM_ACCESSERR4,";イベント中はゴーレムショップは開けません";
        /// GAME_SMSG_GOLEM_ACCESSERR5,";トレード中はゴーレムショップは開けません";
        /// GAME_SMSG_GOLEM_ACCESSERR6,";憑依中はゴーレムショップは開けません";
        /// GAME_SMSG_GOLEM_ACCESSERR7,";行動不能時はゴーレムショップは開けません";
        /// GAME_SMSG_GOLEM_ACCESSERR8,";ゴーレムショップを開ける状態ではありません";
        /// GAME_SMSG_GOLEM_ACCESSERR9,";他の種類のゴーレムにアクセス中です";
        /// </summary>
        public int Result
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value,3);
            }
        }
    }
}

