using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public class 激賞禮券 : Item
    {
        public 激賞禮券()
        {
            //激賞禮券 1
            Init(82200053, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "LOTTO1");
                TakeItem(pc, 21050014, 1);
            });

            //激賞禮券 2
            Init(82200050, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "LOTTO2");
                TakeItem(pc, 21050013, 1);
            });
        }
    }
}