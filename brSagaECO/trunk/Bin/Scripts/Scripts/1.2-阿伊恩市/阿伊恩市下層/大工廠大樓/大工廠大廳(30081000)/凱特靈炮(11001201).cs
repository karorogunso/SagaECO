using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30081000
{
    public class S11001201 : Event
    {
        public S11001201()
        {
            this.EventID = 11001201;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這是我們公司改造的『凱特靈炮』$R;" +
                "$R如果擁有凱特靈炮的話，$R就改造一下試試吧！$R;");
        }
    }
}