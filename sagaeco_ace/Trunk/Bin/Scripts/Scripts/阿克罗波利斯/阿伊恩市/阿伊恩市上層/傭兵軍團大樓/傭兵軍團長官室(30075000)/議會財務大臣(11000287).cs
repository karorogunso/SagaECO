using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30075000
{
    public class S11000287 : Event
    {
        public S11000287()
        {
            this.EventID = 11000287;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "在听我的话吗？$R;" +
                "预算里浪费太多了$R;" +
                "$R武器购买费太多了啊$R;");
            Say(pc, 11000286, 131, "知道了啦！$R;");
        }
    }
}