using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30078000
{
    public class S11000578 : Event
    {
        public S11000578()
        {
            this.EventID = 11000578;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Account.GMLevel == 0)
            {
                switch (Select(pc, "怎麼辦呢？", "", "清潔計劃", "什麼也不做"))
                {
                    case 1:
                        Say(pc, 131, "未開放$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "現在議會休會中$R;");
        }
    }
}