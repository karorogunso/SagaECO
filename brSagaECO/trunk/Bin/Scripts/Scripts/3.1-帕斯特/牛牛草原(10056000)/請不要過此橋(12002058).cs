using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S12002058 : Event
    {
        public S12002058()
        {
            this.EventID = 12002058;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "危險！請勿走近！$R;" +
                "$R;" +
                "                     帕斯特評議會$R;");
        }
    }
}