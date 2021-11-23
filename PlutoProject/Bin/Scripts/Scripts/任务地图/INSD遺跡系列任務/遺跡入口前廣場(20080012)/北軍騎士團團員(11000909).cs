using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20080012
{
    public class S11000909 : Event
    {
        public S11000909()
        {
            this.EventID = 11000909;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "谢谢帮忙$R;" +
                "$R里面很宽敞，注意不要迷路$R;");
        }
    }
}