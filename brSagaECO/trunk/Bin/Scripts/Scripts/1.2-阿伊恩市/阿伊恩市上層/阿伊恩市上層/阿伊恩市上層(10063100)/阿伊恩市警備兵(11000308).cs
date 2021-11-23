using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063100
{
    public class S11000308 : Event
    {
        public S11000308()
        {
            this.EventID = 11000308;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡是議會$R;");
        }
    }
}