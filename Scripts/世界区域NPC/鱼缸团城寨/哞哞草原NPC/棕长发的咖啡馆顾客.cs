
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
    public class S80000603: Event
    {
        public S80000603()
        {
            this.EventID = 80000603;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "老妹到底哪去了呢？$R我明明跟她说过这附近很危险的。", "看起来是姐姐的少女");
            Say(pc, 0, "啊啊，$R要是出了什么事我怎么向妈妈交代啊。", "看起来是姐姐的少女");
        }
    }
}