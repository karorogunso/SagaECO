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
                "$R无法进入摩戈国境，不辛苦吗？$R;");
            switch (Select(pc, "“想买那个吗？”", "", "买", "不买"))
            {
                case 1:
                    OpenShopBuy(pc, 185);
                    Say(pc, 135, "哎呀！谢谢！$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}