
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaMap;
using SagaMap.Manager;
using SagaScript.Chinese.Enums;
namespace WeeklyExploration
{
    public partial class GQuest : Event
    {
        private class Activator : MultiRunTask
        {
            uint mapid;
            int count = 0, maxcount = 3;
            SagaMap.Map map;
            ActorMob mob;
            ActorPC Leader;
            public Activator(ActorPC Leader, uint mapid)
            {
                this.Leader = Leader;
                this.mapid = mapid;
                DueTime = 10000;
                Period = 15000;
                map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            }
            public override void CallBack()
            {
                count++;
                if(Leader == null)
                    this.Deactivate();
                if (!Leader.Buff.Dead && Leader != null && Leader.MapID == Leader.Party.Leader.TInt["S11074000"])
                {
                    if (count % 2 == 0)
                        map.SpawnCustomMob(10000001, map.ID, 10019400, 0, 0, 29, 23, 0, 1, 0, 蓝色鱼人Info(), 蓝色鱼人AI(), null, 0);
                    else
                        map.SpawnCustomMob(10000001, map.ID, 10019404, 0, 0, 29, 23, 0, 1, 0, 黑色鱼人Info(), 黑色鱼人AI(), null, 0);
                }
                if (count == maxcount)
                    mob = map.SpawnCustomMob(10000000, map.ID, 70000003, 10059450, 10010100, 1, 29, 23, 1, 1, 0, 花音Info(), 花音AI(), null, 0)[0];
                if (count > maxcount)
                {
                    if ((mob.Buff.Dead || count > 6000))
                    {
                        if (Leader.Party != null)
                        {
                            foreach (var item in Leader.Party.Members.Values)
                            {
                                if (item.Online)
                                {
                                    if (item.Buff.Dead)
                                    {
                                        SagaMap.Network.Client.MapClient.FromActorPC(item).RevivePC(item);
                                    }
                                    item.TInt["副本复活标记"] = 1;
                                    item.Party.Leader.TInt["复活次数"] = 8;
                                    item.Party.Leader.TInt["设定复活次数"] = 8;
                                    Map newMap = MapManager.Instance.GetMap((uint)Leader.Party.Leader.TInt["S11075000"]);
                                    SagaMap.Network.Client.MapClient.FromActorPC(item).Map.SendActorToMap(item, newMap.ID, Global.PosX8to16(10, newMap.Width), Global.PosY8to16(10, newMap.Height));
                                }
                            }
                        }
                        this.Deactivate();
                    }
                }
            }
        }
    }
}

