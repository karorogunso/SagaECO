
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S80000204 : Event
    {
        public S80000204()
        {
            this.EventID = 80000204;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "回头吧，$R这前方不是我们应该去的地方…$R$R放心，$R只要封印还在，$R这里就很安全，$R我们去另一边看看吧！", "暗鸣");
        }
    }
}

