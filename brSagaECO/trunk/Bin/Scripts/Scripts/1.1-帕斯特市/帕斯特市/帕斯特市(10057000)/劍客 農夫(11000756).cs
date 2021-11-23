using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000756 : Event
    {
        public S11000756()
        {
            this.EventID = 11000756;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 255, "我要成為一個聖騎士！$R;" +
                "$R不能在這種農村裡被埋没掉！$R;");
        }
    }
}