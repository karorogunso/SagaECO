using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30075000
{
    public class S11001198 : Event
    {
        public S11001198()
        {
            this.EventID = 11001198;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "怎样，想不想雇佣我呢？$R;", "佣兵");
        }
    }
}