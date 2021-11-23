using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M30166000
{
    public class S11001976 : Event
    {
        public S11001976()
        {
            this.EventID = 11001976;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "よぅ、お疲れ。$R;" +
            "早速だが……取引するかい？$R;", "壊し屋バキア");
            switch (Select(pc, "取引する？", "", "取引する", "取引しない", "何を？"))
            {
                case 1:
                    OpenShopBuy(pc, 269);
                    break;
                case 2:
                    Say(pc, 131, "おぅ、あれが入り用になったら$R;" +
                    "またこっちに来てくれよな。$R;" +
                    "表通りじゃ流石に出せないんでね。$R;", "壊し屋バキア");
                    break;
                case 4:
                    break;
            }
        }
    }
}
