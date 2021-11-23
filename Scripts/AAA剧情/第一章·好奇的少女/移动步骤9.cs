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
    public class S70001009 : Event
    {
        public S70001009()
        {
            this.EventID = 70001009;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID != pc.TInt["AAA序章剧情图5"]) return;
            Map map = MapManager.Instance.GetMap(pc.MapID);
            ActorMob mob = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图5"], 16580002, 10006100, 0, 8, 72, 1, 1, 0, AAA序章怪物.有攻击力的伊利斯Info(), AAA序章怪物.有攻击力的伊利斯AI(), null, 0)[0];
            ((MobEventHandler)mob.e).AI.Master = pc;
            ActorMob mob2 = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图5"], 16585100, 10006100, 0, 8, 47, 1, 1, 0, AAA序章怪物.伊利斯AnotherInfo(), AAA序章怪物.伊利斯AnotherAI(), null, 0)[0];
            ((MobEventHandler)mob2.e).AI.Master = pc;
            ShowDialog(pc, 10027);
            List<ActorMob> mobs = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图5"], 14690000, 10006100, 1, 8, 47, 6, 6, 0, AAA序章怪物.DEM3Info(), AAA序章怪物.DEM3AI(), (s,e) =>
            {
                pc.TInt["AAA序章剧情图5怪物"]++;
                if (pc.TInt["AAA序章剧情图5怪物"] == 4)
                {
                    ShowDialog(pc, 10028);
                }
                else if (pc.TInt["AAA序章剧情图5怪物"] == 6)
                {
                    short[] pos = new short[2];
                    pos[0] = SagaLib.Global.PosX8to16(0, map.Width);
                    pos[1] = SagaLib.Global.PosY8to16(0, map.Height);
                    map.MoveActor(Map.MOVE_TYPE.START, mob2, pos, 0, 500, false, SagaLib.MoveType.RUN);
                    ShowDialog(pc, 10029);
                    Announce((uint)pc.TInt["AAA序章剧情图5"], "突然冒出来的广播：通往下一个房间的传送点已经打开。");
                    ShowPortal(pc, 8, 17, 70001010);
                }
            }, 1);
            for (int i = 0; i < mobs.Count; i++)
            {
                MobEventHandler eh = (MobEventHandler)mobs[i].e;
                if (!eh.AI.Hate.ContainsKey(mob2.ActorID))
                    eh.AI.Hate.Add(mob2.ActorID, 50000);
                if (!eh.AI.DamageTable.ContainsKey(pc.ActorID))
                    eh.AI.DamageTable.Add(pc.ActorID, 10000);
            }
        }
    }
}

