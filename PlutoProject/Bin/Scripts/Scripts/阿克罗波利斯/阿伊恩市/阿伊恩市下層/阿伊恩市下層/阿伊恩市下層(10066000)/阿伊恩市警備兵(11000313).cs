using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10066000
{
    public class S11000313 : Event
    {
        public S11000313()
        {
            this.EventID = 11000313;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这里闲人勿进$R;" +
                "一般人不能进来$R;" +
                "想进来就去议会得到许可，再来吧$R;");
        }
    }
}