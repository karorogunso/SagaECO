using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10035000
{
    public class S11000867 : Event
    {
        public S11000867()
        {
            this.EventID = 11000867;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 135, "嘿嘿嘿…$R;" +
                "$R無法進入摩根國境，不辛苦嗎？$R;");
            switch (Select(pc, "“想買那個嗎？”", "", "買", "不買"))
            {
                case 1:
                    OpenShopBuy(pc, 185);
                    Say(pc, 135, "哎呀！謝謝！$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}