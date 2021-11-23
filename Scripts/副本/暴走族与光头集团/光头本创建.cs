
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.ActorEventHandlers;
namespace WeeklyExploration
{
    public partial class GQuest : Event
    {
        void 光头本创建(ActorPC pc)
        {
            if (pc.Party == null)
                return;
            if (pc != pc.Party.Leader)
                return;
            
            foreach (var item in pc.Party.Members.Values)
            {
                if (item != null)
                {
                    item.AStr["暴走族与光头集团限制"] = DateTime.Now.ToString("yyyy-MM-dd");
                    item.CStr["暴走族与光头集团队长名"] = item.Party.Leader.Name;
                }
            }
            pc.Party.Leader.TInt["S11074000"] = CreateMapInstance(11074000, 91000999, 21, 21, true, 0, true);//光塔周边
            pc.Party.Leader.TInt["S11075000"] = CreateMapInstance(11075000, 91000999, 21, 21, true, 0, true);//主场馆1F

            foreach (var item in pc.Party.Members.Values)
            {
                if (item != null)
                {
                    item.TInt["副本复活标记"] = 1;
                    item.Party.Leader.TInt["复活次数"] = 8;
                    item.Party.Leader.TInt["设定复活次数"] = 8;
                    Warp(item, (uint)pc.Party.Leader.TInt["S11074000"], 20, 24);
                }
            }
            Activator timer = new Activator(pc.Party.Leader, (uint)pc.Party.Leader.TInt["S11074000"]);
            timer.Activate();//刷花音

            //刷疾风
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap((uint)pc.Party.Leader.TInt["S11075000"]);
            ActorMob hayate = map.SpawnCustomMob(10000000, map.ID, 70000005, 10059352, 10010100, 1, 52, 47, 1, 1, 0, 疾风Info(), 疾风AI(), null, 0)[0];
            ActorMob wuke = null;
            ((MobEventHandler)hayate.e).Defending += (s, e) =>
            {
                if(hayate.HP < hayate.MaxHP * 0.5 && wuke == null && hayate.AttackedForEvent != 1)
                {
                    hayate.AttackedForEvent = 1;
                    hayate.RideID = 0;
                    hayate.HP = hayate.MaxHP;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, hayate, false);
                    wuke = map.SpawnCustomMob(10000000, map.ID, 70000004, 0, 10010100, 1, 52, 47, 1, 1, 0, 吴克Info(), 吴克AI(), null, 0)[0];
                    wuke.TInt["光头疾风ID"] = (int)hayate.ActorID;
                    ((MobEventHandler)wuke.e).AI.Master = hayate;
                    hayate.Slave.Add(wuke);
                    ((MobEventHandler)wuke.e).Defending += (w, k) =>
                    {
                        if (wuke.HP < wuke.MaxHP * 0.05 && hayate != null)
                        {
                            if (hayate.HP > hayate.MaxHP * 0.12)
                            {
                                hayate.HP = hayate.MaxHP;
                                wuke.HP = wuke.MaxHP;
                            }
                        }
                    };
                }
                if(hayate.HP < hayate.MaxHP *0.05 && wuke != null)
                {
                    if(wuke.HP > wuke.MaxHP * 0.12)
                    {
                        hayate.HP = hayate.MaxHP;
                        wuke.HP = wuke.MaxHP;
                    }
                }
            };
        }
    }
}

