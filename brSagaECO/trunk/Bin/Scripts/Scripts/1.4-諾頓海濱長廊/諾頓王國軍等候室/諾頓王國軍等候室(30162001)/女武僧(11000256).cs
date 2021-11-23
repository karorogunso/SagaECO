using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30162001
{
    public class S11000256 : Event
    {
        public S11000256()
        {
            this.EventID = 11000256;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "薔微是諾頓王國的國花$R;" +
                "您不覺得很漂亮嗎…$R;");
        }
    }
}
