using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50006000
{
    public class S11001096 : Event
    {
        public S11001096()
        {
            this.EventID = 11001096;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 111, "……$R;");
        }
    }
}