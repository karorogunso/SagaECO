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
    public class S70001011 : Event
    {
        public S70001011()
        {
            this.EventID = 70001011;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID != pc.TInt["AAA序章剧情图6"]) return;
            Map map = MapManager.Instance.GetMap(pc.MapID);
            ActorMob mob = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图6"], 16580002, 10006100, 0, 46, 75, 1, 1, 0, AAA序章怪物.没攻击力的伊利斯Info(), AAA序章怪物.没攻击力的伊利斯AI(), null, 0)[0];
            ((MobEventHandler)mob.e).AI.Master = pc;
            ActorMob mob2 = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图6"], 16585100, 10006100, 0, 47, 75, 1, 1, 0, AAA序章怪物.伊利斯2AnotherInfo(), AAA序章怪物.伊利斯2AnotherAI(), null, 0)[0];
            ((MobEventHandler)mob2.e).AI.Master = pc;
            ShowDialog(pc, 10030);
            ActorMob mob3 = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图6"], 26135200, 10006100, 1, 47, 75, 1, 1, 0, AAA序章怪物.异常者Info(), AAA序章怪物.异常者AI(), (s,e)=>
            {
                ShowDialog(pc, 10032);
                ShowDialog(pc, 10033);
                short[] pos = new short[2];
                pos[0] = SagaLib.Global.PosX8to16(100, map.Width);
                pos[1] = SagaLib.Global.PosY8to16(46, map.Height);
                map.MoveActor(Map.MOVE_TYPE.START, mob2, pos, 0, 500, false, SagaLib.MoveType.RUN);
                Announce((uint)pc.TInt["AAA序章剧情图6"], "突然冒出来的广播：通往下一个房间的传送点已经打开。");
                ShowPortal(pc, 84, 46, 70001012);
            }, 1)[0];
            MobEventHandler eh = (MobEventHandler)mob3.e;
            if (!eh.AI.Hate.ContainsKey(mob2.ActorID))
                eh.AI.Hate.Add(mob2.ActorID, 50000);
            if (!eh.AI.DamageTable.ContainsKey(pc.ActorID))
                eh.AI.DamageTable.Add(pc.ActorID, 10000);
            ShowDialog(pc, 10031);
        }
    }
}

