using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000664 : Event
    {
        public S11000664()
        {
            this.EventID = 11000664;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "咕噜噜噜？$R;");
        }
    }
}