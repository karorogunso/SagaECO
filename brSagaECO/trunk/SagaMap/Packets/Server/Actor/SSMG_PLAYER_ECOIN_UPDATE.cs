using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_ECOIN_UPDATE : Packet
    {
        //predicted packet
        public SSMG_PLAYER_ECOIN_UPDATE()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x09ED;
        }

        public uint ECoin
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutUInt(value, 2);
            }
        }
    }
}

