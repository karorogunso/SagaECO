using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002044 : Event
    {
        public S12002044()
        {
            this.EventID = 12002044;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "早上起来，宠物竟然在我旁边！！！$R;" +
                "$P本来的好梦……$R;" +
                "变成一生最恐怖的恶梦$R;" +
                "               冒险者嘉嘉$R;");
        }
    }
}
