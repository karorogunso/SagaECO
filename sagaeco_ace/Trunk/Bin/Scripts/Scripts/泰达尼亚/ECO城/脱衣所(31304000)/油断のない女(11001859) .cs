using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001859 : Event
    {
        public S11001859()
        {
            this.EventID = 11001859;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "ちょっと今、話かけないで……。$R;" +
            "今このタイミングを逃してしまうと$R;" +
            "完璧なケアが出来なくなっちゃう。$R;", "油断のない女");
        }


    }
}


