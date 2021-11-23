
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
    public partial class NuanliuSpawn : Event
    {
        public override void OnEvent(ActorPC pc)
        {
        }
        public NuanliuSpawn()
        {
            if (SagaMap.Manager.MapClientManager.Instance.OnlinePlayer.Count < 1)
            {
                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(21180001);

               
                ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 16925100, 0, 10010100, 1, 204, 228, 0, 1, 10000, 萝蕾拉Info(), 萝蕾拉AI(), null, 0)[0];
                ((MobEventHandler)mobS.e).Dying += (s, e) => SInt["萝蕾拉死亡次数"]++;
                mobS.TInt["playersize"] = 1500;

                map.SpawnCustomMob(10000000, map.ID, 14150200, 0, 0, 66, 165, 30, 3, 60, 寄居蟹Info(), 寄居蟹AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14150200, 0, 0, 66, 200, 30, 2, 60, 寄居蟹Info(), 寄居蟹AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14150200, 0, 0, 33, 204, 30, 2, 60, 寄居蟹Info(), 寄居蟹AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14140000, 0, 0, 102, 178, 30, 5, 25, 元素怪Info(), 元素怪AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14140000, 0, 0, 25, 136, 30, 5, 25, 元素怪Info(), 元素怪AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14160500, 0, 0, 153, 94, 30, 4, 25, 毒水母Info(), 毒水母AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14160500, 0, 0, 30, 20, 30, 4, 25, 毒水母Info(), 毒水母AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14160500, 0, 0, 99, 26, 30, 4, 25, 毒水母Info(), 毒水母AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14130400, 0, 0, 26, 60, 30, 5, 60, 海龙Info(), 海龙AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14130400, 0, 0, 114, 30, 30, 5, 60, 海龙Info(), 海龙AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14130400, 0, 0, 18, 95, 30, 5, 60, 海龙Info(), 海龙AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14130400, 0, 0, 203, 213, 30, 5, 60, 海龙Info(), 海龙AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14130400, 0, 0, 223, 192, 30, 5, 60, 海龙Info(), 海龙AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14130400, 0, 0, 212, 78, 30, 5, 60, 海龙Info(), 海龙AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 14130400, 0, 0, 192, 62, 75, 7, 60, 海龙Info(), 海龙AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10060200, 0, 0, 189, 68, 30, 4, 25, 深海飞鱼Info(), 深海飞鱼AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10060200, 0, 0, 207, 103, 30, 4, 25, 深海飞鱼Info(), 深海飞鱼AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10060200, 0, 0, 162, 40, 30, 4, 25, 深海飞鱼Info(), 深海飞鱼AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10060200, 0, 0, 148, 209, 30, 4, 25, 深海飞鱼Info(), 深海飞鱼AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10060200, 0, 0, 148, 204, 56, 4, 25, 深海飞鱼Info(), 深海飞鱼AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10060200, 0, 0, 226, 213, 56, 4, 25, 深海飞鱼Info(), 深海飞鱼AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10870900, 0, 0, 147, 193, 40, 4, 25, 黑色水晶海胆Info(), 黑色水晶海胆AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10870900, 0, 0, 151, 177, 40, 4, 25, 黑色水晶海胆Info(), 黑色水晶海胆AI(), null, 0);
                map.SpawnCustomMob(10000000, map.ID, 10870900, 0, 0, 195, 168, 40, 4, 25, 黑色水晶海胆Info(), 黑色水晶海胆AI(), null, 0);
                //map.SpawnCustomMob(10000000, map.ID, 15580100, 0, 0, 206, 236, 3, 1, 36000, 克苏鲁熔岩Info(), 克苏鲁熔岩AI(), null, 0);
            }
        }
    }
}

