using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30073000
{
    public class S11000284 : Event
    {
        public S11000284()
        {
            this.EventID = 11000284;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "最近阿克罗波利斯怎么样啊？$R;");
            Say(pc, 11000285, 131, "好像没有什么特别的事阿。$R;");
        }
    }
}