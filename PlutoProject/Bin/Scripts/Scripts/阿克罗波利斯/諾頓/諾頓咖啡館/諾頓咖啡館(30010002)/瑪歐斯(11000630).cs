using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010002
{
    public class S11000630 : Event
    {
        public S11000630()
        {
            this.EventID = 11000630;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "嗯..$R;" +
                "这里的浓汤真好吃$R;" +
                "$R味道扩散到全身了$R;");
        }
    }
}
