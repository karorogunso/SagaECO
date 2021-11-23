using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002043 : Event
    {
        public S12002043()
        {
            this.EventID = 12002043;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "北部通路非常危險！$R;" +
                "沒有自信的人請走西部通路$R;" +
                "當然，我會走北邊的！$R;" +
                "         冒險者帕米拉小姐$R;");
        }
    }
}
