using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_BUFF : Packet
    {
        public SSMG_ACTOR_BUFF()
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga17)
                this.data = new byte[54];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2)
                this.data = new byte[46];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                this.data = new byte[42];
            else
                this.data = new byte[38];
            this.offset = 2;
            this.ID = 0x157C;
        }

        public Actor Actor
        {
            set
            {
                this.PutUInt(value.ActorID, 2);
                this.PutInt(value.Buff.Buffs[0].Value, 6);
                this.PutInt(value.Buff.Buffs[1].Value, 10);
                this.PutInt(value.Buff.Buffs[2].Value, 14);
                this.PutInt(value.Buff.Buffs[3].Value, 18);
                this.PutInt(value.Buff.Buffs[4].Value, 22);
                this.PutInt(value.Buff.Buffs[5].Value, 26);
                this.PutInt(value.Buff.Buffs[6].Value, 30);
                this.PutInt(value.Buff.Buffs[7].Value, 34);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    this.PutInt(value.Buff.Buffs[8].Value, 38);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2)
                    this.PutInt(value.Buff.Buffs[9].Value, 42);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2)
                    this.PutInt(value.Buff.Buffs[10].Value, 46);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga14_2)
                    this.PutInt(value.Buff.Buffs[11].Value, 50);
            }
        }


    }
}

