using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30074000
{
    public class S11000290 : Event
    {
        public S11000290()
        {
            this.EventID = 11000290;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "飞天的感觉真是不错啊$R;");
        }
    }
}