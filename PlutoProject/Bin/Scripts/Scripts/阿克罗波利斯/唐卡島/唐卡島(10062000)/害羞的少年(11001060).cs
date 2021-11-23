using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001060 : Event
    {
        public S11001060()
        {
            this.EventID = 11001060;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "埃米尔和活动木偶共存的地方，$R;" +
                "$R真是神奇的地方呀…$R;");
        }
    }
}