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
            Say(pc, 131, "这是我们公司改造的『加特林机关炮』$R;" +
                "$R如果拥有加特林机关炮的话，$R就改造一下试试吧！$R;");
        }
    }
}