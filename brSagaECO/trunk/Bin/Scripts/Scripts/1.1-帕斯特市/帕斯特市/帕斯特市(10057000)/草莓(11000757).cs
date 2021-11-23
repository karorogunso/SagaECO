using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000757 : Event
    {
        public S11000757()
        {
            this.EventID = 11000757;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 255, "粉紅色松鼠還没好嗎…？$R;" +
                "今天想早點回家$R;" +
                "$R落日之後，南邊的城裡$R;" +
                "有恐怖的東西出來啊！$R;");
        }
    }
}