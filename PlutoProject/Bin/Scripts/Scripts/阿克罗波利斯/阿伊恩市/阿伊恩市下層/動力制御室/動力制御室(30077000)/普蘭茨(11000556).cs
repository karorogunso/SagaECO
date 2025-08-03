using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30077000
{
    public class S11000556 : Event
    {
        public S11000556()
        {
            this.EventID = 11000556;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "要检查彻底才行！$R;");
        }
    }
}