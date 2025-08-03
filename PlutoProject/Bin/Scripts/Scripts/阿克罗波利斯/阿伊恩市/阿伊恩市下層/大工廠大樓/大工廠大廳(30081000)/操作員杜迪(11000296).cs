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
            Say(pc, 131, "都说了不是那边，$R;" +
                "是右边啦！！$R;");
            Say(pc, 11000297, 131, "好了，$R;" +
                "交给他们吧$R;");
        }
    }
}