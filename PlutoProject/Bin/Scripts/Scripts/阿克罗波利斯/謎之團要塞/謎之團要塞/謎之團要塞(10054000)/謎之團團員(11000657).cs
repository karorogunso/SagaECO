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
    public class S11000657 : Event
    {
        public S11000657()
        {
            this.EventID = 11000657;
        }

        public override void OnEvent(ActorPC pc)
        {
                Say(pc, 131, "不要妨碍监视。$R;");
        }
    }
}