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
            Say(pc, 131, "早上起來，寵物竟然在我旁邊！！！$R;" +
                "$P本來的好夢……$R;" +
                "變成一生最恐怖的惡夢$R;" +
                "               冒險者嘉嘉$R;");
        }
    }
}
