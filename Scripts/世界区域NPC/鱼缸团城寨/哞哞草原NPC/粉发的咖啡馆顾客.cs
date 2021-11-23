
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
    public class S80000601: Event
    {
        public S80000601()
        {
            this.EventID = 80000601;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "我听说法伊斯特在很久之前爆发过一种病毒，$R城镇居民一夜之间全部死光了，$R现在那边已经是死城了。", "粉发的少女");
            Say(pc, 0, "偷听别人谈话似乎不太好，还是快离开吧。", " ");
        }
    }
}