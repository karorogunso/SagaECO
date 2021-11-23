using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30154000
{
    public class S11001133 : Event
    {
        public S11001133()
        {
            this.EventID = 11001133;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "嗯，剩下一个部件，$R;" +
                "怎么回事呢？$R;");
        }
    }
}