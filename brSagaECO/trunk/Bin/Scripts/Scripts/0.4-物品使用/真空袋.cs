using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class 真空袋 : Item
    {
        public 真空袋()
        {
            //真空袋一般士兵用
            Init(90000112, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "WEAPON90");
                TakeItem(pc, 10021600, 1);
            });

            //真空袋將軍用
            Init(90000113, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "WEAPON90_rare");
                TakeItem(pc, 10021601, 1);
            });
        }
    }
}