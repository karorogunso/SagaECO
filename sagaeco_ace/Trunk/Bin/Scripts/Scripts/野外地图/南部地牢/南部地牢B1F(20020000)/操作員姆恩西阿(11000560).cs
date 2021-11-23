using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20020000
{
    public class S11000560 : Event
    {
        public S11000560()
        {
            this.EventID = 11000560;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这是货运列车经过的地方，$R;" +
                "要小心啊$R;" +
                "$R不要妨碍运输阿。$R;");
        }
    }
}