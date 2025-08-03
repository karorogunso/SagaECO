using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10018100
{
    public class S11000337 : Event
    {
        public S11000337()
        {
            this.EventID = 11000337;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 500, "……$R;");
        }
    }
}