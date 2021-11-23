using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30081000
{
    public class S11000296 : Event
    {
        public S11000296()
        {
            this.EventID = 11000296;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "都說了不是那邊，$R;" +
                "是右邊啦！！$R;");
            Say(pc, 11000297, 131, "好了，$R;" +
                "交給他們吧$R;");
        }
    }
}