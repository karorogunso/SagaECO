using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Map;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class S11001669 : Event
    {
        public S11001669()
        {
            this.EventID = 11001669;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, ".....没油了...$R", "抵抗军成员佣兵");
            Say(pc, 0, "真的!!! ", "抵抗军成员佣兵");
        }
    }
}