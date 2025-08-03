using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000322 : Event
    {
        public S11000322()
        {
            this.EventID = 11000322;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "总有一天，我要统一全世界啊！$R;");
        }
    }
}