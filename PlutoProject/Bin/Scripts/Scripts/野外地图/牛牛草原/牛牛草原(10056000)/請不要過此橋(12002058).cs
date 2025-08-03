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
            Say(pc, 131, "危险！请勿走近！$R;" +
                "$R;" +
                "                     法伊斯特评议会$R;");
        }
    }
}