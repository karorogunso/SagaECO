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
            Say(pc, 131, "聽說這裡有世界上最大的礦山$R;" +
                "才來的，$R;" +
                "$R礦山到底在哪裡啊?$R;" +
                "$P哎！太熱了。$R;");
        }
    }
}