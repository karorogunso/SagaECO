using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30082000
{
    public class S11000301 : Event
    {
        public S11000301()
        {
            this.EventID = 11000301;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "啊，我好想休息啊$R;");
        }
    }
}