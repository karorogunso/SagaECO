using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001496 : Event
    {
        public S11001496()
        {
            this.EventID = 11001496;
        }

        public override void OnEvent(ActorPC pc)
        {

            Say(pc, 131, "讨厌泰达尼亚的人呢……。$R;", "美人鱼");

            Say(pc, 131, "这里的空气$R;" +
            "稍微有些变化呢。$R;", "美人鱼");
            Select(pc, "想要去哪里呢？", "", "商人在那里？", "有药店么？", "食品店呢？", "武器店呢？", "宠物医生在哪里？", "要存储在这里吗", "哪里也不去");
        }


    }
}


