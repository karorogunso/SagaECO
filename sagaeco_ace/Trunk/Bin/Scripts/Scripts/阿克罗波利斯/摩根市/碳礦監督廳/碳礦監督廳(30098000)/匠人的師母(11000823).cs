using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30098000
{
    public class S11000823 : Event
    {
        public S11000823()
        {
            this.EventID = 11000823;

        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "什么事情？$R;" +
                "男人都去干活了…$R;" +
                "如果没有重要的事情请出去好吗？$R;" +
                "$R打扫卫生、洗衣服…做饭…$R;" +
                "我很忙的啊$R;");
        }
    }
}