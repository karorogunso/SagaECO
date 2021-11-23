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

            Say(pc, 131, "討厭塔妮亞的人呀……。$R;", "美人魚");

            Say(pc, 131, "這裡稍稍的$R;" +
            "空氣有些變化呢。$R;", "美人魚");
            Select(pc, "想要去哪裡呢？", "", "商人在那裡？", "有藥店么？", "食品店呢？", "武器店呢？", "寵物醫生在哪裡？", "要存儲在這裡嗎", "哪裡也不要去");
        }


    }
}


