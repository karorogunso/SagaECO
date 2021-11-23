
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
namespace WeeklyExploration
{
    public partial class 万圣节 : Event
    {
        public override void OnEvent(ActorPC pc)
        {
        }
        public 万圣节()
        {
            if (SagaMap.Manager.MapClientManager.Instance.OnlinePlayer.Count < 1)
            {
                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(10069001);
                map.SpawnCustomMob(10000000, map.ID, 10221500, 0, 0, 125, 125, 125, 45, 15, 鬼火Info(), 鬼火AI(), (MobCallback)Ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 10240600, 0, 10010100, 1, 125, 125, 125, 1, 2400, 小恶魔Info(), 小恶魔AI(), Ondie, 1);
                //map.SpawnCustomMob(10000000, map.ID, 10240600, 0, 0, 125, 125, 125, 1, 2400, 小恶魔Info(), 小恶魔AI(), (MobCallback)Ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 10250400, 0, 0, 125, 125, 125, 3, 15, 梦魇Info(), 梦魇AI(), (MobCallback)Ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 10271000, 0, 0, 125, 125, 125, 45, 15, 黑暗之羽Info(), 黑暗之羽AI(), (MobCallback)Ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 10415000, 0, 0, 125, 125, 125, 35, 15, 不死之王Info(), 不死之王AI(), (MobCallback)Ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 10690700, 0, 0, 125, 125, 125, 25, 15, 小僵尸Info(), 小僵尸AI(), (MobCallback)Ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 10700000, 0, 0, 125, 125, 125, 25, 15, 海盗AInfo(), 海盗A(), (MobCallback)Ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 10710000, 0, 10010100, 1, 125, 125, 125, 2, 700, 海盗BInfo(), 海盗B(), Ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 10675000, 0, 10010100, 1, 125, 125, 125, 1, 2400, 稻草人Info(), 稻草人AI(), Ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 10180500, 0, 10010100, 1, 125, 125, 125, 1, 2400, 丧尸Info(), 丧尸AI(), Ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 16425100, 0, 10010100, 1, 125, 125, 125, 1, 7200, 柠妹Info(), 柠妹AI(), Ondie, 1);
                //map.SpawnCustomMob(10000000, map.ID, 10060006, 0, 0, 125, 125, 125, 50, 10, 鱼Info(), 鱼AI(), (MobCallback)Ondie, 1);

            }
        }

        void Ondie(MobEventHandler e, ActorPC pc)
        {
        }

    }
}

