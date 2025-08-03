using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000753 : Event
    {
        public S11000753()
        {
            this.EventID = 11000753;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 255, "噼啪劈啪!$R;" +
                "嘿呦!嘿呦$R;");
        }
    }
}