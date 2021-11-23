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
    public class S11001143 : Event
    {
        public S11001143()
        {
            this.EventID = 11001143;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Gender == PC_GENDER.FEMALE)
            {
                Say(pc, 131, "嗨！！$R;" +
                    "在那裡的小姐！真是挺可爱的！！$R;" +
                    "$R看您可爱，我特别告诉您好事吧，$R;" +
                    "$P这条街上有隐藏的店，$R;" +
                    "不会被人发现，$R;" +
                    "$R没有招牌，但找找看，$R找到了跟我约会吧。$R;");
                return;
            }
            Say(pc, 131, "那位要走的帅哥！$R;" +
                "要介绍可爱的小姐呀？?$R;" +
                "$R只要介绍给我$R;" +
                "我会告诉您好的事情…$R;");
        }
    }
}