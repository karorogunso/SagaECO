using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000318 : Event
    {
        public S11000318()
        {
            this.EventID = 11000318;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "卜卜…$R;");
        }
    }
}