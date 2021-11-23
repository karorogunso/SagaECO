using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10018100
{
    public class S11000336 : Event
    {
        public S11000336()
        {
            this.EventID = 11000336;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 500, "汪汪！！汪！$R;");
        }
    }
}