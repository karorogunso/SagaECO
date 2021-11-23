using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10068000
{
    public class S12002064 : Event
    {
        public S12002064()
        {
            this.EventID = 12002064;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡是帕斯特市$R;" +
                "                     帕斯特評議會$R;");
        }
    }
}