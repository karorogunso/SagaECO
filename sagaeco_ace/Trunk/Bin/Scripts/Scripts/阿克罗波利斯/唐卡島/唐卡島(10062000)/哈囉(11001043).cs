using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001043 : Event
    {
        public S11001043()
        {
            this.EventID = 11001043;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 255, "酷啦啦~$R;");
        }
    }
}