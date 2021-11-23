using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10046000
{
    public class S11000378 : Event
    {
        public S11000378()
        {
            this.EventID = 11000378;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 135, "嘿嘿嘿……$R;" +
                "$R無法進入阿伊恩薩烏斯聯邦國國境$R;" +
                "不辛苦嗎？$R;");
            switch (Select(pc, "想買那個嗎？", "", "買", "不買"))
            {
                case 1:
                    OpenShopBuy(pc, 104);
                    Say(pc, 135, "哎呀！謝謝！$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}

