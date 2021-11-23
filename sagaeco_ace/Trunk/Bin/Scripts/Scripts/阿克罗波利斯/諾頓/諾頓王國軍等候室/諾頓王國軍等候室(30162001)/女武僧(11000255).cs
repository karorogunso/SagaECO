using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30162001
{
    public class S11000255 : Event
    {
        public S11000255()
        {
            this.EventID = 11000255;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "您呀$R;" +
                "上次在女王陛下面前$R;" +
                "摔倒了是吗？$R;" +
                "$R到底在想什么呢？$R;" +
                "这么不小心，怎么行呢$R;");
            Say(pc, 11000254, 131, "对不起，姐姐$R;" +
                "（哭声）$R;");
        }
    }
}
