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
    public class S70001004 : Event
    {
        public S70001004()
        {
            this.EventID = 70001004;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID != pc.TInt["AAA序章剧情图2"]) return;
            Map map = MapManager.Instance.GetMap(pc.MapID);
            Timer timer = new Timer("AAA序章步骤5", 0, 8000);
            timer.OnTimerCall += (s, e) =>
            {
                timer.Deactivate();
                if (pc == null) return;

                ActorMob mob = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图2"], 10990000, 10006100, 0, 125, 194, 1, 1, 0, AAA序章怪物.巴鲁鲁Info(), AAA序章怪物.巴鲁鲁AI(), null, 0)[0];
                ((MobEventHandler)mob.e).AI.Master = pc;
                mob.TInt["playersize"] = 800;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mob, false);
                ShowDialog(pc, 10011);
                SetNextMoveEvent(pc, 70001005);
                Navigate(pc, 153, 170);
            };
            timer.Activate();
        }
    }
}

