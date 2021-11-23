using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10054001
{
    public class S12002059 : Event
    {
        public S12002059()
        {
            this.EventID = 12002059;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡開始是帕斯特市$R;" +
                "                     帕斯特評議會$R;");
        }
    }
}