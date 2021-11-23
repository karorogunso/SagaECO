using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001502 : Event
    {
        public S11001502()
        {
            this.EventID = 11001502;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "不要說沒有用的話。$R;", "美人魚藥店");
        }


    }
}


