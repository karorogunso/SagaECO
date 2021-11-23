using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000312 : Event
    {
        public S11000312()
        {
            this.EventID = 11000312;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "啊，要进去吗？$R;");
        }
    }
}