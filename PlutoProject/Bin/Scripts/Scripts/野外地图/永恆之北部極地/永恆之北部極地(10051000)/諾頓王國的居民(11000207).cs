using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10051000
{
    public class S11000207 : Event
    {
        public S11000207()
        {
            this.EventID = 11000207;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "危险…$R不能再往前走了$R;");
        }
    }
}
