using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SIZE : Packet
    {
        /// <summary>
        /// [00][0E][02][0F]
        /// [00][00][??][??] //キャラID
        /// [00][00][07][D0] //2000 1000で標準 500チビチビ 2000デカデカ
        /// [00][00][05][DC] //1500 固定？　値を変えても変化無し
        /// </summary>
        public SSMG_PLAYER_SIZE()
        {
            this.data = new byte[14];
            this.offset = 2;
            this.ID = 0x020F;
        }

        /// <summary>
        /// キャラID        
        /// </summary>
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        /// <summary>
        /// 2000 1000で標準 500チビチビ 2000デカデカ
        /// </summary>
        public uint Size
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        /// <summary>
        /// 1500 固定？　値を変えても変化無し 
        /// </summary>
        public uint unknwon
        {
            set
            {
                this.PutUInt(1500, 10);
            }
        }



    }
}

