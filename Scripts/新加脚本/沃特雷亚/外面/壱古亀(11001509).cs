using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001509 : Event
    {
        public S11001509()
        {
            this.EventID = 11001509;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "斯ー斯ー……。$R;", "壱古亀");
        }


    }
}


