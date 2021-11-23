using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20020000
{
    public class S11000561 : Event
    {
        public S11000561()
        {
            this.EventID = 11000561;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "會妨礙公事的，$R;" +
                "外人請回去！$R;");
        }
    }
}