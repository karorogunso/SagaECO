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
    public class S11001059 : Event
    {
        public S11001059()
        {
            this.EventID = 11001059;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 131, "我是旅行的皮諾！$R;" +
                    "$R為了成為埃米爾在環遊世界唷。$R;");
                return;
            }
            Say(pc, 131, "這裡可以成為埃米爾嗎？$R;");
            Say(pc, 11001060, 131, "不知道行不行呀，$R;" +
                "應該能找到線索吧…$R;" +
                "$R這裡是您最初的皮諾玩偶故鄉！$R;");
        }
    }
}