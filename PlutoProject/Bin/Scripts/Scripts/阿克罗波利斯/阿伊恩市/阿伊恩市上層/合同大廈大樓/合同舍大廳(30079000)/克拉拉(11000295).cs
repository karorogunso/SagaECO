using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30079000
{
    public class S11000295 : Event
    {
        public S11000295()
        {
            this.EventID = 11000295;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 190, "太慢了，$R;" +
                "所以我才不喜欢政府啊$R;");
        }
    }
}