using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10054000
{
    public class S11000658 : Event
    {
        public S11000658()
        {
            this.EventID = 11000658;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "不要在里面惹事$R;");
        }
    }
}