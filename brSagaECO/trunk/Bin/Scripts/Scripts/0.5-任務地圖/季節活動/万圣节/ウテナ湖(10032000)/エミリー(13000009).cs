using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10032000
{
    public class S13000009 : Event
    {
        public S13000009()
        {
            this.EventID = 13000009;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 131, "祈りをささげていて$R;" +
            "こちらに気がつかないようだ。$R;", " ");
        }
    }
}
