using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000324 : Event
    {
        public S11000324()
        {
            this.EventID = 11000324;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "(要阻止他才行呢)$R;");
        }
    }
}