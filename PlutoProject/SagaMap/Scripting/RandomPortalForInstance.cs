using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;

namespace SagaMap.Scripting
{
    public abstract class RandomPortalForInstance : Event
    {
        public uint mapID;
        public byte x1, y1;
        public byte x2, y2;

        public void Init(uint eventID, uint mapID, byte x1, byte y1, byte x2, byte y2)
        {
            this.EventID = eventID;
            this.mapID = mapID;
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        public override void OnEvent(ActorPC pc)
        {
            uint id = 0;
            
            if(pc.Party == null)
            {
                Say(pc, 131, "你不能通过这个传送门。");
                return;
            }
            if(pc.Party != null)
            id = (uint)pc.Party.Leader.TInt["S" + mapID.ToString()];
            else
                id = (uint)pc.TInt["S" + mapID.ToString()];
            if (id == 0)
            {
                Say(pc, 131, "你不能通过这个传送门。。" + "S" + mapID.ToString() + " "+id.ToString());
                return;
            }
            byte x, y;

            x = (byte)Global.Random.Next(x1, x2);
            y = (byte)Global.Random.Next(y1, y2);

            Warp(pc, id, x, y);
        }
    }
}