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
    public class S70001006 : Event
    {
        public S70001006()
        {
            this.EventID = 70001006;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID != pc.TInt["AAA序章剧情图3"]) return;
            Map map = MapManager.Instance.GetMap(pc.MapID);
            ActorMob mob = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图3"], 16580002, 10006100, 0, 155, 97, 1, 1, 0, AAA序章怪物.有攻击力的伊利斯Info(), AAA序章怪物.有攻击力的伊利斯AI(), null, 0)[0];
            ((MobEventHandler)mob.e).AI.Master = pc;
            Navigate(pc, 150, 93);
            Timer timer = new Timer("AAA序章步骤6", 0, 10000);
            timer.OnTimerCall += (s, e) =>
            {
                timer.Deactivate();
                if (pc == null) return;

                ShowDialog(pc, 10014);
                NavigateCancel(pc);
                ActorMob mob2 = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图3"], 16585100, 10006500, 0, 147, 74, 1, 1, 0, AAA序章怪物.伊利斯AnotherInfo(), AAA序章怪物.伊利斯AnotherAI(), null, 0)[0];
                ((MobEventHandler)mob2.e).AI.Master = pc;
                SkillHandler.Instance.ActorSpeak(mob2, "你们是谁？是从哪里来的？");
                List<ActorMob> mobs = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图3"], 19070000, 10010100, 1, 147, 74, 5, 3, 0, AAA序章怪物.DEM2Info(), AAA序章怪物.DEM2AI(), (x,y)=>
                {
                    pc.TInt["AAA剧情序章DEM死亡"]++;
                    if(pc.TInt["AAA剧情序章DEM死亡"] == 2)
                    {
                        ShowDialog(pc, 10015);
                        ((MobEventHandler)mob2.e).AI.Pause();
                        short[] pos = new short[2];
                        pos[0] = SagaLib.Global.PosX8to16(129, map.Width);
                        pos[1] = SagaLib.Global.PosY8to16(93, map.Height);
                        map.MoveActor(Map.MOVE_TYPE.START, mob2, pos, 45, 500);
                        ShowDialog(pc, 10016);
                        ShowDialog(pc, 10017);
                        ShowDialog(pc, 10018);
                        ShowDialog(pc, 10019);
                        Timer t2 = new Timer("AAA序章步骤7", 0, 100000);
                        t2.OnTimerCall += (z, c) =>
                        {
                            t2.Deactivate();
                            if (pc == null) return;
                            if (pc.MapID != (uint)pc.TInt["AAA序章剧情图3"]) return;
                            pc.TInt["AAA序章剧情图4"] = CreateMapInstance(60001000, 10054000, 144, 152, true, 0, true);
                            Warp(pc, (uint)pc.TInt["AAA序章剧情图4"], 26, 26);
                            SetNextMoveEvent(pc, 70001007);
                        };
                        t2.Activate();
                    };
                }, 1);
                for (int i = 0; i < mobs.Count; i++)
                {
                    MobEventHandler eh = (MobEventHandler)mobs[i].e;
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

