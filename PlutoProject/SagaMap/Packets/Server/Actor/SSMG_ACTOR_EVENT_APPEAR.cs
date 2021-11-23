using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Map;


namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_EVENT_APPEAR : Packet
    {
        public SSMG_ACTOR_EVENT_APPEAR()
        {
            this.data = new byte[19];
            this.offset = 2;
            this.ID = 0x0BB8;
          
        }

        public ActorEvent Actor
        {
            set
            {
                byte[] objName = null;
                byte[] title = Global.Unicode.GetBytes(value.Title + "\0");
                MapInfo info = MapInfoFactory.Instance.MapInfo[value.MapID];
                
                switch (value.Type)
                {
                    case ActorEventTypes.ROPE:
                        objName = Global.Unicode.GetBytes("fg_rope_01\0");
                        break;
                    case ActorEventTypes.TENT:
                        objName = Global.Unicode.GetBytes("33_tent01\0");
                        break;
                }
                byte[] buf = new byte[19 + objName.Length + title.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutUInt(value.ActorID, 2);
                this.PutByte((byte)objName.Length);
                this.PutBytes(objName);
                this.PutByte(Global.PosX16to8(value.X, info.width));
                this.PutByte(Global.PosY16to8(value.Y, info.height));
                switch (value.Type)
                {
                    case ActorEventTypes.ROPE:
                        this.PutByte(6);
                        break;
                    case ActorEventTypes.TENT :
                        this.PutByte(4);
                        break;
                }
                this.PutUInt(value.EventID);
                this.PutByte((byte)title.Length);
                this.PutBytes(title);
                this.PutUInt(value.Caster.CharID);
            }
        }
    }
}

