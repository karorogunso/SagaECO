using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000264 : Event
    {
        public S13000264()
        {
            this.EventID = 13000264;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 13000263, 65535, "あの大きな釜は$R;" +
            "死者を呼び出すためのものよ。$R;", "二年生");
        }
    }
}