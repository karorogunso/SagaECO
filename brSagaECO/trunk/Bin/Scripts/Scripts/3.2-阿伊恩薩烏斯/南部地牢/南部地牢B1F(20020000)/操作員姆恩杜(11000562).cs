using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20020000
{
    public class S11000562 : Event
    {
        public S11000562()
        {
            this.EventID = 11000562;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "這裡有冒險者把魔物擊退了$R;" +
                "所以安全多了$R;" +
                "$R但是還不能安心$R;");
        }
    }
}