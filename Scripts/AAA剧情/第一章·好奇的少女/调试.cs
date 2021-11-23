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
    public class S70001999 : Event
    {
        public S70001999()
        {
            this.EventID = 70001999;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(Select(pc,"调试？","","7","b") == 1)
            {
                pc.TInt["AAA序章剧情图4"] = CreateMapInstance(60001000, 10054000, 144, 152, true, 0, true);
                Warp(pc, (uint)pc.TInt["AAA序章剧情图4"], 26, 26);
                SetNextMoveEvent(pc, 70001007);
            }


        }
    }
}

