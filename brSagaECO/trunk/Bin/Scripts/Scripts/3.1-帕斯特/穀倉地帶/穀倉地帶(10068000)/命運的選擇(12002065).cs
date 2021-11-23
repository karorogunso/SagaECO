using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10068000
{
    public class S12002065 : Event
    {
        public S12002065()
        {
            this.EventID = 12002065;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "西邊延續「生命」的大地$R;" +
                "東邊超越「死亡」的島$R;");
        }
    }
}