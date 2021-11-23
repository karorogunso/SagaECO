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
    public class S70001005 : Event
    {
        public S70001005()
        {
            this.EventID = 70001005;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID != pc.TInt["AAA序章剧情图2"]) return;
            Map map = MapManager.Instance.GetMap(pc.MapID);
            Timer timer = new Timer("AAA序章步骤6", 0, 10000);
            timer.OnTimerCall += (s, e) =>
            {
                timer.Deactivate();
                if (pc == null) return;

                NavigateCancel(pc);
                Say(pc, 0, "请击杀该DEM以继续任务。", "译本注释①");
                ShowDialog(pc, 10012);
                ShowDialog(pc, 10013);
                ActorMob mob = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图2"], 19070000, 10006100, 1, 151, 136, 1, 1, 0, AAA序章怪物.DEM1Info(), AAA序章怪物.DEM1AI(), (x, y) => {
                    pc.TInt["AAA序章剧情图3"] = CreateMapInstance(60904000, 10054000, 144, 152, true, 0, true);
                    Warp(pc, (uint)pc.TInt["AAA序章剧情图3"], 151, 97);
                    SetNextMoveEvent(pc, 70001006);
                }, 1)[0];

            };
            timer.Activate();
        }
    }
}

