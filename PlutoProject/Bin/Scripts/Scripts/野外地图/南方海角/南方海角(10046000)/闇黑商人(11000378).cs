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
                "$R无法进入艾恩萨乌斯联邦国国境$R;" +
                "不辛苦吗？$R;");
            switch (Select(pc, "想买那个吗？", "", "买", "不买"))
            {
                case 1:
                    OpenShopBuy(pc, 104);
                    Say(pc, 135, "哎呀！谢谢！$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}

