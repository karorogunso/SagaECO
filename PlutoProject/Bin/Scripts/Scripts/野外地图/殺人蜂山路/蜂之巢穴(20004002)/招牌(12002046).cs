using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M20004002
{
    public class S12002046 : Event
    {
        public S12002046()
        {
            this.EventID = 12002046;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "从这巢的缝看进去的话$R;" +
                "涌出阵阵蜂蜜的香气$R;" +
                "$R所以飞虫都聚集过来了$R;");
        }
    }
}
