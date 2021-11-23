using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000681 : Event
    {
        public S11000681()
        {
            this.EventID = 11000681;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "受贝利尔的委托$R;" +
                "虽然正在养皮露露$R;" +
                "$R但到底打算要怎样？$R;");
        }
    }
}