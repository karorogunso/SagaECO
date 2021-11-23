using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10061000
{
    public class S11000551 : Event
    {
        public S11000551()
        {
            this.EventID = 11000551;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "不喜歡冷，才從諾頓來到這裡，$R;" +
                "$R但是為什麼這裡那麼熱啊？$R;" +
                "$P適合生存的地方$R;" +
                "到底在哪裡？$R;");
        }
    }
}