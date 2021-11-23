using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;
using SagaMap.Manager;
using SagaDB.Actor;
using SagaDB.Map;
using SagaDB.Item;
using SagaDB.Skill;
using SagaDB.Quests;

using SagaMap.Packets.Server;

namespace SagaMap.Scripting
{
    public abstract partial class Event
    {

        protected void OpenPprotectListOpen(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);

            SSMG_PPROTECT_INITI p = new SSMG_PPROTECT_INITI();

            client.netIO.SendPacket(p);
        }
    }
}
