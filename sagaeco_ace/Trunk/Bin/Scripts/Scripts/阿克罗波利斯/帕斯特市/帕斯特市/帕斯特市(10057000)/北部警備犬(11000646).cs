using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000646 : Event
    {
        public S11000646()
        {
            this.EventID = 11000646;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "咕嚕咕嚕！$R;");
        }
    }
}