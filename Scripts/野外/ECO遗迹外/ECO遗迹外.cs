
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
    public partial class ECO遗迹外 : Event
    {
        public override void OnEvent(ActorPC pc)
        {
        }
        public ECO遗迹外()
        {
            if (SagaMap.Manager.MapClientManager.Instance.OnlinePlayer.Count < 1)
            {
                SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(11025000);
                map.SpawnCustomMob(10000000, map.ID, 10110000, 0, 0, 125, 125, 125, 45, 15, 圆滚兔1Info(), 圆滚兔1AI(), (MobCallback)Ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 10110000, 0, 0, 125, 125, 125, 25, 15, 圆滚兔2Info(), 圆滚兔2AI(), (MobCallback)Ondie, 1);
                map.SpawnCustomMob(10000000, map.ID, 10110000, 0, 0, 125, 125, 125, 25, 15, 圆滚兔3Info(), 圆滚兔3AI(), (MobCallback)Ondie, 1);

            }
        }

        void Ondie(MobEventHandler e, ActorPC pc)
        {
        }

    }
}

