
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
    public class S80000807: Event
    {
        public S80000807()
        {
            this.EventID = 80000807;
        }

        public override void OnEvent(ActorPC pc)
        {
            ChangeMessageBox(pc);
            Say(pc, 0, "好慢啊……到底在干些什么呢？", "沙月");
        }
    }
}