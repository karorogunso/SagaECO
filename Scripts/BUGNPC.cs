
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.ActorEventHandlers;
namespace SagaScript.M30210000
{
    public class S50003017 : Event
    {
        public S50003017()
        {
            this.EventID = 50003017;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "GM控制台", "", "立刻复活并把复活次数增加到30次", "立刻复活并把复活次数增加到50次", "离开"))  
            {
                case 1:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.Online && pc.MapID == item.Character.MapID)
                        {
                            Say(item.Character, 0, "复活次数增加到了30次。");
                            Revive(item.Character, 5);
                            item.Character.TInt["副本复活标记"] = 3;
                            item.Character.TInt["复活次数"] = 30;
                            item.Character.TInt["设定复活次数"] = 30;
                        }
                    }
                            break;
                case 2:
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.Online && pc.MapID == item.Character.MapID)
                        {
                            Say(item.Character, 0, "复活次数增加到了50次。");
                            Revive(item.Character, 5);
item.Character.TInt["副本复活标记"] = 3;
                            item.Character.TInt["复活次数"] = 50;
                            item.Character.TInt["设定复活次数"] = 50;
                        }
                    }
                    break;
            }
            return;
            switch (Select(pc, "GM控制台", "", "刷新通天塔BOSS", "刷新暖流BOSS", "离开"))
            {
                case 1:
                    SagaMap.Map map2 = SagaMap.Manager.MapManager.Instance.GetMap(20170000);
                    map2.SpawnCustomMob(10000000, map2.ID, 14110002, 0, 0, 9, 32, 0, 1, 0, Exploration.塔内.泰达尼亚龙Info(), Exploration.塔内.泰达尼亚龙AI(), null, 0);
                    break;
                case 2:
                    SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(21180001);
                    ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 16925100, 0, 10010100, 1, 204, 228, 0, 1, 0, WeeklyExploration.NuanliuSpawn.萝蕾拉Info(), WeeklyExploration.NuanliuSpawn.萝蕾拉AI(), null, 0)[0];
                    ((MobEventHandler)mobS.e).Dying += (s, e) => SInt["萝蕾拉死亡次数"]++;
                    mobS.TInt["playersize"] = 1500;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mobS, false);
                    break;
            }
        }
    }
}

