using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript.M50039000
{
    public class S11001348 : Event
    {
        public S11001348()
        {
            this.EventID = 11001348;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 111, "……。$R;", "モモ");
        }
    }
}