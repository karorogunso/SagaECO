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
    public class S70001008 : Event
    {
        public S70001008()
        {
            this.EventID = 70001008;
        }

        public override void OnEvent(ActorPC pc)
        {
            pc.TInt["AAA序章剧情图5"] = CreateMapInstance(64008000, 10054000, 144, 152, true, 0, true);
            Warp(pc, (uint)pc.TInt["AAA序章剧情图5"], 8, 75);
            SetNextMoveEvent(pc, 70001009);
        }
    }
}

