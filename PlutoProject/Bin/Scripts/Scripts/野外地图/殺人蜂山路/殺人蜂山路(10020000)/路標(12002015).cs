using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10020000
{
    public class S12002015 : Event
    {
        public S12002015()
        {
            this.EventID = 12002015;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "南边是不死之岛和军舰岛$R;");
        }
    }
}
