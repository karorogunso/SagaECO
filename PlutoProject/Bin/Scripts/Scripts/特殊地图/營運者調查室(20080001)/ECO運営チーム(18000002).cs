using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M20080001
{
    public class S18000002 : Event
    {
        public S18000002()
        {
            this.EventID = 18000002;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "你干了什么坏事了吗。$R;" +
            "$R要不怎么会被抓到这来！$R;", "ECO運営チーム");
        }


    }
}


