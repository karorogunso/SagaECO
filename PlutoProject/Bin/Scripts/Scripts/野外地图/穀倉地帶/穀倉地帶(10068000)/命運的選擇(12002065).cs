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
            Say(pc, 131, "西边延续‘生命’的大地$R;" +
                "东边超越‘死亡’的岛$R;");
        }
    }
}