using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20081000
{
    public class S12001069 : Event
    {
        public S12001069()
        {
            this.EventID = 12001069;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "上面好像有寫著什麽？$R;");
        }
    }
}
