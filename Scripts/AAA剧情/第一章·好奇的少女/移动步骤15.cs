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
using System.Collections.Generic;

namespace SagaScript.M30210000
{
    public class S70001015 : Event
    {
        public S70001015()
        {
            this.EventID = 70001015;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID != pc.TInt["AAA序章剧情图8"]) return;
            Map map = MapManager.Instance.GetMap(pc.MapID);
            ActorMob mob = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图8"], 16580002, 10006100, 0, 80, 9, 1, 1, 0, AAA序章怪物.没攻击力的伊利斯Info(), AAA序章怪物.没攻击力的伊利斯AI(), null, 0)[0];
            ((MobEventHandler)mob.e).AI.Master = pc;
            ActorMob mob2 = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图8"], 16585100, 10006100, 0, 80, 10, 1, 1, 0, AAA序章怪物.伊利斯2AnotherInfo(), AAA序章怪物.伊利斯2AnotherAI(), null, 0)[0];
            ((MobEventHandler)mob2.e).AI.Master = pc;
            ShowDialog(pc, 10037);
            ActorMob mob3 = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图6"], 26090100, 10006100, 1, 102, 52, 1, 1, 0, AAA序章怪物.异常者XXInfo(), AAA序章怪物.异常者XXAI(), null, 0)[0];
            ShowDialog(pc, 10038);
            ShowDialog(pc, 10039);
            Timer timer = new Timer("AAA序章步骤10", 0, 105000);
            timer.OnTimerCall += (s, e) =>
            {
                timer.Deactivate();
                if (pc == null) return;
                if (pc.MapID != pc.TInt["AAA序章剧情图8"]) return;

                ShowDialog(pc, 10040);
                SkillHandler.Instance.CauseDamage(mob2, mob3, 233333333, true);
                SkillHandler.Instance.ShowVessel(mob3, 233333333, 0, 0, SkillHandler.AttackResult.Critical);
                SkillHandler.Instance.ShowEffectOnActor(mob2, 5156);
                SkillHandler.Instance.ShowEffectOnActor(mob3, 5004);
               
                Timer timer2 = new Timer("AAA序章步骤11", 0, 60000);
                timer2.OnTimerCall += (x, y) =>
                {
                    timer2.Deactivate();
                    if (pc == null) return;
                    if (pc.MapID != pc.TInt["AAA序章剧情图8"]) return;
                    ShowDialog(pc, 10041);
                    Warp(pc, (uint)pc.TInt["AAA序章剧情图8"], 0, 0);
                    ShowDialog(pc, 10042);
                    ShowDialog(pc, 10043);
                    ShowDialog(pc, 10044);
                    ShowDialog(pc, 10045);
                    if (pc.AInt["序章·好奇的少女"] != 1 && pc.AInt["序章·好奇的少女技能点获取"] != 1)
                    {
                        Say(pc, 131, "你获得了一个技能点。");
                        pc.AInt["序章·好奇的少女技能点获取"] = 1;
                        pc.SkillPoint3 += 1;
                        TitleProccess(pc, 58, 1);
                        TitleProccess(pc, 59, 1);
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();//发送玩家信息封包
                        ShowEffect(pc, 4131);//显示特效，ID4131
                    }
                    pc.AInt["序章·好奇的少女"] = 1;
                    Say(pc, 131, "请等待对话结束，$R稍后将会传送回主城。$R现在也可以使用/home强制结束对话回主城。$R$R感谢您的体验，$R请不要过多的在意表现形式的还原度，$R肯定没有日服好。");
                    Timer timer3 = new Timer("AAA序章步骤12", 0, 95000);
                    timer3.OnTimerCall += (x2, y2) =>
                    {
                        timer3.Deactivate();
                        if (pc == null) return;
                        if (pc.MapID != pc.TInt["AAA序章剧情图8"]) return;
                        Warp(pc, 10054000, 89, 157);
                    };
                    timer3.Activate();
                };
                timer2.Activate();
            };
            timer.Activate();
        }
    }
}

