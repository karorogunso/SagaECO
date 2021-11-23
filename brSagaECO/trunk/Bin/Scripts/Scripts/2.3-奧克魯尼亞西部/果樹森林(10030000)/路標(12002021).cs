using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10030000
{
    public class S12002021 : Event
    {
        public S12002021()
        {
            this.EventID = 12002021;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "果樹森林$R;");
        }
    }
}
