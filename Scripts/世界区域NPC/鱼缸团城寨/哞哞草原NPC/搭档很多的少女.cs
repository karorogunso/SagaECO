
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
    public class S80000609: Event
    {
        public S80000609()
        {
            this.EventID = 80000609;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "你好啊，你也有属于自己的搭档吗？", "搭档很多的少女");
        }
    }
}