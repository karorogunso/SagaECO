
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.ActorEventHandlers;
namespace SagaScript.M30210000
{
    public class S60000999 : Event
    {
        public S60000999()
        {
            this.EventID = 60000999;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "注意！$R再往前走你将会回到原版世界！$R$R请注意以下内容：$R-原版世界大部分NPC已被隐藏$R-所有怪物无掉落$R-部分传送点被迫封印$R-原版怪物非常危险！！$R$R祝您旅途愉快！", "旧世界告示板");
        }
    }
}

