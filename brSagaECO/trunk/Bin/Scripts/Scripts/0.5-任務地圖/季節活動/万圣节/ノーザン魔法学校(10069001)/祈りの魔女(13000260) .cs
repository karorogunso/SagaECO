using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10069001
{
    public class S13000260 : Event
    {
        public S13000260()
        {
            this.EventID = 13000260;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, 131, "なにやら怪しげな呪文を唱えている。$R;", " ");
        }
    }
}