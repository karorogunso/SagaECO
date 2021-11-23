using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30081000
{
    public class S11000297 : Event
    {
        public S11000297()
        {
            this.EventID = 11000297;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "工作真快乐呢$R;");
        }
    }
}