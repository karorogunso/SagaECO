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
                    "在那裡的小姐！真是挺可愛的！！$R;" +
                    "$R看您可愛，我特別告訴您好事吧，$R;" +
                    "$P這條街上有隱藏的店，$R;" +
                    "不會被人發現，$R;" +
                    "$R沒有招牌，但找找看，$R找到了跟我約會吧。$R;");
                return;
            }
            Say(pc, 131, "那位要走的帥哥！$R;" +
                "要介紹可愛的小姐呀？?$R;" +
                "$R只要介紹給我$R;" +
                "我會告訴您好的事情…$R;");
        }
    }
}