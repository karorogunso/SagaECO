using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30156000
{
    public class S11001067 : Event
    {
        public S11001067()
        {
            this.EventID = 11001067;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 255, "嗯？$R;" +
                    "什麼事情呢？$R;");
                return;
            }
            Say(pc, 255, "……$R;");
        }
    }
}