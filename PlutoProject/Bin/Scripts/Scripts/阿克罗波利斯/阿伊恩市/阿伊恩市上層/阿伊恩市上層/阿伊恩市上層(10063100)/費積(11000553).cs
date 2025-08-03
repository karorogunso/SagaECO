using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10063100
{
    public class S11000553 : Event
    {
        public S11000553()
        {
            this.EventID = 11000553;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "听说这里有世界上最大的矿山$R;" +
                "才来的，$R;" +
                "$R矿山到底在哪里啊?$R;" +
                "$P哎！太热了。$R;");
        }
    }
}