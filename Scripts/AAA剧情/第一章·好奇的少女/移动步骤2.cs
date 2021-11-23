
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
using SagaMap.ActorEventHandlers;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap.Skill;
namespace SagaScript.M30210000
{
    public partial class S70001002 : Event
    {
        public S70001002()
        {
            this.EventID = 70001002;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID != pc.TInt["AAA序章剧情图"]) return;
            Timer timer = new Timer("AAA序章步骤2", 0, 10000);
            Map map = MapManager.Instance.GetMap(pc.MapID);
            timer.OnTimerCall += (s, e) =>
            {
                s.Deactivate();
                if (pc == null) return;
                ShowDialog(pc, 10006);
                ActorMob BOSS2 = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图"], 10870700, 10010100, 1, 78, 150, 1, 1, 0, AAA序章怪物.黄色石头Info(), AAA序章怪物.黄色石头AI(), (x, y) =>
                {
                    foreach (var item in x.mob.Slave)
                    {
                        MobEventHandler eh = (MobEventHandler)item.e;
                        item.Buff.死んだふり = true;
                        eh.OnDie(true);
                    }
                    ShowDialog(pc, 10007);
                    Timer timer2 = new Timer("AAA序章步骤3", 0, 40000);
                    timer2.OnTimerCall += (s2, e2) =>
                    {
                        s2.Deactivate();
                        if (pc == null) return;
                        if (pc.MapID != pc.TInt["AAA序章剧情图"]) return;
                        pc.TInt["AAA序章剧情图2"] = CreateMapInstance(60904000, 10054000, 144, 152, true, 0, true);
                        Warp(pc, (uint)pc.TInt["AAA序章剧情图2"], 90, 157);
                        SetNextMoveEvent(pc, 70001003);
                    };
                    timer2.Activate();

                }, 1)[0];
                if (!((MobEventHandler)BOSS2.e).AI.Hate.ContainsKey(pc.ActorID))
                    ((MobEventHandler)BOSS2.e).AI.Hate.Add(pc.ActorID, 500);
                SkillHandler.Instance.ActorSpeak(BOSS2, "嗷....");
                List<ActorMob> mobs = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图"], 10870300, 10010100, 1, 78, 150, 3, 10, 0, AAA序章怪物.蓝色石头Info(), AAA序章怪物.蓝色石头AI(), null, 0);
                for (int i = 0; i < mobs.Count; i++)
                {
                    MobEventHandler eh = (MobEventHandler)mobs[i].e;
                    eh.AI.Master = BOSS2;
                    BOSS2.Slave.Add(mobs[i]);
                    if (eh.AI.Hate.ContainsKey(pc.ActorID))
                        eh.AI.Hate.Add(pc.ActorID, 500);
                }
            };
            timer.Activate();
        }
    }
}

