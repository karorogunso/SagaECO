using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000752 : Event
    {
        public S11000752()
        {
            this.EventID = 11000752;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 255, "为了我们的孩子$R;" +
                "嘿呦!嘿呦$R;");
        }
    }
}