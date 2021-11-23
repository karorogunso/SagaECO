using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30081000
{
    public class S11000558 : Event
    {
        public S11000558()
        {
            this.EventID = 11000558;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "操作一些東西$R;" +
                "是非常快樂的事。$R;");
        }
    }
}