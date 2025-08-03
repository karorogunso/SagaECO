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
            Say(pc, 131, "不喜欢冷，才从诺森来到这里，$R;" +
                "$R但是为什么这里那么热啊？$R;" +
                "$P适合生存的地方$R;" +
                "到底在哪里？$R;");
        }
    }
}