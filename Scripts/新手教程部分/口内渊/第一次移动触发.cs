
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaMap;
using SagaMap.Manager;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
using SagaMap.ActorEventHandlers;
namespace SagaScript.M30210000
{
    public partial class S50200000 : Event
    {
        public S50200000()
        {
            this.EventID = 50200000;
        }

        public override void OnEvent(ActorPC pc)
        {
            Map map = MapManager.Instance.GetMap(pc.MapID);

            Actor 夏影 = map.SpawnCustomMob(10000000, map.ID, 60000005, 0, 10010100, 0, 31, 176, 1, 1, 0, 夏影Info(), 夏影AI(), null, 0, false, false)[0];
            Actor 兔纸 = map.SpawnCustomMob(10000000, map.ID, 60000012, 0, 10010100, 0, 29, 177, 1, 1, 0, 星妈Info(), 星妈AI(), null, 0, false, false)[0];
            Actor 星妈 = map.SpawnCustomMob(10000000, map.ID, 60000015, 0, 10010100, 0, 29, 175, 1, 1, 0, 兔纸Info(), 兔纸AI(), null, 0, false, false)[0];
            pc.Slave.Add(夏影);
            pc.Slave.Add(兔纸);
            pc.Slave.Add(星妈);
            ShowPicture(pc, "interface/event/tutorial01.bmp", "新手指导！！");
            ((MobEventHandler)夏影.e).AI.Master = pc;
            ((MobEventHandler)兔纸.e).AI.Master = pc;
            ((MobEventHandler)星妈.e).AI.Master = pc;
            ShowDialog(pc, 20000);

            List<ActorMob> 蜘蛛 = map.SpawnCustomMob(10000000, map.ID, 10211000, 0, 0, 52, 142, 0, 10, 0,蜘蛛Info(),蜘蛛AI(),null,0);
            foreach (ActorMob item in 蜘蛛)
            {
                if (item == 蜘蛛[0])
                {
                    ((MobEventHandler)item.e).Defending += (s, e) =>
                    {
                    /*暂停3位NPC*/
                        if (e.TInt["新手教程1"] != 1)
                        {
                            foreach (var i in pc.Slave)
                            {
                                ((MobEventHandler)i.e).AI.Pause();
                                i.Buff.Stone = true;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, i, false);
                            }
                            e.TInt["新手教程1"] = 1;
                            ShowDialog(e, 20001);
                        }
                    };
                }
                ((MobEventHandler)item.e).Dying += (s, e) =>
                {
                    pc.TInt["新手教程2"] += 1;
                    if(pc.TInt["新手教程2"] >= 10)
                    {
                        ShowDialog(pc, 20002);
                    }
                };
            }
        }
    }
}