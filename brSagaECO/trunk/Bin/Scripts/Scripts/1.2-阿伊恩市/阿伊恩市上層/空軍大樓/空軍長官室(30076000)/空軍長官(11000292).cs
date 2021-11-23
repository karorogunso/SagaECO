using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30076000
{
    public class S11000292 : Event
    {
        public S11000292()
        {
            this.EventID = 11000292;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_0a36)
            {
                Say(pc, 131, "您是隸屬南軍呢。$R;" +
                    "需要唐卡入國許可証嗎？$R;");
                OpenShopBuy(pc, 207);
                return;
            }
            */
            Say(pc, 131, "聽說時空的裂縫加大的話$R;" +
                "氣流會不隱定呢。$R;" +
                "$R不會是真的吧？$R;");
        }
    }
}