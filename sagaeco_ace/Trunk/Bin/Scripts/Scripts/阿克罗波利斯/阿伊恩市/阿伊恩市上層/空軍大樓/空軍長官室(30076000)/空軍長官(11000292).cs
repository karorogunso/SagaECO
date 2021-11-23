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
            Say(pc, 131, "听说时空的裂缝加大的话$R;" +
                "气流会不隐定呢。$R;" +
                "$R不会是真的吧？$R;");
        }
    }
}