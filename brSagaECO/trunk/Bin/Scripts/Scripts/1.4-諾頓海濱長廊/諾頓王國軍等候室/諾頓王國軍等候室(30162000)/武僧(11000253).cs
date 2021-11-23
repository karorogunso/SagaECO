using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30162000
{
    public class S11000253 : Event
    {
        public S11000253()
        {
            this.EventID = 11000253;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "哎呀，肚子餓了$R;" +
                "$R有句話說的好，民以食為天嘛$R;" +
                "有沒有吃的呀$R;");
        }
    }
}
