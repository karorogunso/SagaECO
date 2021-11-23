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
    public class S70001010 : Event
    {
        public S70001010()
        {
            this.EventID = 70001010;
        }

        public override void OnEvent(ActorPC pc)
        {
            pc.TInt["AAA序章剧情图6"] = CreateMapInstance(64009000, 10054000, 144, 152, true, 0, true);
            Warp(pc, (uint)pc.TInt["AAA序章剧情图6"], 45, 81);
            SetNextMoveEvent(pc, 70001011);
        }
    }
}

