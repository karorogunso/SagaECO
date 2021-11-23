
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
    public partial class S70001001 : Event
    {
        public S70001001()
        {
            this.EventID = 70001001;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID != pc.TInt["AAA序章剧情图"]) return;
            Say(pc, 0, "本剧情为体验形式！$R$R对于高强玩家，$R如果想看完整剧情，$R请在剧情走完后再杀掉小怪，$R以免剧情中途跳过而错过剧情！$R$R本剧情带语音，若听不见语音，请检查配音是否被禁音。", "阅读提示");
            ShowDialog(pc, 10003);
            Map map = MapManager.Instance.GetMap(pc.MapID);
            ActorMob mob = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图"], 16580002, 10006100, 0, 104, 21, 1, 1, 0, AAA序章怪物.有攻击力的伊利斯Info(), AAA序章怪物.有攻击力的伊利斯AI(), null, 0)[0];
            ((MobEventHandler)mob.e).AI.Master = pc;
            Timer timer = new Timer("AAA序章步骤1", 0, 20000);
            timer.OnTimerCall += (s, e) =>
            {
                s.Deactivate();
                if (pc == null) return;
                ShowDialog(pc, 10004);
                ActorMob BOSS1 = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图"], 10540503, 10010100, 1, 62, 90, 1, 1, 0, AAA序章怪物.舞龙鱼人Info(), AAA序章怪物.舞龙鱼人AI(), (x, y) =>
                {
                    foreach (var item in x.mob.Slave)
                    {
                        MobEventHandler eh = (MobEventHandler)item.e;
                        item.Buff.死んだふり = true;
                        eh.OnDie(true);
                        SkillHandler.Instance.ActorSpeak(item, "闹！！忙丝塔！！！");
                    }
                    ShowDialog(pc, 10005);
                    SetNextMoveEvent(pc, 70001002);
                }, 1)[0];
                if (!((MobEventHandler)BOSS1.e).AI.Hate.ContainsKey(pc.ActorID))
                    ((MobEventHandler)BOSS1.e).AI.Hate.Add(pc.ActorID, 500);
                SkillHandler.Instance.ActorSpeak(BOSS1, "略略略略略！");
                List<ActorMob> mobs = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图"], 15180000, 10010100, 1, 62, 90, 3, 10, 0, AAA序章怪物.奇怪的鱼人Info(), AAA序章怪物.奇怪的鱼人AI(), null, 0);
                for (int i = 0; i < mobs.Count; i++)
                {
                    MobEventHandler eh = (MobEventHandler)mobs[i].e;
                    eh.AI.Master = BOSS1;
                    BOSS1.Slave.Add(mobs[i]);
                    SkillHandler.Instance.ActorSpeak(mobs[i], "囖囖囖囖囖！！");
                    if (eh.AI.Hate.ContainsKey(pc.ActorID))
                        eh.AI.Hate.Add(pc.ActorID, 500);
                }
            };
            timer.Activate();
        }
    }
}

