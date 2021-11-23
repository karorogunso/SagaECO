using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000759 : Event
    {
        public S11000759()
        {
            this.EventID = 11000759;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 255, "接近的话很危险！$R;");
        }
    }
}