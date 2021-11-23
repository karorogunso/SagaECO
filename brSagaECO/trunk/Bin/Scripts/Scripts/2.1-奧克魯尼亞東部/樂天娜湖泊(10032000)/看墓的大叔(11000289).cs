using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10032000
{
    public class S11000289 : Event
    {
        public S11000289()
        {
            this.EventID = 11000289;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡是湖畔的墳地!$R;" +
                "$R仰望湖的話會洗走悲傷的!$R;");
        }
    }
}
