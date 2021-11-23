using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30162001
{
    public class S11000254 : Event
    {
        public S11000254()
        {
            this.EventID = 11000254;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "姐姐，这次休假$R;" +
                "一起去购物吧$R;");
            Say(pc, 11000255, 131, "好啊$R;" +
                "去哪儿？$R;");
            Say(pc, 11000254, 131, "姐姐想去哪儿？$R;");
            Say(pc, 11000255, 131, "我？$R;" +
                "$R哪儿都可以$R;" +
                "只要跟您去…$R;");
        }
    }
}
