using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001497 : Event
    {
        public S11001497()
        {
            this.EventID = 11001497;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "好像要珍珠啊……珍珠。$R;", "美人魚");
        }


    }
}


