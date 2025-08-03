using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;
using SagaDB.Actor;
using SagaDB.Map;

namespace SagaMap.Scripting
{
    public abstract class Portal:Event
    {
        public uint mapID;
        public byte x;
        public byte y;

        public void Init(uint eventid,uint mapid, byte x, byte y)
        {
            this.EventID = eventid;
            this.mapID = mapid;
            this.x = x;
            this.y = y;
        }

        public override void OnEvent(ActorPC pc)
        {
            Warp(pc, mapID, x, y);
        }
    }
}
