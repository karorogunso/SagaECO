using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30091000
{
    public class S11000698 : Event
    {
        public S11000698()
        {
            this.EventID = 11000698;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "呼呼$R;" +
                "來上學的嗎？$R;");
        }
    }
}