using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30154000
{
    public class S11001135 : Event
    {
        public S11001135()
        {
            this.EventID = 11001135;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "怎麼看都像蝌蚪…$R;");
        }
    }
}