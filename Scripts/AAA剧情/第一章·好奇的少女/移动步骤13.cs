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
    public class S70001013 : Event
    {
        public S70001013()
        {
            this.EventID = 70001013;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID != pc.TInt["AAA序章剧情图7"]) return;
            Map map = MapManager.Instance.GetMap(pc.MapID);
            ActorMob mob = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图7"], 16580002, 10006100, 0, 41, 45, 1, 1, 0, AAA序章怪物.没攻击力的伊利斯Info(), AAA序章怪物.没攻击力的伊利斯AI(), null, 0)[0];
            ((MobEventHandler)mob.e).AI.Master = pc;
            ActorMob mob2 = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图7"], 16585100, 10006100, 0, 41, 45, 1, 1, 0, AAA序章怪物.伊利斯2AnotherInfo(), AAA序章怪物.伊利斯2AnotherAI(), null, 0)[0];
            ((MobEventHandler)mob2.e).AI.Master = pc;
            List<ActorMob> mobs = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图7"], 26135200, 10006100, 1, 41, 45, 100, 10, 0, AAA序章怪物.异常者XInfo(), AAA序章怪物.异常者XAI(), null, 0);
            ShowDialog(pc, 10034);
            ShowDialog(pc, 10035);
            Timer timer = new Timer("AAA序章步骤9", 0, 60000);
            timer.OnTimerCall += (s, e) =>
            {
                timer.Deactivate();
                if (pc == null) return;
                if (pc.MapID != pc.TInt["AAA序章剧情图7"]) return;

                ShowDialog(pc, 10036);
                ShowPortal(pc, 81, 85, 70001014);
            };
            timer.Activate();
        }
    }
}

