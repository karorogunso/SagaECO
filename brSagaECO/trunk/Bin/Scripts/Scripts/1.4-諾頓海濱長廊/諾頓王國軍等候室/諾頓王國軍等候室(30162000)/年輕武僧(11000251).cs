using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30162000
{
    public class S11000251 : Event
    {
        public S11000251()
        {
            this.EventID = 11000251;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "哥哥，這次休假$R;" +
                "我們倆一起去買東西怎麼樣？$R;");
            Say(pc, 11000252, 131, "嗯？$R;" +
                "我…那個…$R我有事情要做呢$R;");
            Say(pc, 131, "哥哥什麼時候有時間呢？$R;" +
                "我都可以配合的，無所謂$R;" +
                "$R只要能和哥哥在一起唷$R;");
        }
    }
}
