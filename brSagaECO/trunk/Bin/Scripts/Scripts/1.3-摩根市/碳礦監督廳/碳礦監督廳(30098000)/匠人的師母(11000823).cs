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
            Say(pc, 131, "什麼事情？$R;" +
                "男人都去幹活了…$R;" +
                "如果沒有重要的事情請出去好嗎？$R;" +
                "$R打掃衛生、洗衣服…做飯…$R;" +
                "我很忙的阿$R;");
        }
    }
}