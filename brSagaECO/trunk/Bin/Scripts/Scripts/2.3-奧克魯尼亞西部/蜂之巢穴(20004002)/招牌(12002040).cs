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
            Say(pc, 131, "寫著什麼……？$R;" +
                "$P袋裡傳來奇怪的香味$R;" +
                "除了奇怪的蘑菇和杰利科$R;" +
                "什麽都沒有呢？？$R;");
        }
    }
}
