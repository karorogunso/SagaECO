using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.ODWar;

using SagaMap.Manager;
using SagaMap.Skill;
using SagaMap.Network.Client;
using SagaMap.Mob;

namespace SagaMap.Tasks.System
{
    public class NPC逛店 : MultiRunTask
    {
        Map map;
        public NPC逛店()
        {
            map = MapManager.Instance.GetMap(10054000);
            period = 30000;
            dueTime = 0;
        }

        static NPC逛店 instance;

        public static NPC逛店 Instance
        {
            get
            {
                if (instance == null)
                    instance = new NPC逛店();
                return instance;
            }
        }
        public override void CallBack()
        {
            period = Global.Random.Next(30000, 60000);
            int rate = Global.Random.Next(0, 100);
            if(rate < 30)
            {
                List<ActorPC> pcs = GetPlayersWhoOpeningShop(map);
                if(pcs.Count > 0)
                {
                    ActorPC Target = pcs[Global.Random.Next(0, pcs.Count - 1)];
                    MapClient client = MapClient.FromActorPC(Target);
                    //TODO:已经获取玩家了，接下来要根据NPCBuyFactory.NpcBuyList来让NPC几率性的购买玩家商店的物品
                    //TODO:购买必须另建线程，以营造NPC逛店的效果。超过阔值价格NPC绝对不会购买，其他价格根据与最低价格的差，来随机性购买。
                }
            }
        }
        List<ActorPC> GetPlayersWhoOpeningShop(Map map)
        {
            List<ActorPC> pcs = new List<ActorPC>();
            foreach (var item in map.Actors.Values)
            {
                if(item.type == ActorType.PC)
                {
                    ActorPC pc = item as ActorPC;
                    MapClient client = MapClient.FromActorPC(pc);
                    if(pc.Online)
                    {
                        if(pc.Playershoplist.Count > 0 && client.Shopswitch == 1)
                        {
                            pcs.Add(pc);
                        }
                    }
                }
            }
            return pcs;
        }
    }
}
