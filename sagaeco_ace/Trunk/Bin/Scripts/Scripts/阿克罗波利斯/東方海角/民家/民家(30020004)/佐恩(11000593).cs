using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30020004
{
    public class S11000593 : Event
    {
        public S11000593()
        {
            this.EventID = 11000593;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 501, "咕嚕~$R;");
        }
    }
}