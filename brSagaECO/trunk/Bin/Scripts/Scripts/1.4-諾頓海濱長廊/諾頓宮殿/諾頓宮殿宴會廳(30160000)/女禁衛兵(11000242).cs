using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30160000
{
    public class S11000242 : Event
    {
        public S11000242()
        {
            this.EventID = 11000242;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "我們為保護女王陛下而存在$R;" +
                "$R女王陛下萬歲！$R;");
        }
    }
}