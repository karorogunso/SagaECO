using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000316 : Event
    {
        public S11000316()
        {
            this.EventID = 11000316;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "今天会给什么故事？$R;");
        }
    }
}