using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10020000
{
    public class S12002016 : Event
    {
        public S12002016()
        {
            this.EventID = 12002016;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "西边不死之岛$R;" +
                "南边军舰岛$R;");
        }
    }
}
