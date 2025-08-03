using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10028000
{
    public class S12002020 : Event
    {
        public S12002020()
        {
            this.EventID = 12002020;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "南边是军舰岛$R;");
        }
    }
}
