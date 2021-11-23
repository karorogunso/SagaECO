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
    public class S70001014 : Event
    {
        public S70001014()
        {
            this.EventID = 70001014;
        }

        public override void OnEvent(ActorPC pc)
        {
            pc.TInt["AAA序章剧情图8"] = CreateMapInstance(61004000, 10054000, 144, 152, true, 0, true);
            Warp(pc, (uint)pc.TInt["AAA序章剧情图8"], 80, 13);
            SetNextMoveEvent(pc, 70001015);
        }
    }
}

