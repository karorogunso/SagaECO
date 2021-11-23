
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
    public class S80000606: Event
    {
        public S80000606()
        {
            this.EventID = 80000606;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "河那边的洞穴里面到底有什么呢……？", "凝望河的少女");
            Say(pc, 0, "要是桥修好了的话，就能去看看了吧？", "凝望河的少女");
        }
    }
}