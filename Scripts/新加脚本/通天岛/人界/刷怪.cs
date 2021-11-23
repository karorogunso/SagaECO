
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaDB.Actor;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace Exploration
{
    public partial class 通天岛 : Event
    {
        public override void OnEvent(ActorPC pc)
        {
        }
        public 通天岛()
        {
            if (SagaMap.Manager.MapClientManager.Instance.OnlinePlayer.Count < 1)
            {
                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(10058000);
                map.SpawnCustomMob(10000000, map.ID, 30000000, 0, 0, 127, 208, 50, 10, 50, 岩石Info(), 岩石AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 30010000, 0, 0, 127, 208, 50, 2, 120, 矿石Info(), 矿石AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 30540000, 0, 0, 127, 208, 50, 3, 50, 豌豆Info(), 豌豆AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 30740000, 0, 0, 154, 212, 1, 1, 500, 西瓜Info(), 西瓜AI(), null, 0);
            }
        }
    }
}

