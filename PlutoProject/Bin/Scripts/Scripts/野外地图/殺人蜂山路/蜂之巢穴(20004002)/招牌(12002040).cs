using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002040 : Event
    {
        public S12002040()
        {
            this.EventID = 12002040;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "写着什么……？$R;" +
                "$P袋里传来奇怪的香味$R;" +
                "除了奇怪的蘑菇和杰利科$R;" +
                "什么都没有呢？？$R;");
        }
    }
}
