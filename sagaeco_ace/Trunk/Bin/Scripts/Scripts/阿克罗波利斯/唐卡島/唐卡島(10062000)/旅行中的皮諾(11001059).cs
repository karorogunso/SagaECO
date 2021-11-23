using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001059 : Event
    {
        public S11001059()
        {
            this.EventID = 11001059;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 131, "我是旅行的皮诺！$R;" +
                    "$R为了成为埃米尔在环游世界唷。$R;");
                return;
            }
            Say(pc, 131, "这里可以成为埃米尔吗？$R;");
            Say(pc, 11001060, 131, "不知道行不行呀，$R;" +
                "应该能找到线索吧…$R;" +
                "$R这里是您最初的皮诺玩偶故乡！$R;");
        }
    }
}