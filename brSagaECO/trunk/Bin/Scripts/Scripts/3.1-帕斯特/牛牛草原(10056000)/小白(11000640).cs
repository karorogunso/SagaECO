using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000640 : Event
    {
        public S11000640()
        {
            this.EventID = 11000640;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 500, "哇嗚！$R;");
            Say(pc, 11000638, 131, "這小子！叫你別叫了！$R;");
        }
    }
}