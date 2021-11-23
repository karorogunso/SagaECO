using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTY_MEMBER_BUFF : Packet
    {
        public SSMG_PARTY_MEMBER_BUFF()
        {
            if (Configuration.Instance.Version < SagaLib.Version.Saga11)
                this.data = new byte[42];
            else
                this.data = new byte[46];
            this.offset = 2;
            this.ID = 0x19FA;
        }

        public ActorPC Actor
        {
            set
            {
                this.PutUInt(value.Party.IndexOf(value), 2);
                this.PutUInt(value.CharID, 6);
                this.PutInt(value.Buff.Buffs[0].Value, 10);
                this.PutInt(value.Buff.Buffs[1].Value, 14);
                this.PutInt(value.Buff.Buffs[2].Value, 18);
                this.PutInt(value.Buff.Buffs[3].Value, 22);
                this.PutInt(value.Buff.Buffs[4].Value, 26);
                this.PutInt(value.Buff.Buffs[5].Value, 30);
                this.PutInt(value.Buff.Buffs[6].Value, 34);
            }
        }


    }
}

