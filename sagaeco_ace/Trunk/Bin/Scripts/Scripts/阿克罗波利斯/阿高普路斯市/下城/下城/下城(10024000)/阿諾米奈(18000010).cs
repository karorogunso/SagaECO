using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Map;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class E18000010 : Event
    {
        public E18000010()
        {
            this.EventID = 18000010;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "哈啊 哈啊.....", "艾尔米娜");
            Say(pc, 0, "下半身....好热", "艾尔米娜");
            Say(pc, 0, "QAQ 啊? 我什么都没说!", "艾尔米娜");
        }
    }
}