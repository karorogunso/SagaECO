
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S12001118: Event
    {
        public S12001118()
        {
            this.EventID = 12001118;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Select(pc, "要出去吗？", "", "出去", "我要睡在这里") == 1)
            {
                Warp(pc, 10054000, 164, 145);
                pc.TInt["副本复活标记"] = 0;
            }
            else
                return;
        }
    }
}