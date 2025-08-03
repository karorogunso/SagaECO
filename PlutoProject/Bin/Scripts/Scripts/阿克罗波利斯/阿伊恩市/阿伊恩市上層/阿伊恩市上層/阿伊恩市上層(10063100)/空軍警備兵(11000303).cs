using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063100
{
    public class S11000303 : Event
    {
        public S11000303()
        {
            this.EventID = 11000303;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这里是空军本部$R;");
        }
    }
}