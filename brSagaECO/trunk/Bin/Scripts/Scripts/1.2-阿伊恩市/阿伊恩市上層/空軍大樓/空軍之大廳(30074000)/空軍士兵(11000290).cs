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
            Say(pc, 131, "飛天的感覺真是不錯啊$R;");
        }
    }
}