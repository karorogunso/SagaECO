using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30130000
{
    public class S11000696 : Event
    {
        public S11000696()
        {
            this.EventID = 11000696;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "哇!是冒险家!$R真的不知多久没见到了!!$R;");
        }
    }
}