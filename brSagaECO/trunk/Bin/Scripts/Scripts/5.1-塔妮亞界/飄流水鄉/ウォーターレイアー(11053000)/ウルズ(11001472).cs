using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S11001472 : Event
    {
        public S11001472()
        {
            this.EventID = 11001472;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "管理世界樹$R;" +
            "是我們姐妹的責任。$R;" +
            "$R如果要破壞世界樹的話$R;" +
            "是無論如何都不能原諒的。$R;", "ウルズ");
        }


    }
}


