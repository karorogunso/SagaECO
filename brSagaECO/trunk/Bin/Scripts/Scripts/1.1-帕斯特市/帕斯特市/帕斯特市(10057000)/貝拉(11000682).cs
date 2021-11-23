using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000682 : Event
    {
        public S11000682()
        {
            this.EventID = 11000682;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "嗉吡！嗉吡！$R;");
        }
    }
}