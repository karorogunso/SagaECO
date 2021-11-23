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
    public class S70001007 : Event
    {
        public S70001007()
        {
            this.EventID = 70001007;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID != pc.TInt["AAA序章剧情图4"]) return;
            Map map = MapManager.Instance.GetMap(pc.MapID);
            ActorMob mob = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图4"], 16580002, 10006100, 0, 27, 27, 1, 1, 0, AAA序章怪物.有攻击力的伊利斯Info(), AAA序章怪物.有攻击力的伊利斯AI(), null, 0)[0];
            ((MobEventHandler)mob.e).AI.Master = pc;
            ActorMob mob2 = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图4"], 16585100, 10006100, 0, 28, 28, 1, 1, 0, AAA序章怪物.伊利斯AnotherInfo(), AAA序章怪物.伊利斯AnotherAI(), null, 0)[0];
            ((MobEventHandler)mob2.e).AI.Master = pc;
            ShowDialog(pc, 10020);
            ShowDialog(pc, 10021);
            ShowDialog(pc, 10022);
            ShowDialog(pc, 10023);
            short[] pos = new short[2];
            pos[0] = SagaLib.Global.PosX8to16(26, map.Width);
            pos[1] = SagaLib.Global.PosY8to16(7, map.Height);
            map.MoveActor(Map.MOVE_TYPE.START, mob2, pos, 0, 500, false, SagaLib.MoveType.RUN);
            Timer timer = new Timer("AAA序章步骤8", 0, 45000);
            timer.OnTimerCall += (s, e) =>
            {
                timer.Deactivate();
                if (pc == null) return;
                if (pc.MapID != pc.TInt["AAA序章剧情图4"]) return;
                List<ActorMob> mob3 = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图4"], 14710000, 10010100, 1, 26, 14, 3, 2, 0, AAA序章怪物.防卫兵器Info(), AAA序章怪物.防卫兵器AI(), (z,y)=>
                {
                    pc.TInt["AAA序章剧情图4防卫兵器"]++;
                    if (pc.TInt["AAA序章剧情图4防卫兵器"] == 1)
                    {
                        SkillHandler.Instance.ShowEffectOnActor(z.mob, 5002);
                        ShowDialog(pc, 10024);
                    }
                    else
                    {
                        SkillHandler.Instance.ShowEffectOnActor(z.mob, 5002);
                        ShowDialog(pc, 10025);
                        ShowDialog(pc, 10026);
                        Announce((uint)pc.TInt["AAA序章剧情图4"], "突然冒出来的广播：通往下一个房间的传送点已经打开。");
                        ShowPortal(pc, 8, 25, 70001008);
                    }
                }, 1);
                for (int i = 0; i < mob3.Count; i++)
                {
                    MobEventHandler eh = (MobEventHandler)mob3[i].e;
                    if (!eh.AI.Hate.ContainsKey(mob2.ActorID))
                        eh.AI.Hate.Add(mob2.ActorID, 50000);
                    if (!eh.AI.DamageTable.ContainsKey(pc.ActorID))
                        eh.AI.DamageTable.Add(pc.ActorID, 10000);
                }
            };
            timer.Activate();
        }
    }
}

