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
            Say(pc, 131, "在聽我的話嗎？$R;" +
                "預算裡浪費太多了$R;" +
                "$R武器購買費太多了啊$R;");
            Say(pc, 11000286, 131, "知道了啦！$R;");
        }
    }
}