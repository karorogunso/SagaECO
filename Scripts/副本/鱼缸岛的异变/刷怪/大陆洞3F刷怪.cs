
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
namespace SagaScript.M30210000
{
    public partial class 暗鸣 : Event
    {
        void 大陆B3F刷怪(uint mapid, ActorPC pc)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10060200, 0, 0, 93, 92, 18, 3, 0, 深海飞鱼Info(), 深海飞鱼AI(), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10061000, 0, 0, 49, 41, 35, 3, 0, 死亡飞鱼Info(), 死亡飞鱼AI(), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 10060600, 0, 0, 63, 65, 27, 4, 0, 天使飞鱼Info(), 天使飞鱼AI(), null, 0);
            if (Global.Random.Next(0, 100) < 30 && DateTime.Now.Year == 2017 &&((DateTime.Now.Month == 1 && DateTime.Now.Day >= 11) || (DateTime.Now.Month == 2 && DateTime.Now.Day <=3)))
            {
               ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 10370000, 0, 0, 68, 62, 0, 1, 0, 迎春宝箱Info(), 迎春宝箱AI(), null, 0)[0];
                ((MobEventHandler)mobS.e).Dying += (s, e) => SInt["副本新春宝箱死亡次数"]++;
            }
        }
    }
}

