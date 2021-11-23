using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000654 : Event
    {
        public S11000654()
        {
            this.EventID = 11000654;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "『绿盾军快点过来！』$R;" +
                "$P…挑起这样的牌子$R;");
        }
    }
}